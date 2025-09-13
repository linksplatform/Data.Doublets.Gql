# @linksplatform/doublets-gql

TypeScript/JavaScript Doublets Adapter via GraphQL client for the Links Platform ecosystem.

This package provides a JavaScript/TypeScript client for interacting with Doublets (Links) data through a GraphQL API. It offers both a low-level GraphQL client and a high-level JavaScript-native API for easy integration.

## Features

- 🚀 **Native JavaScript API**: JavaScript-friendly interface with Promise-based operations
- 🔧 **GraphQL Client**: Low-level GraphQL client for advanced operations  
- 📝 **TypeScript Support**: Full TypeScript type definitions included
- 🧪 **Well Tested**: Comprehensive unit test coverage
- 🔄 **CRUD Operations**: Complete Create, Read, Update, Delete functionality
- 🔍 **Advanced Querying**: Filtering, sorting, pagination, and aggregation
- 🏗️ **Query Builder**: Fluent API for building complex queries
- ⚡ **Batch Operations**: Efficient bulk create/update/delete operations

## Installation

```bash
npm install @linksplatform/doublets-gql
```

## Quick Start

```typescript
import { DoubletsAdapter, createDoubletsClient } from '@linksplatform/doublets-gql';

// Create a client instance
const client = createDoubletsClient({
  endpoint: 'http://localhost:4000/graphql',
  headers: {
    'Authorization': 'Bearer your-token-here'
  }
});

// Basic operations
async function examples() {
  // Create a new link
  const link = await client.create(1, 2);
  console.log('Created link:', link);

  // Get or create (find existing or create new)
  const existingOrNew = await client.getOrCreate(1, 2);
  
  // Get a link by ID
  const retrieved = await client.get(link.id);
  
  // Update a link
  const updated = await client.update(link.id, 3, 4);
  
  // Delete a link
  const deleted = await client.delete(link.id);
  
  // Search for links
  const searchResults = await client.search({ from_id: 1 });
  
  // Get all outgoing links from a node
  const outgoing = await client.getOutgoing(1);
  
  // Get all incoming links to a node
  const incoming = await client.getIncoming(2);
}
```

## API Reference

### DoubletsAdapter

The main class providing a JavaScript-native API for Doublets operations.

#### Constructor

```typescript
new DoubletsAdapter(config: DoubletsClientConfig)
```

#### Basic CRUD Operations

```typescript
// Create a new link
create(from_id: number, to_id: number): Promise<Link>

// Get existing link or create new one
getOrCreate(from_id: number, to_id: number): Promise<Link>

// Update an existing link
update(id: number, from_id: number, to_id: number): Promise<Link>

// Delete a link by ID
delete(id: number): Promise<boolean>

// Get a link by ID
get(id: number): Promise<Link | null>

// Check if a link exists
exists(id: number): Promise<boolean>
```

#### Query Operations

```typescript
// Get all links with optional filtering/pagination
getAll(query?: LinksQueryArgs): Promise<Link[]>

// Search for links matching a pattern
search(pattern: Partial<Link>): Promise<Link[]>

// Count links matching a condition
count(where?: LinksBooleanExpression): Promise<number>
```

#### Relationship Operations

```typescript
// Get links where the specified node is the source
getOutgoing(from_id: number): Promise<Link[]>

// Get links where the specified node is the target
getIncoming(to_id: number): Promise<Link[]>

// Get all links connected to a node (both directions)
getConnected(id: number): Promise<Link[]>
```

#### Typed Links

```typescript
// Create a link with explicit type
createTyped(from_id: number, to_id: number, type_id: number): Promise<Link>

// Get or create a typed link
getOrCreateTyped(from_id: number, to_id: number, type_id: number): Promise<Link>

// Find links by type
findByType(type_id: number): Promise<Link[]>
```

#### Batch Operations

```typescript
// Create multiple links at once
createBatch(links: Array<{from_id: number, to_id: number, type_id?: number}>): Promise<Link[]>

// Delete multiple links by condition
deleteWhere(where: LinksBooleanExpression): Promise<number>

// Update multiple links by condition
updateWhere(where: LinksBooleanExpression, set: Partial<Link>): Promise<number>
```

### Query Builder

Build complex queries using the fluent WhereBuilder API:

```typescript
import { DoubletsAdapter } from '@linksplatform/doublets-gql';

// Simple conditions
const condition = DoubletsAdapter.where()
  .id(1)
  .fromId(2)
  .toId(3)
  .build();

// Complex conditions with logical operators
const complexCondition = DoubletsAdapter.where()
  .id(1)
  .and(
    DoubletsAdapter.where()
      .fromId(2)
      .or(DoubletsAdapter.where().toId(3))
  )
  .build();

// Range queries
const rangeCondition = DoubletsAdapter.where()
  .idGreaterThan(100)
  .idLessThan(200)
  .build();

// IN queries
const inCondition = DoubletsAdapter.where()
  .idIn([1, 2, 3, 4, 5])
  .build();

// Use with queries
const results = await client.getAll({
  where: condition,
  limit: 10,
  order_by: [{ id: 'asc' }]
});
```

### Advanced Usage

#### Using the Low-Level GraphQL Client

```typescript
import { DoubletsGraphQLClient } from '@linksplatform/doublets-gql';

const graphqlClient = new DoubletsGraphQLClient({
  endpoint: 'http://localhost:4000/graphql'
});

// Direct GraphQL operations
const links = await graphqlClient.queryLinks({
  where: { from_id: { _eq: 1 } },
  limit: 10
});

// Custom queries using the underlying GraphQL client
const customResult = await graphqlClient.getClient().request(`
  query CustomQuery($id: bigint!) {
    links_by_pk(id: $id) {
      id
      from_id
      to_id
      # Custom fields...
    }
  }
`, { id: 1 });
```

#### Error Handling

```typescript
try {
  const link = await client.create(1, 2);
} catch (error) {
  if (error.response?.errors) {
    console.error('GraphQL errors:', error.response.errors);
  } else {
    console.error('Network or other error:', error.message);
  }
}
```

#### Authentication

```typescript
// Set headers during initialization
const client = new DoubletsAdapter({
  endpoint: 'http://localhost:4000/graphql',
  headers: {
    'Authorization': 'Bearer token',
    'X-API-Key': 'api-key'
  }
});

// Update headers later
client.setAuth({
  'Authorization': 'Bearer new-token'
});
```

## Data Types

### Link

```typescript
interface Link {
  id: number;
  from_id: number;
  to_id: number;
  type_id?: number;
}
```

### Extended Link (with relationships)

```typescript
interface ExtendedLink extends Link {
  from?: Link;
  to?: Link;
  type?: Link;
  in?: Link[];
  out?: Link[];
}
```

### Query Arguments

```typescript
interface LinksQueryArgs {
  distinct_on?: string[];
  limit?: number;
  offset?: number;
  order_by?: LinksOrderBy[];
  where?: LinksBooleanExpression;
}
```

## Examples

### Basic CRUD Operations

```typescript
import { createDoubletsClient } from '@linksplatform/doublets-gql';

const client = createDoubletsClient({
  endpoint: 'http://localhost:4000/graphql'
});

async function crudExample() {
  // Create
  const newLink = await client.create(1, 2);
  console.log('Created:', newLink);
  
  // Read
  const foundLink = await client.get(newLink.id);
  console.log('Found:', foundLink);
  
  // Update  
  const updatedLink = await client.update(newLink.id, 3, 4);
  console.log('Updated:', updatedLink);
  
  // Delete
  const wasDeleted = await client.delete(newLink.id);
  console.log('Deleted:', wasDeleted);
}
```

### Graph Traversal

```typescript
async function traversalExample() {
  // Get all connections for a node
  const nodeId = 1;
  const connected = await client.getConnected(nodeId);
  
  // Get outgoing relationships (what this node points to)
  const outgoing = await client.getOutgoing(nodeId);
  
  // Get incoming relationships (what points to this node)
  const incoming = await client.getIncoming(nodeId);
  
  console.log(`Node ${nodeId} has ${connected.length} total connections`);
  console.log(`${outgoing.length} outgoing, ${incoming.length} incoming`);
}
```

### Complex Queries

```typescript
async function complexQueryExample() {
  // Find all links of a specific type with pagination
  const typedLinks = await client.getAll({
    where: {
      type_id: { _eq: 5 }
    },
    order_by: [{ id: 'desc' }],
    limit: 20,
    offset: 0
  });
  
  // Count links created after a certain ID
  const recentCount = await client.count({
    id: { _gt: 1000 }
  });
  
  // Complex condition using query builder
  const complexResults = await client.getAll({
    where: DoubletsAdapter.where()
      .typeId(1)
      .and(
        DoubletsAdapter.where()
          .fromId(2)
          .or(DoubletsAdapter.where().toId(3))
      )
      .build()
  });
}
```

### Batch Operations

```typescript
async function batchExample() {
  // Create multiple links at once
  const linksToCreate = [
    { from_id: 1, to_id: 2, type_id: 1 },
    { from_id: 2, to_id: 3, type_id: 1 },
    { from_id: 3, to_id: 4, type_id: 1 }
  ];
  
  const createdLinks = await client.createBatch(linksToCreate);
  console.log(`Created ${createdLinks.length} links`);
  
  // Update multiple links
  const updatedCount = await client.updateWhere(
    { type_id: { _eq: 1 } },
    { type_id: 2 }
  );
  console.log(`Updated ${updatedCount} links`);
  
  // Delete multiple links
  const deletedCount = await client.deleteWhere({
    id: { _in: createdLinks.map(link => link.id) }
  });
  console.log(`Deleted ${deletedCount} links`);
}
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Run the test suite: `npm test`
6. Submit a pull request

## License

MIT - see LICENSE file for details.

## Links Platform

This package is part of the [Links Platform](https://github.com/linksplatform) ecosystem. Links Platform is a project focused on developing a universal data structure and algorithms for efficient storage and manipulation of links/connections between objects.

For more information:
- [Main Repository](https://github.com/linksplatform/Data.Doublets.Gql)
- [Documentation](https://linksplatform.github.io/)
- [Links Platform Organization](https://github.com/linksplatform)