# Platform.Data.Doublets.Gql.Client

GraphQL client for Platform.Data.Doublets operations.

## Installation

```bash
nimble install platform_data_doublets_gql_client
```

## Usage

```nim
import platform_data_doublets_gql_client
import httpclient, asyncdispatch

# Create client with basic connection
let client = newDoubletsGqlClient("http://localhost:60341/v1/graphql")

# Create client with custom headers
let headers = newHttpHeaders()
headers["Authorization"] = "Bearer your-token"
let authClient = newDoubletsGqlClient("http://localhost:60341/v1/graphql", headers)

# Create a new link
let linkId = client.create(fromId = 1, toId = 2)

# Get or create a link (preferred method)
let linkId2 = client.getOrCreate(fromId = 3, toId = 4)

# Update a link
let updatedId = client.update(linkId, newFromId = 5, newToId = 6)

# Get a link
let link = client.get(linkId)
echo "Link: ", link.id, " -> ", link.fromId, " -> ", link.toId

# Check if link exists
if client.exists(linkId):
  echo "Link exists"

# Get total count
echo "Total links: ", client.count()

# Iterate over all links (in batches)
for link in client.each(batchSize = 100):
  echo "Link: ", link.id, " -> ", link.fromId, " -> ", link.toId

# Delete a link
client.delete(linkId)

# Close connections
client.close()
```

## Async Usage

```nim
import asyncdispatch

proc asyncOperations() {.async.} =
  let client = newDoubletsGqlClient("http://localhost:60341/v1/graphql")
  
  # Async create
  let linkId = await client.createAsync(fromId = 1, toId = 2)
  
  # Async get or create
  let linkId2 = await client.getOrCreateAsync(fromId = 3, toId = 4)
  
  client.close()

waitFor asyncOperations()
```

## Requirements

- Nim >= 1.6.0
- httpclient >= 1.0.0
- json >= 1.0.0
- asyncdispatch >= 1.0.0
- Running Platform.Data.Doublets GraphQL server

## API Reference

### Types

- `LinkIndex` - Alias for `uint64`, represents a link identifier
- `Link` - Object containing `id`, `fromId`, and `toId` fields
- `DoubletsGqlClient` - Main client class for GraphQL operations
- `DoubletsGqlException` - Exception type for GraphQL-related errors

### Core Operations

- `create(fromId, toId)` - Creates a new link
- `getOrCreate(fromId, toId)` - Gets existing or creates new link (recommended)
- `update(linkId, fromId, toId)` - Updates an existing link
- `update(oldFromId, oldToId, newFromId, newToId)` - Updates link by content
- `delete(linkId)` - Deletes a link
- `get(linkId)` - Retrieves a link as Link object
- `getLink(linkId)` - Retrieves a link as sequence [id, fromId, toId]
- `exists(linkId)` - Checks if link exists
- `count()` - Returns total number of links
- `each(batchSize)` - Iterator over all links in batches

### Async Operations

- `createAsync(fromId, toId)` - Async create
- `getOrCreateAsync(fromId, toId)` - Async get or create
- All other operations can be wrapped in async procedures

### Low-level Operations

- `executeGraphQL(query)` - Execute raw GraphQL query
- `executeGraphQLAsync(query)` - Execute raw GraphQL query async

## GraphQL Server

This client is designed to work with the Platform.Data.Doublets GraphQL server. You can start the server locally:

```bash
cd csharp/Platform.Data.Doublets.Gql.Server
dotnet run
```

The server will be available at `http://localhost:60341/v1/graphql`.

## Notes

This package provides a GraphQL client for Platform.Data.Doublets operations. For a unified API that can work with both GraphQL and native backends, consider using the `platform_data_doublets_client` package instead.