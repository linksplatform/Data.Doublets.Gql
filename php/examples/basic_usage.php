<?php

require_once __DIR__ . '/../vendor/autoload.php';

use LinksPlatform\Data\Doublets\Gql\DoubletsClientFactory;
use LinksPlatform\Data\Doublets\Gql\LinksQuery;

echo "=== PHP Doublets Client - Basic Usage Example ===\n\n";

try {
    // Create client for local development
    echo "Connecting to local Doublets GraphQL server...\n";
    $client = DoubletsClientFactory::createLocal();
    
    // Alternative: Connect to demo server
    // $client = DoubletsClientFactory::createDemo();
    
    echo "Connected to: " . $client->getEndpoint() . "\n\n";
    
    // Example 1: Create a new link
    echo "1. Creating a new link...\n";
    $newLink = $client->create(fromId: 1, toId: 2);
    echo "Created: {$newLink}\n\n";
    
    // Example 2: Get link by ID
    echo "2. Getting link by ID...\n";
    $foundLink = $client->get($newLink->getId());
    if ($foundLink) {
        echo "Found: {$foundLink}\n\n";
    }
    
    // Example 3: Get or create (idempotent operation)
    echo "3. Get or create operation...\n";
    $sameLink = $client->getOrCreate(1, 2);
    echo "Got existing: {$sameLink}\n\n";
    
    // Example 4: Update a link
    echo "4. Updating the link...\n";
    $updatedLink = $client->update($newLink->getId(), fromId: 1, toId: 5);
    echo "Updated: {$updatedLink}\n\n";
    
    // Example 5: Create more links for querying
    echo "5. Creating more links for demonstration...\n";
    $link2 = $client->create(fromId: 2, toId: 3);
    $link3 = $client->create(fromId: 1, toId: 3);
    echo "Created: {$link2}\n";
    echo "Created: {$link3}\n\n";
    
    // Example 6: Query with criteria
    echo "6. Querying links with criteria...\n";
    $query = (new LinksQuery())
        ->whereFromId(1)
        ->limit(10)
        ->orderBy('id', 'desc');
    
    $results = $client->find($query);
    echo "Found {$results->count()} links with from_id = 1:\n";
    foreach ($results as $link) {
        echo "  - {$link}\n";
    }
    echo "\n";
    
    // Example 7: Get all links with pagination
    echo "7. Getting all links (limited)...\n";
    $allLinks = $client->all(limit: 5);
    echo "Total links (first 5): {$allLinks->count()}\n";
    foreach ($allLinks as $link) {
        echo "  - {$link}\n";
    }
    echo "\n";
    
    // Example 8: Count operations
    echo "8. Counting links...\n";
    $totalCount = $client->count();
    echo "Total links in database: {$totalCount}\n";
    
    $filteredCount = $client->count($query);
    echo "Links with from_id = 1: {$filteredCount}\n\n";
    
    // Example 9: Collection operations
    echo "9. Working with collections...\n";
    $collection = $client->all(limit: 10);
    
    // Filter collection
    $filtered = $collection->filter(fn($link) => $link->getFromId() === 1);
    echo "Filtered collection size: {$filtered->count()}\n";
    
    // Map to get just IDs
    $ids = $collection->map(fn($link) => $link->getId());
    echo "All IDs: " . implode(', ', $ids) . "\n";
    
    // Find specific links
    $byFromId = $collection->findByFromId(1);
    echo "Links from node 1: {$byFromId->count()}\n\n";
    
    // Example 10: Clean up - delete created links
    echo "10. Cleaning up - deleting created links...\n";
    $deleted1 = $client->delete($newLink->getId());
    $deleted2 = $client->delete($link2->getId());
    $deleted3 = $client->delete($link3->getId());
    
    echo "Cleanup completed. Deleted: " . ($deleted1 + $deleted2 + $deleted3) . " links\n";
    
} catch (Exception $e) {
    echo "Error: " . $e->getMessage() . "\n";
    echo "Make sure the Doublets GraphQL server is running!\n";
    echo "Start it with: cd csharp/Platform.Data.Doublets.Gql.Server && dotnet run\n";
}