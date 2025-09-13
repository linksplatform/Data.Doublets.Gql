## Platform.Data.Doublets.Gql.Client
##
## This module provides a GraphQL client for Doublets operations.
## It implements the core ILinks interface for CRUD operations via GraphQL.

import httpclient, json, asyncdispatch, strutils, strformat

type
  LinkIndex* = uint64
  
  Link* = object
    id*: LinkIndex
    fromId*: LinkIndex  
    toId*: LinkIndex

  DoubletsGqlException* = object of CatchableError

  DoubletsGqlClient* = ref object
    graphqlUrl*: string
    httpClient*: HttpClient
    asyncHttpClient*: AsyncHttpClient
    headers*: HttpHeaders

proc newDoubletsGqlClient*(graphqlUrl: string, headers: HttpHeaders = nil): DoubletsGqlClient =
  ## Creates a new DoubletsGqlClient connected to the specified GraphQL endpoint
  result = DoubletsGqlClient()
  result.graphqlUrl = graphqlUrl
  result.httpClient = newHttpClient()
  result.asyncHttpClient = newAsyncHttpClient()
  result.headers = if headers != nil: headers else: newHttpHeaders()
  result.headers["Content-Type"] = "application/json"

proc close*(client: DoubletsGqlClient) =
  ## Closes the HTTP connections
  if not client.httpClient.isNil:
    client.httpClient.close()
  if not client.asyncHttpClient.isNil:
    client.asyncHttpClient.close()

proc executeGraphQL*(client: DoubletsGqlClient, query: string): JsonNode =
  ## Executes a GraphQL query and returns the JSON response
  let body = %*{
    "query": query
  }
  
  let response = client.httpClient.request(
    client.graphqlUrl, 
    httpMethod = HttpPost,
    body = $body,
    headers = client.headers
  )
  
  if response.status.startsWith("2"):
    let jsonResponse = parseJson(response.body)
    if jsonResponse.hasKey("errors"):
      raise newException(DoubletsGqlException, "GraphQL error: " & $jsonResponse["errors"])
    return jsonResponse
  else:
    raise newException(DoubletsGqlException, "HTTP error: " & response.status)

proc executeGraphQLAsync*(client: DoubletsGqlClient, query: string): Future[JsonNode] {.async.} =
  ## Asynchronously executes a GraphQL query and returns the JSON response
  let body = %*{
    "query": query
  }
  
  let response = await client.asyncHttpClient.request(
    client.graphqlUrl,
    httpMethod = HttpPost, 
    body = $body,
    headers = client.headers
  )
  
  if response.status.startsWith("2"):
    let jsonResponse = parseJson(response.body)
    if jsonResponse.hasKey("errors"):
      raise newException(DoubletsGqlException, "GraphQL error: " & $jsonResponse["errors"])
    return jsonResponse
  else:
    raise newException(DoubletsGqlException, "HTTP error: " & response.status)

# GraphQL query builders
proc buildLinksQuery(whereClause: string = "", limit: int = -1, offset: int = 0): string =
  var query = "{ links"
  
  var args: seq[string] = @[]
  if whereClause != "":
    args.add(&"where: {whereClause}")
  if limit > 0:
    args.add(&"limit: {limit}")
  if offset > 0:
    args.add(&"offset: {offset}")
  
  if args.len > 0:
    query &= "(" & args.join(", ") & ")"
  
  query &= " { id from_id to_id } }"
  return query

proc buildInsertMutation(fromId: LinkIndex, toId: LinkIndex): string =
  return &"""mutation {{ 
    insert_links_one(object: {{from_id: {fromId}, to_id: {toId}}}) {{ 
      id from_id to_id 
    }} 
  }}"""

proc buildUpdateMutation(linkId: LinkIndex, fromId: LinkIndex, toId: LinkIndex): string =
  return &"""mutation {{ 
    update_links(_set: {{from_id: {fromId}, to_id: {toId}}}, where: {{id: {{_eq: {linkId}}}}}) {{ 
      returning {{ id from_id to_id }} 
    }} 
  }}"""

proc buildUpdateMutationByFromTo(oldFromId: LinkIndex, oldToId: LinkIndex, newFromId: LinkIndex, newToId: LinkIndex): string =
  return &"""mutation {{ 
    update_links(_set: {{from_id: {newFromId}, to_id: {newToId}}}, where: {{from_id: {{_eq: {oldFromId}}}, to_id: {{_eq: {oldToId}}}}}) {{ 
      returning {{ id from_id to_id }} 
    }} 
  }}"""

proc buildDeleteMutation(linkId: LinkIndex): string =
  return &"""mutation {{ 
    delete_links(where: {{id: {{_eq: {linkId}}}}}) {{ 
      returning {{ id from_id to_id }} 
    }} 
  }}"""

proc buildCountQuery(): string =
  return "{ links_aggregate { aggregate { count } } }"

# Convert JSON response to Link objects
proc jsonToLink(jsonNode: JsonNode): Link =
  return Link(
    id: LinkIndex(jsonNode["id"].getInt()),
    fromId: LinkIndex(jsonNode["from_id"].getInt()),
    toId: LinkIndex(jsonNode["to_id"].getInt())
  )

proc jsonToLinks(jsonNode: JsonNode): seq[Link] =
  result = @[]
  if jsonNode.hasKey("data") and jsonNode["data"].hasKey("links"):
    for linkJson in jsonNode["data"]["links"]:
      result.add(jsonToLink(linkJson))

# Core CRUD operations
proc create*(client: DoubletsGqlClient, fromId: LinkIndex, toId: LinkIndex): LinkIndex =
  ## Creates a new link with specified fromId and toId
  ## Returns the id of the created link
  let query = buildInsertMutation(fromId, toId)
  let response = client.executeGraphQL(query)
  
  if response.hasKey("data") and response["data"].hasKey("insert_links_one"):
    return LinkIndex(response["data"]["insert_links_one"]["id"].getInt())
  else:
    raise newException(DoubletsGqlException, "Failed to create link")

proc getOrCreate*(client: DoubletsGqlClient, fromId: LinkIndex, toId: LinkIndex): LinkIndex =
  ## Gets existing link or creates a new one with specified fromId and toId
  ## This is the primary method for creating/retrieving links
  
  # First try to find existing link
  let whereClause = &"{{from_id: {{_eq: {fromId}}}, to_id: {{_eq: {toId}}}}}"
  let query = buildLinksQuery(whereClause, limit = 1)
  let response = client.executeGraphQL(query)
  
  let links = jsonToLinks(response)
  if links.len > 0:
    return links[0].id
  else:
    # Create new link if not found
    return client.create(fromId, toId)

proc update*(client: DoubletsGqlClient, linkId: LinkIndex, fromId: LinkIndex, toId: LinkIndex): LinkIndex =
  ## Updates an existing link with new fromId and toId
  ## Returns the id of the updated link
  let query = buildUpdateMutation(linkId, fromId, toId)
  let response = client.executeGraphQL(query)
  
  if response.hasKey("data") and response["data"].hasKey("update_links"):
    let returning = response["data"]["update_links"]["returning"]
    if returning.len > 0:
      return LinkIndex(returning[0]["id"].getInt())
    else:
      raise newException(DoubletsGqlException, "No link was updated")
  else:
    raise newException(DoubletsGqlException, "Failed to update link")

proc update*(client: DoubletsGqlClient, oldFromId: LinkIndex, oldToId: LinkIndex, newFromId: LinkIndex, newToId: LinkIndex): LinkIndex =
  ## Updates a link identified by oldFromId and oldToId with new values
  let query = buildUpdateMutationByFromTo(oldFromId, oldToId, newFromId, newToId)
  let response = client.executeGraphQL(query)
  
  if response.hasKey("data") and response["data"].hasKey("update_links"):
    let returning = response["data"]["update_links"]["returning"]
    if returning.len > 0:
      return LinkIndex(returning[0]["id"].getInt())
    else:
      raise newException(DoubletsGqlException, "No link was updated")
  else:
    raise newException(DoubletsGqlException, "Failed to update link")

proc delete*(client: DoubletsGqlClient, linkId: LinkIndex) =
  ## Deletes a link by its id
  let query = buildDeleteMutation(linkId)
  let response = client.executeGraphQL(query)
  
  if not response.hasKey("data") or not response["data"].hasKey("delete_links"):
    raise newException(DoubletsGqlException, "Failed to delete link")

proc get*(client: DoubletsGqlClient, linkId: LinkIndex): Link =
  ## Gets a link by its id
  ## Returns a Link object with id, fromId, and toId
  let whereClause = &"{{id: {{_eq: {linkId}}}}}"
  let query = buildLinksQuery(whereClause, limit = 1)
  let response = client.executeGraphQL(query)
  
  let links = jsonToLinks(response)
  if links.len > 0:
    return links[0]
  else:
    raise newException(DoubletsGqlException, "Link not found: " & $linkId)

proc getLink*(client: DoubletsGqlClient, linkId: LinkIndex): seq[LinkIndex] =
  ## Gets a link as a sequence [id, fromId, toId]
  let link = client.get(linkId)
  result = @[link.id, link.fromId, link.toId]

proc exists*(client: DoubletsGqlClient, linkId: LinkIndex): bool =
  ## Checks if a link exists by its id
  try:
    discard client.get(linkId)
    return true
  except DoubletsGqlException:
    return false

proc count*(client: DoubletsGqlClient): LinkIndex =
  ## Returns the total number of links in the database
  let query = buildCountQuery()
  let response = client.executeGraphQL(query)
  
  if response.hasKey("data") and response["data"].hasKey("links_aggregate"):
    return LinkIndex(response["data"]["links_aggregate"]["aggregate"]["count"].getInt())
  else:
    return 0

iterator each*(client: DoubletsGqlClient, batchSize: int = 1000): Link =
  ## Iterates over all links in the database in batches
  var offset = 0
  var hasMore = true
  
  while hasMore:
    let query = buildLinksQuery("", limit = batchSize, offset = offset)
    let response = client.executeGraphQL(query)
    let links = jsonToLinks(response)
    
    if links.len == 0:
      hasMore = false
    else:
      for link in links:
        yield link
      offset += batchSize
      hasMore = links.len == batchSize

# Async versions of core operations
proc createAsync*(client: DoubletsGqlClient, fromId: LinkIndex, toId: LinkIndex): Future[LinkIndex] {.async.} =
  ## Asynchronously creates a new link with specified fromId and toId
  let query = buildInsertMutation(fromId, toId)
  let response = await client.executeGraphQLAsync(query)
  
  if response.hasKey("data") and response["data"].hasKey("insert_links_one"):
    return LinkIndex(response["data"]["insert_links_one"]["id"].getInt())
  else:
    raise newException(DoubletsGqlException, "Failed to create link")

proc getOrCreateAsync*(client: DoubletsGqlClient, fromId: LinkIndex, toId: LinkIndex): Future[LinkIndex] {.async.} =
  ## Asynchronously gets existing link or creates a new one with specified fromId and toId
  let whereClause = &"{{from_id: {{_eq: {fromId}}}, to_id: {{_eq: {toId}}}}}"
  let query = buildLinksQuery(whereClause, limit = 1)
  let response = await client.executeGraphQLAsync(query)
  
  let links = jsonToLinks(response)
  if links.len > 0:
    return links[0].id
  else:
    return await client.createAsync(fromId, toId)

# Cleanup
proc `=destroy`*(client: DoubletsGqlClient) =
  if not client.httpClient.isNil:
    client.close()