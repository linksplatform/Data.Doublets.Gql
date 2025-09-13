# Platform.Data.Doublets for Nim

This directory contains three Nimble packages that provide comprehensive Doublets support for the Nim programming language:

## Packages

### 1. Platform.Data.Doublets.Native
**Location:** `platform_data_doublets_native/`
**Purpose:** Native DLL wrapper providing direct access to Platform.Data.Doublets native library

- Direct binding to Platform.Data.Doublets.dll
- Minimal overhead, maximum performance
- Synchronous operations only
- Requires native DLL to be available

### 2. Platform.Data.Doublets.Gql.Client  
**Location:** `platform_data_doublets_gql_client/`
**Purpose:** GraphQL client for remote Doublets operations

- Communicates with Platform.Data.Doublets GraphQL server
- Network-based operations
- Both synchronous and asynchronous support
- Requires running GraphQL server

### 3. Platform.Data.Doublets.Client
**Location:** `platform_data_doublets_client/`
**Purpose:** Abstract unified API for both native and GraphQL clients

- Seamless switching between native and GraphQL backends
- Unified interface and consistent API
- Runtime backend selection
- Future-proof for additional implementations

## Installation

You can install packages individually based on your needs:

```bash
# For native DLL support only
nimble install platform_data_doublets_native

# For GraphQL support only  
nimble install platform_data_doublets_gql_client

# For unified API (recommended)
nimble install platform_data_doublets_client

# Install all packages
nimble install platform_data_doublets_native platform_data_doublets_gql_client platform_data_doublets_client
```

## Quick Start

### Using the Unified API (Recommended)

```nim
import platform_data_doublets_client

# Native backend
let nativeClient = newNativeDoubletsClient("database.links")
let linkId1 = nativeClient.create(1, 2)

# GraphQL backend  
let gqlClient = newGraphQLDoubletsClient("http://localhost:60341/v1/graphql")
let linkId2 = gqlClient.create(3, 4)

# Same API for both!
proc processLinks(client: IDoubletsClient) =
  let count = client.count()
  echo "Total links: ", count
  client.close()

processLinks(nativeClient)
processLinks(gqlClient)
```

### Using Individual Packages

```nim
# Native only
import platform_data_doublets_native
let client = newDoubletsClient("database.links")
let linkId = client.create(1, 2)
client.close()

# GraphQL only
import platform_data_doublets_gql_client  
let client = newDoubletsGqlClient("http://localhost:60341/v1/graphql")
let linkId = client.create(1, 2)
client.close()
```

## Architecture Overview

```
┌─────────────────────────────────────────────┐
│        platform_data_doublets_client        │
│              (Unified API)                  │
├─────────────────────┬───────────────────────┤
│                     │                       │
│  Native Wrapper     │   GraphQL Wrapper     │
│                     │                       │
└─────────┬───────────┴───────────┬───────────┘
          │                       │
          ▼                       ▼
┌─────────────────────┐ ┌─────────────────────┐
│ platform_data_      │ │ platform_data_      │
│ doublets_native     │ │ doublets_gql_client │
│                     │ │                     │
│ ┌─────────────────┐ │ │ ┌─────────────────┐ │
│ │ Native DLL      │ │ │ │ HTTP Client     │ │
│ │ C API Bindings  │ │ │ │ GraphQL Queries │ │
│ └─────────────────┘ │ │ └─────────────────┘ │
└─────────────────────┘ └─────────────────────┘
          │                       │
          ▼                       ▼
┌─────────────────────┐ ┌─────────────────────┐
│ Platform.Data.      │ │ GraphQL Server      │
│ Doublets.dll        │ │ (C# Application)    │
└─────────────────────┘ └─────────────────────┘
```

## Use Cases

### When to Use Native Client
- Maximum performance required
- Local database access
- Synchronous operations preferred
- Native DLL is available

### When to Use GraphQL Client
- Remote server access required
- Network-based operations
- Asynchronous operations needed
- Multiple clients accessing same data

### When to Use Unified API
- Need to switch between backends
- Future-proofing against backend changes
- Consistent API across different deployments
- Recommended for most applications

## Core Operations

All packages provide the same core operations:

- `create(fromId, toId)` - Create a new link
- `getOrCreate(fromId, toId)` - Get existing or create new link
- `update(linkId, fromId, toId)` - Update an existing link
- `delete(linkId)` - Delete a link
- `get(linkId)` - Retrieve a link
- `exists(linkId)` - Check if link exists
- `count()` - Get total number of links
- `each()` - Iterate over all links

## Requirements

### Common Requirements
- Nim >= 1.6.0

### Native Package Requirements
- Platform.Data.Doublets.dll in system PATH or application directory
- Compatible C API implementation

### GraphQL Package Requirements
- httpclient, json, asyncdispatch packages
- Running Platform.Data.Doublets GraphQL server

## Testing

Each package includes its own test suite:

```bash
# Test native package
cd platform_data_doublets_native && nimble test

# Test GraphQL package  
cd platform_data_doublets_gql_client && nimble test

# Test unified client
cd platform_data_doublets_client && nimble test
```

## Contributing

1. Follow existing code style and patterns
2. Add tests for new functionality
3. Update documentation for API changes
4. Ensure compatibility across all packages

## License

All packages are licensed under LGPL-3.0-or-later, consistent with the main Platform.Data.Doublets project.

## Links

- [Main Repository](https://github.com/linksplatform/Data.Doublets.Gql)
- [Platform Documentation](https://github.com/LinksPlatform/Documentation)
- [Issue Tracker](https://github.com/linksplatform/Data.Doublets.Gql/issues)
- [Discord Community](https://discord.gg/eEXJyjWv5e)