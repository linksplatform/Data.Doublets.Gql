# Platform.Data.Doublets.Client

Abstract API for both Platform.Data.Doublets.Native and Platform.Data.Doublets.Gql.Client, allowing seamless switching between native DLL and GraphQL implementations.

## Installation

```bash
nimble install platform_data_doublets_client
```

Note: You'll also need to install either or both of the concrete implementations:
- `nimble install platform_data_doublets_native` (for native DLL support)
- `nimble install platform_data_doublets_gql_client` (for GraphQL support)

## Usage

### Basic Usage

```nim
import platform_data_doublets_client

# Create a native client
let nativeClient = newNativeDoubletsClient("path/to/database.links")

# Create a GraphQL client  
let gqlClient = newGraphQLDoubletsClient("http://localhost:60341/v1/graphql")

# Both clients have the same API
proc useClient(client: IDoubletsClient) =
  # Create a new link
  let linkId = client.create(fromId = 1, toId = 2)
  
  # Get or create a link (preferred method)
  let linkId2 = client.getOrCreate(fromId = 3, toId = 4)
  
  # Get a link
  let link = client.get(linkId)
  echo "Link: ", link.id, " -> ", link.fromId, " -> ", link.toId
  
  # Check client type
  if client.isNative():
    echo "Using native implementation"
  elif client.isGraphQL():
    echo "Using GraphQL implementation"
  
  client.close()

# Use the same code with different backends
useClient(nativeClient)
useClient(gqlClient)
```

### Configuration-based Creation

```nim
import platform_data_doublets_client

# Native client configuration
let nativeConfig = DoubletsClientConfig(
  clientType: Native,
  databasePath: "database.links"
)

# GraphQL client configuration
let gqlConfig = DoubletsClientConfig(
  clientType: GraphQL,
  graphqlUrl: "http://localhost:60341/v1/graphql",
  headers: @[("Authorization", "Bearer token")]
)

# Create clients from configuration
let nativeClient = newDoubletsClient(nativeConfig)
let gqlClient = newDoubletsClient(gqlConfig)

# Use unified API
proc doOperations(client: IDoubletsClient) =
  echo "Client type: ", client.getClientType()
  let count = client.count()
  echo "Total links: ", count
  client.close()

doOperations(nativeClient)
doOperations(gqlClient)
```

### Batch Operations

```nim
import platform_data_doublets_client

let client = newGraphQLDoubletsClient("http://localhost:60341/v1/graphql")

# Create multiple links
let linksToCreate = @[(1.LinkIndex, 2.LinkIndex), (3.LinkIndex, 4.LinkIndex)]
let createdIds = client.createBatch(linksToCreate)

# Delete multiple links
client.deleteBatch(createdIds)

client.close()
```

### Async Operations

```nim
import platform_data_doublets_client, asyncdispatch

proc asyncOperations() {.async.} =
  let client = newGraphQLDoubletsClient("http://localhost:60341/v1/graphql")
  
  # Async operations (when supported by underlying client)
  let linkId = await client.createAsync(fromId = 1, toId = 2)
  let linkId2 = await client.getOrCreateAsync(fromId = 3, toId = 4)
  
  client.close()

waitFor asyncOperations()
```

## Requirements

- Nim >= 1.6.0
- At least one concrete implementation:
  - `platform_data_doublets_native` (requires Platform.Data.Doublets.dll)
  - `platform_data_doublets_gql_client` (requires running GraphQL server)

## API Reference

### Types

- `LinkIndex` - Alias for `uint64`, represents a link identifier
- `Link` - Object containing `id`, `fromId`, and `toId` fields
- `IDoubletsClient` - Abstract interface for all client implementations
- `DoubletsClientType` - Enumeration: `Native`, `GraphQL`
- `DoubletsClientConfig` - Configuration object for client creation
- `DoubletsException` - Exception type for doublets-related errors

### Factory Functions

- `newDoubletsClient(config)` - Create client from configuration
- `newNativeDoubletsClient(databasePath)` - Convenience for native client
- `newGraphQLDoubletsClient(graphqlUrl, headers)` - Convenience for GraphQL client

### Core Operations (IDoubletsClient Interface)

- `create(fromId, toId)` - Creates a new link
- `getOrCreate(fromId, toId)` - Gets existing or creates new link (recommended)
- `update(linkId, fromId, toId)` - Updates an existing link
- `update(oldFromId, oldToId, newFromId, newToId)` - Updates link by content
- `delete(linkId)` - Deletes a link
- `get(linkId)` - Retrieves a link as Link object
- `getLink(linkId)` - Retrieves a link as sequence [id, fromId, toId]
- `exists(linkId)` - Checks if link exists
- `count()` - Returns total number of links
- `close()` - Closes the client connection

### Client Type Inspection

- `isNative()` - Returns true if client uses native implementation
- `isGraphQL()` - Returns true if client uses GraphQL implementation
- `getClientType()` - Returns the DoubletsClientType

### Batch Operations

- `createBatch(links)` - Creates multiple links
- `deleteBatch(linkIds)` - Deletes multiple links

### Async Operations

- `createAsync(fromId, toId)` - Async create (when supported)
- `getOrCreateAsync(fromId, toId)` - Async get or create (when supported)

## Architecture

This package provides a unified interface that abstracts away the differences between native DLL and GraphQL implementations. The architecture allows:

1. **Seamless switching** between backends without code changes
2. **Runtime configuration** of which backend to use  
3. **Consistent API** across all implementations
4. **Future extensibility** for additional backends

## Notes

- The abstract client automatically handles the differences between native and GraphQL implementations
- Some operations may be more efficient with native implementation, others with GraphQL
- Async operations are primarily supported by the GraphQL client
- For maximum compatibility, use the synchronous API methods

## Example Applications

See the `examples/` directory for complete example applications showing:
- Migration between native and GraphQL backends
- Performance comparison between implementations
- Async vs sync usage patterns