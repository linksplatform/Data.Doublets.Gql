# PHP Doublets Adapter via GraphQL Client

A PHP library that provides standard Doublets CRUD operations API in native PHP code style via GraphQL client. This adapter connects to the LinksPlatform Doublets GraphQL server and provides an idiomatic PHP interface for working with Links (Doublets).

## Features

- 🚀 **Native PHP API** - Clean, idiomatic PHP interface following modern PHP standards
- 🔄 **Full CRUD Operations** - Create, Read, Update, Delete operations for Links
- 🎯 **Query Builder** - Fluent interface for building complex queries
- 📦 **Collection Support** - Rich collection class with filtering, mapping, and iteration
- 🧪 **Fully Tested** - Comprehensive test suite with PHPUnit
- 🔧 **Easy Setup** - Simple factory methods for common configurations
- 🌐 **GraphQL Powered** - Uses reliable GraphQL client library underneath
- 🔀 **Swappable Architecture** - Designed to be replaceable with native library when available

## Installation

Install via Composer:

```bash
composer require linksplatform/data-doublets-gql-php
```

## Quick Start

### Basic Usage

```php
<?php
use LinksPlatform\Data\Doublets\Gql\DoubletsClientFactory;

// Connect to local development server
$client = DoubletsClientFactory::createLocal();

// Or connect to demo server
$client = DoubletsClientFactory::createDemo();

// Create a new link
$link = $client->create(fromId: 1, toId: 2);
echo "Created link: {$link}\n"; // Link(id: 3, from: 1, to: 2)

// Get a link by ID
$link = $client->get(3);
if ($link) {
    echo "Found link: {$link}\n";
}

// Update a link
$updatedLink = $client->update(id: 3, fromId: 1, toId: 5);
echo "Updated link: {$updatedLink}\n";

// Delete a link
$deleted = $client->delete(3);
echo $deleted ? "Link deleted\n" : "Link not found\n";
```

### Advanced Query Examples

```php
<?php
use LinksPlatform\Data\Doublets\Gql\DoubletsClientFactory;
use LinksPlatform\Data\Doublets\Gql\LinksQuery;

$client = DoubletsClientFactory::createLocal();

// Find links with complex criteria
$query = (new LinksQuery())
    ->whereFromId(1)
    ->whereToId(2)
    ->limit(10)
    ->offset(5)
    ->orderBy('id', 'desc');

$links = $client->find($query);

echo "Found {$links->count()} links:\n";
foreach ($links as $link) {
    echo "- {$link}\n";
}

// Get all links with pagination
$allLinks = $client->all(limit: 20, offset: 0);

// Count links
$totalCount = $client->count();
echo "Total links: {$totalCount}\n";

// Count with criteria
$filteredCount = $client->count($query);
echo "Filtered links: {$filteredCount}\n";
```

### Working with Collections

```php
<?php
$links = $client->all();

// Filter collections
$filtered = $links->filter(fn($link) => $link->getFromId() === 1);

// Find specific links
$link = $links->findById(123);
$fromLinks = $links->findByFromId(1);
$toLinks = $links->findByToId(2);

// Map to extract data
$ids = $links->map(fn($link) => $link->getId());

// Convert to array or JSON
$array = $links->toArray();
$json = json_encode($links); // Uses JsonSerializable
```

### Batch Operations

```php
<?php
// Create multiple links at once
$linksData = [
    ['from_id' => 1, 'to_id' => 2],
    ['from_id' => 2, 'to_id' => 3],
    ['from_id' => 3, 'to_id' => 4],
];

$createdLinks = $client->createMany($linksData);
echo "Created {$createdLinks->count()} links\n";

// Delete multiple links
$deleteQuery = (new LinksQuery())->whereFromId(1);
$deletedLinks = $client->deleteMany($deleteQuery);
echo "Deleted {$deletedLinks->count()} links\n";
```

### Custom Configuration

```php
<?php
use LinksPlatform\Data\Doublets\Gql\DoubletsClientFactory;

// Custom endpoint
$client = DoubletsClientFactory::create('http://custom-server:8080/v1/graphql');

// With authentication token
$client = DoubletsClientFactory::createWithToken(
    'http://secured-server/v1/graphql',
    'your-api-token-here'
);

// With custom headers
$client = DoubletsClientFactory::createWithHeaders(
    'http://custom-server/v1/graphql',
    [
        'Authorization' => 'Bearer your-token',
        'X-API-Key' => 'your-api-key',
        'X-Client-Version' => '1.0.0'
    ]
);
```

## API Reference

### DoubletsClient

Main client class implementing the `LinksInterface`.

#### Core Methods

- `create(int $fromId, int $toId): Link` - Create a new link
- `getOrCreate(int $fromId, int $toId): Link` - Get existing or create new link
- `get(int $id): ?Link` - Get link by ID
- `update(int $id, int $fromId, int $toId): Link` - Update existing link
- `delete(int $id): bool` - Delete link by ID
- `find(LinksQuery $query): LinksCollection` - Find links by criteria
- `all(?int $limit = null, ?int $offset = null): LinksCollection` - Get all links
- `count(?LinksQuery $query = null): int` - Count links

#### Batch Methods

- `createMany(array $links): LinksCollection` - Create multiple links
- `deleteMany(LinksQuery $query): LinksCollection` - Delete multiple links

### LinksQuery

Fluent query builder for constructing search criteria.

#### Methods

- `whereId(int $id): self` - Filter by link ID
- `whereFromId(int $fromId): self` - Filter by source ID
- `whereToId(int $toId): self` - Filter by target ID
- `limit(int $limit): self` - Limit results
- `offset(int $offset): self` - Skip results
- `orderBy(string $field, string $direction = 'asc'): self` - Order results
- `distinctOn(string $field): self` - Distinct values

### Link

Represents a single link with ID, from_id, and to_id.

#### Methods

- `getId(): int` - Get link ID
- `getFromId(): int` - Get source ID
- `getToId(): int` - Get target ID
- `toArray(): array` - Convert to array
- `toJson(): string` - Convert to JSON
- `__toString(): string` - String representation

### LinksCollection

Collection class for working with multiple links.

#### Methods

- `count(): int` - Count links
- `first(): ?Link` - Get first link
- `last(): ?Link` - Get last link
- `findById(int $id): ?Link` - Find by ID
- `findByFromId(int $fromId): LinksCollection` - Find by source ID
- `findByToId(int $toId): LinksCollection` - Find by target ID
- `filter(callable $callback): LinksCollection` - Filter links
- `map(callable $callback): array` - Map to array
- `toArray(): array` - Convert to array

## Requirements

- PHP 8.0 or higher
- Composer for dependency management
- Access to a LinksPlatform Doublets GraphQL server

## Development

### Running Tests

```bash
composer test
```

### Code Style

```bash
composer cs-check  # Check coding standards
composer cs-fix    # Fix coding standards
```

### Installing Dependencies

```bash
composer install
```

## Architecture

This library is designed with a clean architecture that separates concerns:

- **Core Layer**: Link, LinksCollection, LinksQuery - Domain models
- **Interface Layer**: LinksInterface - Contract definition  
- **GraphQL Layer**: GraphQLClient, QueryBuilder - GraphQL abstraction
- **Client Layer**: DoubletsClient - Main implementation
- **Factory Layer**: DoubletsClientFactory - Easy instantiation

The GraphQL client layer can be easily swapped out for a native C++ library implementation when it becomes available, without changing the public API.

## License

MIT License

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Links

- [LinksPlatform Documentation](https://github.com/LinksPlatform/Documentation)
- [Data.Doublets.Gql Repository](https://github.com/linksplatform/Data.Doublets.Gql)
- [Online GraphQL Demo](http://linksplatform.ddns.net:29018/ui/playground)