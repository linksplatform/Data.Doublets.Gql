# Rust GraphQL Server for Data.Doublets

This is a Rust implementation of a GraphQL server for the Data.Doublets system using `async_graphql`.

## Features

- **Complete GraphQL Schema**: Full implementation of queries and mutations for:
  - Links (core doublets functionality)
  - Numbers, Strings, Objects (data types)
  - Materialized Path (MP) operations
  - Selectors and aggregation operations

- **Modern Async Architecture**:
  - Built with `async_graphql` v7.0 
  - Uses `actix-web` for HTTP server
  - Full async/await support throughout
  - Compatible with Tokio runtime

- **Data.Doublets Integration**:
  - Uses the Rust `doublets` crate for storage
  - File-mapped memory backend for performance
  - Supports all core doublets operations (create, read, update, delete)

## Dependencies

- `async_graphql` 7.0 - Modern GraphQL server library
- `actix-web` 4.9 - Web framework
- `doublets` 0.1.0-beta.3 - Core doublets storage
- `tokio` 1.47 - Async runtime
- `serde` 1.0 - Serialization support

## API Structure

### Queries
- `links` - Query links with filtering, ordering, and pagination
- `links_by_pk` - Get a specific link by ID
- `links_aggregate` - Aggregate operations on links
- Similar queries for `numbers`, `strings`, `objects`, `mp`, `selectors`

### Mutations  
- `insert_links` / `insert_links_one` - Create new links
- `delete_links_by_pk` - Delete links by ID
- `update_links` / `update_links_by_pk` - Update existing links
- Similar operations for other data types

## GraphQL Playground

When running, the server provides:
- GraphQL endpoint: `http://localhost:8000`
- Interactive GraphQL Playground: `http://localhost:8000` (GET request)

## Example Queries

### Query all links:
```graphql
{
  links {
    id
    from_id  
    to_id
  }
}
```

### Insert a new link:
```graphql
mutation {
  insert_links_one(object: {from_id: 1, to_id: 2}) {
    id
    from_id
    to_id
  }
}
```

## Building and Running

```bash
# Build the project
cargo build --release

# Run the server
cargo run

# The server will start on http://localhost:8000
```

## Implementation Status

✅ Core server infrastructure with async_graphql
✅ Complete schema definition
✅ Basic links queries and mutations  
✅ Integration with doublets storage
✅ GraphQL Playground interface
🔄 Some advanced operations still marked as `todo!()`

This implementation provides a solid foundation for a high-performance GraphQL API over the Data.Doublets storage system.