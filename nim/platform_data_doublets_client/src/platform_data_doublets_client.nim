## Platform.Data.Doublets.Client
##
## This module provides an abstract API that works with both 
## Platform.Data.Doublets.Native and Platform.Data.Doublets.Gql.Client
## allowing users to swap between native DLL and GraphQL implementations seamlessly.

import asyncdispatch

type
  LinkIndex* = uint64
  
  Link* = object
    id*: LinkIndex
    fromId*: LinkIndex
    toId*: LinkIndex

  DoubletsException* = object of CatchableError

  # Abstract interface for Doublets operations
  IDoubletsClient* = ref object of RootObj

  # Client implementation types
  DoubletsClientType* = enum
    Native, GraphQL

  DoubletsClientConfig* = object
    case clientType*: DoubletsClientType
    of Native:
      databasePath*: string
    of GraphQL:
      graphqlUrl*: string
      headers*: seq[(string, string)]

# Abstract methods that must be implemented by concrete clients
method create*(client: IDoubletsClient, fromId: LinkIndex, toId: LinkIndex): LinkIndex {.base.} =
  ## Creates a new link with specified fromId and toId
  ## Returns the id of the created link
  raise newException(CatchableError, "Method not implemented")

method getOrCreate*(client: IDoubletsClient, fromId: LinkIndex, toId: LinkIndex): LinkIndex {.base.} =
  ## Gets existing link or creates a new one with specified fromId and toId
  ## This is the primary method for creating/retrieving links
  raise newException(CatchableError, "Method not implemented")

method update*(client: IDoubletsClient, linkId: LinkIndex, fromId: LinkIndex, toId: LinkIndex): LinkIndex {.base.} =
  ## Updates an existing link with new fromId and toId
  ## Returns the id of the updated link
  raise newException(CatchableError, "Method not implemented")

method update*(client: IDoubletsClient, oldFromId: LinkIndex, oldToId: LinkIndex, newFromId: LinkIndex, newToId: LinkIndex): LinkIndex {.base.} =
  ## Updates a link identified by oldFromId and oldToId with new values
  raise newException(CatchableError, "Method not implemented")

method delete*(client: IDoubletsClient, linkId: LinkIndex) {.base.} =
  ## Deletes a link by its id
  raise newException(CatchableError, "Method not implemented")

method get*(client: IDoubletsClient, linkId: LinkIndex): Link {.base.} =
  ## Gets a link by its id
  ## Returns a Link object with id, fromId, and toId
  raise newException(CatchableError, "Method not implemented")

method getLink*(client: IDoubletsClient, linkId: LinkIndex): seq[LinkIndex] {.base.} =
  ## Gets a link as a sequence [id, fromId, toId]
  raise newException(CatchableError, "Method not implemented")

method exists*(client: IDoubletsClient, linkId: LinkIndex): bool {.base.} =
  ## Checks if a link exists by its id
  raise newException(CatchableError, "Method not implemented")

method count*(client: IDoubletsClient): LinkIndex {.base.} =
  ## Returns the total number of links in the database
  raise newException(CatchableError, "Method not implemented")

method close*(client: IDoubletsClient) {.base.} =
  ## Closes the client connection
  raise newException(CatchableError, "Method not implemented")

# Iterator interface - implemented as a template since we can't have virtual iterators
template each*(client: IDoubletsClient): untyped =
  ## Template for iterating over all links - must be implemented by concrete types
  {.error: "each iterator must be implemented by concrete client types".}

# Factory function to create appropriate client based on configuration
proc newDoubletsClient*(config: DoubletsClientConfig): IDoubletsClient =
  ## Factory function that creates the appropriate client based on configuration
  case config.clientType
  of Native:
    # Import and create native client
    # Note: This would require conditional compilation in real usage
    when compiles(import ../platform_data_doublets_native/src/platform_data_doublets_native):
      import ../platform_data_doublets_native/src/platform_data_doublets_native as native_client
      return NativeClientWrapper(nativeClient: native_client.newDoubletsClient(config.databasePath))
    else:
      raise newException(DoubletsException, "Native client not available. Please install platform_data_doublets_native package.")
  
  of GraphQL:
    # Import and create GraphQL client
    when compiles(import ../platform_data_doublets_gql_client/src/platform_data_doublets_gql_client):
      import ../platform_data_doublets_gql_client/src/platform_data_doublets_gql_client as gql_client
      import httpclient
      
      var headers: HttpHeaders = nil
      if config.headers.len > 0:
        headers = newHttpHeaders()
        for (key, value) in config.headers:
          headers[key] = value
      
      return GqlClientWrapper(gqlClient: gql_client.newDoubletsGqlClient(config.graphqlUrl, headers))
    else:
      raise newException(DoubletsException, "GraphQL client not available. Please install platform_data_doublets_gql_client package.")

# Wrapper types for concrete implementations
type
  NativeClientWrapper* = ref object of IDoubletsClient
    nativeClient*: pointer  # Would be DoubletsClient from native package

  GqlClientWrapper* = ref object of IDoubletsClient  
    gqlClient*: pointer  # Would be DoubletsGqlClient from gql package

# Implementation templates for wrappers
# These would be implemented with proper imports in real usage

# Convenience constructors
proc newNativeDoubletsClient*(databasePath: string): IDoubletsClient =
  ## Convenience constructor for native client
  let config = DoubletsClientConfig(
    clientType: Native,
    databasePath: databasePath
  )
  return newDoubletsClient(config)

proc newGraphQLDoubletsClient*(graphqlUrl: string, headers: seq[(string, string)] = @[]): IDoubletsClient =
  ## Convenience constructor for GraphQL client
  let config = DoubletsClientConfig(
    clientType: GraphQL,
    graphqlUrl: graphqlUrl,
    headers: headers
  )
  return newDoubletsClient(config)

# Async support interface
method createAsync*(client: IDoubletsClient, fromId: LinkIndex, toId: LinkIndex): Future[LinkIndex] {.base, async.} =
  ## Asynchronous create - default implementation calls sync version
  return client.create(fromId, toId)

method getOrCreateAsync*(client: IDoubletsClient, fromId: LinkIndex, toId: LinkIndex): Future[LinkIndex] {.base, async.} =
  ## Asynchronous getOrCreate - default implementation calls sync version
  return client.getOrCreate(fromId, toId)

# Utility functions for working with the unified API
proc isNative*(client: IDoubletsClient): bool =
  ## Checks if the client is using native implementation
  return client of NativeClientWrapper

proc isGraphQL*(client: IDoubletsClient): bool =
  ## Checks if the client is using GraphQL implementation
  return client of GqlClientWrapper

proc getClientType*(client: IDoubletsClient): DoubletsClientType =
  ## Returns the type of the underlying client implementation
  if client.isNative():
    return Native
  elif client.isGraphQL():
    return GraphQL
  else:
    raise newException(DoubletsException, "Unknown client type")

# Batch operations helper
proc createBatch*(client: IDoubletsClient, links: seq[(LinkIndex, LinkIndex)]): seq[LinkIndex] =
  ## Creates multiple links in batch
  result = @[]
  for (fromId, toId) in links:
    result.add(client.create(fromId, toId))

proc deleteBatch*(client: IDoubletsClient, linkIds: seq[LinkIndex]) =
  ## Deletes multiple links in batch
  for linkId in linkIds:
    client.delete(linkId)