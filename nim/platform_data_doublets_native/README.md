# Platform.Data.Doublets.Native

Native Nim wrapper for the Platform.Data.Doublets DLL API.

## Installation

```bash
nimble install platform_data_doublets_native
```

## Usage

```nim
import platform_data_doublets_native

# Open a database connection
let client = newDoubletsClient("path/to/database.links")

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

# Iterate over all links
for link in client.each():
  echo "Link: ", link.id, " -> ", link.fromId, " -> ", link.toId

# Delete a link
client.delete(linkId)

# Close connection
client.close()
```

## Requirements

- Nim >= 1.6.0
- Platform.Data.Doublets.dll in your system PATH or application directory
- The DLL must be compatible with the expected C API

## API Reference

### Types

- `LinkIndex` - Alias for `uint64`, represents a link identifier
- `Link` - Object containing `id`, `fromId`, and `toId` fields
- `DoubletsClient` - Main client class for database operations
- `DoubletsException` - Exception type for doublets-related errors

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
- `each()` - Iterator over all links

## Notes

This package provides a direct wrapper around the native Platform.Data.Doublets DLL. For a unified API that can work with both native and GraphQL backends, consider using the `platform_data_doublets_client` package instead.