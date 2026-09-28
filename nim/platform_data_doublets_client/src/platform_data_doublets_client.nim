## One idiomatic API with interchangeable native and GraphQL backends.
import std/[options, httpclient]
import platform_data_doublets_native as native
import platform_data_doublets_gql_client as gql

type
  Link* = object
    id*, source*, target*: uint64
  Backend* = enum nativeBackend, graphqlBackend
  DoubletsClient* = ref object
    case backend: Backend
    of nativeBackend: nativeClient: native.NativeClient
    of graphqlBackend: graphqlClient: gql.GraphqlClient

proc openNativeClient*(databasePath: string;
                       libraryPath = native.defaultLibraryPath()): DoubletsClient =
  DoubletsClient(backend: nativeBackend, nativeClient: native.openNative(databasePath, libraryPath))

proc openGraphqlClient*(endpoint: string; timeoutMs = 10_000;
                        headers: HttpHeaders = nil): DoubletsClient =
  DoubletsClient(backend: graphqlBackend, graphqlClient: gql.openGraphql(endpoint, timeoutMs, headers))

proc close*(client: DoubletsClient) =
  if client == nil: return
  case client.backend
  of nativeBackend: client.nativeClient.close()
  of graphqlBackend: client.graphqlClient.close()

proc checkClient(client: DoubletsClient) =
  if client == nil: raise newException(ValueError, "Doublets client is nil")

proc convert(link: native.NativeLink): Link = Link(id: link.id, source: link.source, target: link.target)
proc convert(link: gql.GqlLink): Link = Link(id: link.id, source: link.source, target: link.target)

proc get*(client: DoubletsClient; id: uint64): Option[Link] =
  client.checkClient()
  case client.backend
  of nativeBackend:
    let row = client.nativeClient.get(id)
    if row.isSome: result = some(convert(row.get))
  of graphqlBackend:
    let row = client.graphqlClient.get(id)
    if row.isSome: result = some(convert(row.get))

proc create*(client: DoubletsClient; source = 0'u64; target = 0'u64): Link =
  client.checkClient()
  case client.backend
  of nativeBackend: convert(client.nativeClient.create(source, target))
  of graphqlBackend: convert(client.graphqlClient.create(source, target))

proc getOrCreate*(client: DoubletsClient; source, target: uint64): Link = client.create(source, target)

proc update*(client: DoubletsClient; id, source, target: uint64): Option[Link] =
  client.checkClient()
  case client.backend
  of nativeBackend:
    let row = client.nativeClient.update(id, source, target)
    if row.isSome: result = some(convert(row.get))
  of graphqlBackend:
    let row = client.graphqlClient.update(id, source, target)
    if row.isSome: result = some(convert(row.get))

proc delete*(client: DoubletsClient; id: uint64): bool =
  client.checkClient()
  case client.backend
  of nativeBackend: client.nativeClient.delete(id)
  of graphqlBackend: client.graphqlClient.delete(id)

proc all*(client: DoubletsClient): seq[Link] =
  client.checkClient()
  case client.backend
  of nativeBackend:
    for row in client.nativeClient.all(): result.add(convert(row))
  of graphqlBackend:
    for row in client.graphqlClient.all(): result.add(convert(row))

proc count*(client: DoubletsClient): int =
  client.checkClient()
  case client.backend
  of nativeBackend: client.nativeClient.count()
  of graphqlBackend: client.graphqlClient.count()

proc exists*(client: DoubletsClient; id: uint64): bool = client.get(id).isSome

proc find*(client: DoubletsClient; source, target: uint64): Option[Link] =
  client.checkClient()
  case client.backend
  of nativeBackend:
    let row = client.nativeClient.find(source, target)
    if row.isSome: result = some(convert(row.get))
  of graphqlBackend:
    let row = client.graphqlClient.find(source, target)
    if row.isSome: result = some(convert(row.get))

proc each*(client: DoubletsClient; visitor: proc(link: Link): bool) =
  for link in client.all():
    if not visitor(link): break

proc backend*(client: DoubletsClient): Backend =
  client.checkClient()
  client.backend
