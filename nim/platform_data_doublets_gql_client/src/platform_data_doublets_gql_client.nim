## Standalone synchronous GraphQL transport. Uses only Nim's standard library.
## Compile with -d:ssl for HTTPS. No native library is loaded by this package.
import std/[httpclient, json, options, uri]

type
  GraphqlError* = object of CatchableError
  GqlLink* = object
    id*, source*, target*: uint64
  GraphqlClient* = ref object
    endpoint: string
    transport: HttpClient

proc openGraphql*(endpoint: string; timeoutMs = 10_000;
                  headers: HttpHeaders = nil): GraphqlClient =
  let parsed = parseUri(endpoint)
  if parsed.scheme notin ["http", "https"] or parsed.hostname.len == 0:
    raise newException(ValueError, "GraphQL endpoint must be an absolute HTTP(S) URL")
  if timeoutMs <= 0: raise newException(ValueError, "HTTP timeout must be positive")
  let client = newHttpClient(timeout = timeoutMs)
  if headers != nil: client.headers = headers
  client.headers["Content-Type"] = "application/json"
  result = GraphqlClient(endpoint: endpoint, transport: client)

proc close*(client: GraphqlClient) =
  if client != nil and client.transport != nil:
    client.transport.close()
    client.transport = nil

proc checkOpen(client: GraphqlClient) =
  if client == nil or client.transport == nil:
    raise newException(GraphqlError, "GraphQL client is closed")

proc execute*(client: GraphqlClient; query: string;
              variables: JsonNode = nil): JsonNode =
  ## Returns data. Any GraphQL error, including partial-data errors, is raised.
  ## Writes are never retried automatically after transport failures.
  client.checkOpen()
  let payload = %* {"query": query}
  if variables != nil:
    if variables.kind != JObject: raise newException(ValueError, "Variables must be a JSON object")
    payload["variables"] = variables
  var response: Response
  try:
    response = client.transport.request(client.endpoint, httpMethod = HttpPost, body = $payload)
  except CatchableError as error:
    raise newException(GraphqlError, "GraphQL transport failed: " & error.msg)
  if response.code.int < 200 or response.code.int >= 300:
    raise newException(GraphqlError, "GraphQL HTTP status " & $response.code.int)
  var text: string
  try: text = response.body
  except CatchableError as error:
    raise newException(GraphqlError, "GraphQL response body failed: " & error.msg)
  var body: JsonNode
  try: body = parseJson(text)
  except JsonParsingError:
    raise newException(GraphqlError, "GraphQL response is not valid JSON")
  if body.kind != JObject: raise newException(GraphqlError, "GraphQL response must be an object")
  if body.hasKey("errors") and body["errors"].kind != JNull:
    if body["errors"].kind != JArray or body["errors"].len > 0:
      raise newException(GraphqlError, "GraphQL response contains errors: " & $body["errors"])
  if not body.hasKey("data") or body["data"].kind != JObject:
    raise newException(GraphqlError, "GraphQL response has no data object")
  body["data"]

proc requireField(node: JsonNode; key: string; kind: JsonNodeKind): JsonNode =
  if node.kind != JObject or not node.hasKey(key) or node[key].kind != kind:
    raise newException(GraphqlError, "Malformed GraphQL field: " & key)
  node[key]

proc unsigned(node: JsonNode; key: string): uint64 =
  let value = requireField(node, key, JInt).getBiggestInt()
  if value < 0: raise newException(GraphqlError, "Negative link address in response")
  uint64(value)

proc parseLink(node: JsonNode): GqlLink =
  result = GqlLink(id: unsigned(node, "id"), source: unsigned(node, "from_id"),
                   target: unsigned(node, "to_id"))
  if result.id == 0: raise newException(GraphqlError, "Zero link ID in response")

proc address(value: uint64): string =
  if value > uint64(high(int64)):
    raise newException(ValueError, "GraphQL addresses must fit signed 64-bit integers")
  $value

const fields = "id from_id to_id"

proc all*(client: GraphqlClient): seq[GqlLink] =
  let data = client.execute("{links{" & fields & "}}")
  for row in requireField(data, "links", JArray): result.add(parseLink(row))

proc get*(client: GraphqlClient; id: uint64): Option[GqlLink] =
  client.checkOpen()
  if id == 0: return none(GqlLink)
  # Use the documented links(where:) field; main C# has no working by_pk resolver.
  let data = client.execute("{links(where:{id:{_eq:" & address(id) & "}}){" & fields & "}}")
  let rows = requireField(data, "links", JArray)
  if rows.len > 1: raise newException(GraphqlError, "Multiple links returned for one ID")
  if rows.len == 1:
    let link = parseLink(rows[0])
    if link.id != id: raise newException(GraphqlError, "GraphQL returned a different link ID")
    result = some(link)

proc exists*(client: GraphqlClient; id: uint64): bool = client.get(id).isSome
proc count*(client: GraphqlClient): int = client.all().len

proc find*(client: GraphqlClient; source, target: uint64): Option[GqlLink] =
  discard address(source)
  discard address(target)
  # The original C# native indices do not reliably find zero endpoints.
  # Blank rows may already be duplicated by raw server inserts: pick the lowest ID.
  if source == 0 or target == 0:
    for link in client.all():
      if link.source == source and link.target == target and
          (result.isNone or link.id < result.get.id):
        result = some(link)
    return
  let data = client.execute("{links(where:{from_id:{_eq:" & address(source) &
    "},to_id:{_eq:" & address(target) & "}}){" & fields & "}}")
  let rows = requireField(data, "links", JArray)
  if rows.len > 1: raise newException(GraphqlError, "Duplicate source/target pairs in response")
  if rows.len == 1:
    let link = parseLink(rows[0])
    if link.source != source or link.target != target:
      raise newException(GraphqlError, "GraphQL returned a different link pair")
    result = some(link)

proc create*(client: GraphqlClient; source = 0'u64; target = 0'u64): GqlLink =
  if source == 0 or target == 0:
    let existing = client.find(source, target)
    if existing.isSome: return existing.get
  let data = client.execute("mutation{insert_links_one(object:{from_id:" & address(source) &
    ",to_id:" & address(target) & "}){" & fields & "}}")
  result = parseLink(requireField(data, "insert_links_one", JObject))
  if result.source != source or result.target != target:
    raise newException(GraphqlError, "GraphQL insert returned a different pair")

proc getOrCreate*(client: GraphqlClient; source, target: uint64): GqlLink =
  client.create(source, target)

proc update*(client: GraphqlClient; id, source, target: uint64): Option[GqlLink] =
  # Validate all addresses before any request or write.
  let index = address(id)
  let fromId = address(source)
  let toId = address(target)
  if client.get(id).isNone: return none(GqlLink)
  let duplicate = client.find(source, target)
  if duplicate.isSome and duplicate.get.id != id:
    raise newException(ValueError, "Another link already has this source/target pair")
  let data = client.execute("mutation{update_links(where:{id:{_eq:" & index &
    "}},_set:{from_id:" & fromId & ",to_id:" & toId & "}){affected_rows returning{" & fields & "}}}")
  let response = requireField(data, "update_links", JObject)
  let affected = requireField(response, "affected_rows", JInt).getInt()
  let rows = requireField(response, "returning", JArray)
  if affected == 0 and rows.len == 0: return none(GqlLink)
  if affected != 1 or rows.len != 1:
    raise newException(GraphqlError, "Unexpected update result count")
  let link = parseLink(rows[0])
  if link.id != id or link.source != source or link.target != target:
    raise newException(GraphqlError, "GraphQL update returned a different link")
  some(link)

proc delete*(client: GraphqlClient; id: uint64): bool =
  client.checkOpen()
  if id == 0: return false
  let data = client.execute("mutation{delete_links(where:{id:{_eq:" & address(id) & "}}){affected_rows}}")
  let affected = requireField(requireField(data, "delete_links", JObject), "affected_rows", JInt).getInt()
  if affected < 0 or affected > 1: raise newException(GraphqlError, "Unexpected delete result count")
  affected == 1

proc each*(client: GraphqlClient; visitor: proc(link: GqlLink): bool) =
  for link in client.all():
    if not visitor(link): break
