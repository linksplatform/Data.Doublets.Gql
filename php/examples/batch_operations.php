<?php

require_once __DIR__ . '/../vendor/autoload.php';

use LinksPlatform\Data\Doublets\Gql\DoubletsClientFactory;
use LinksPlatform\Data\Doublets\Gql\LinksQuery;

echo "=== PHP Doublets Client - Batch Operations Example ===\n\n";

try {
    // Create client
    echo "Connecting to Doublets GraphQL server...\n";
    $client = DoubletsClientFactory::createLocal();
    echo "Connected to: " . $client->getEndpoint() . "\n\n";
    
    // Example 1: Batch create operations
    echo "1. Creating multiple links in batch...\n";
    $linksToCreate = [
        ['from_id' => 10, 'to_id' => 20],
        ['from_id' => 20, 'to_id' => 30],
        ['from_id' => 30, 'to_id' => 40],
        ['from_id' => 10, 'to_id' => 30],  // Alternative path
        ['from_id' => 10, 'to_id' => 40],  // Direct connection
    ];
    
    $createdLinks = $client->createMany($linksToCreate);
    echo "Created {$createdLinks->count()} links:\n";
    foreach ($createdLinks as $link) {
        echo "  - {$link}\n";
    }
    echo "\n";
    
    // Example 2: Query the created links
    echo "2. Querying created links...\n";
    $query = (new LinksQuery())
        ->whereFromId(10)
        ->orderBy('to_id', 'asc');
    
    $fromNode10 = $client->find($query);
    echo "Links from node 10: {$fromNode10->count()}\n";
    foreach ($fromNode10 as $link) {
        echo "  - {$link}\n";
    }
    echo "\n";
    
    // Example 3: Working with the collection
    echo "3. Collection analysis...\n";
    $allCreated = $client->find((new LinksQuery())->whereFromId(10));
    
    // Find links going to specific targets
    $toNode30 = $allCreated->findByToId(30);
    $toNode40 = $allCreated->findByToId(40);
    
    echo "Direct connections from 10:\n";
    echo "  - To node 30: {$toNode30->count()} links\n";
    echo "  - To node 40: {$toNode40->count()} links\n";
    
    // Map to show all target nodes
    $targets = $allCreated->map(fn($link) => $link->getToId());
    echo "  - All targets: " . implode(', ', $targets) . "\n\n";
    
    // Example 4: Advanced querying
    echo "4. Advanced querying with multiple criteria...\n";
    
    // Find all intermediate nodes (nodes that are both source and target)
    $allLinks = $client->all();
    $fromIds = array_unique($allLinks->map(fn($link) => $link->getFromId()));
    $toIds = array_unique($allLinks->map(fn($link) => $link->getToId()));
    $intermediateNodes = array_intersect($fromIds, $toIds);
    
    echo "Intermediate nodes (both source and target): " . implode(', ', $intermediateNodes) . "\n";
    
    // Find paths through intermediate nodes
    foreach ($intermediateNodes as $intermediate) {
        $incoming = $allLinks->findByToId($intermediate);
        $outgoing = $allLinks->findByFromId($intermediate);
        
        if ($incoming->count() > 0 && $outgoing->count() > 0) {
            echo "Node {$intermediate} has {$incoming->count()} incoming and {$outgoing->count()} outgoing links\n";
        }
    }
    echo "\n";
    
    // Example 5: Batch operations with filtering
    echo "5. Working with filtered collections...\n";
    
    // Get all our test links
    $testLinks = $client->all()->filter(function($link) {
        return in_array($link->getFromId(), [10, 20, 30]) && 
               in_array($link->getToId(), [20, 30, 40]);
    });
    
    echo "Test links found: {$testLinks->count()}\n";
    
    // Group by from_id
    $groupedBySource = [];
    foreach ($testLinks as $link) {
        $fromId = $link->getFromId();
        if (!isset($groupedBySource[$fromId])) {
            $groupedBySource[$fromId] = [];
        }
        $groupedBySource[$fromId][] = $link;
    }
    
    echo "Grouped by source:\n";
    foreach ($groupedBySource as $fromId => $links) {
        $targets = array_map(fn($link) => $link->getToId(), $links);
        echo "  - From {$fromId}: " . implode(', ', $targets) . "\n";
    }
    echo "\n";
    
    // Example 6: Batch cleanup
    echo "6. Batch cleanup - deleting test links...\n";
    
    // Delete all links where from_id is 10
    $deleteQuery = (new LinksQuery())->whereFromId(10);
    $deletedFromNode10 = $client->deleteMany($deleteQuery);
    echo "Deleted {$deletedFromNode10->count()} links from node 10:\n";
    foreach ($deletedFromNode10 as $link) {
        echo "  - Deleted: {$link}\n";
    }
    
    // Delete remaining test links
    $deleteQuery2 = (new LinksQuery())->whereFromId(20);
    $deletedFromNode20 = $client->deleteMany($deleteQuery2);
    
    $deleteQuery3 = (new LinksQuery())->whereFromId(30);
    $deletedFromNode30 = $client->deleteMany($deleteQuery3);
    
    $totalDeleted = $deletedFromNode10->count() + $deletedFromNode20->count() + $deletedFromNode30->count();
    echo "\nTotal cleanup: Deleted {$totalDeleted} test links\n";
    
    // Verify cleanup
    echo "\n7. Verification - checking remaining links...\n";
    $remainingTestLinks = $client->all()->filter(function($link) {
        return in_array($link->getFromId(), [10, 20, 30]) && 
               in_array($link->getToId(), [20, 30, 40]);
    });
    
    echo "Remaining test links: {$remainingTestLinks->count()}\n";
    if ($remainingTestLinks->count() === 0) {
        echo "✅ Cleanup successful - no test links remaining\n";
    } else {
        echo "⚠️  Some test links still exist:\n";
        foreach ($remainingTestLinks as $link) {
            echo "  - {$link}\n";
        }
    }
    
} catch (Exception $e) {
    echo "Error: " . $e->getMessage() . "\n";
    echo "Make sure the Doublets GraphQL server is running!\n";
    echo "Start it with: cd csharp/Platform.Data.Doublets.Gql.Server && dotnet run\n";
}