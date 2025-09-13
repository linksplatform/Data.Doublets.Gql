import { DoubletsAdapter, createDoubletsClient } from '@linksplatform/doublets-gql';

const client = createDoubletsClient({
  endpoint: 'http://localhost:4000/graphql'
});

async function advancedQueriesExample() {
  try {
    console.log('=== Advanced Queries Example ===\n');

    // 1. Create some sample data with types
    console.log('1. Creating sample data with types...');
    const sampleData = await client.createBatch([
      { from_id: 1, to_id: 2, type_id: 1 }, // Type 1 = "friendship"
      { from_id: 2, to_id: 3, type_id: 1 }, // Type 1 = "friendship"
      { from_id: 3, to_id: 4, type_id: 2 }, // Type 2 = "parent-child"
      { from_id: 4, to_id: 5, type_id: 2 }, // Type 2 = "parent-child"
      { from_id: 1, to_id: 6, type_id: 3 }, // Type 3 = "work relationship"
      { from_id: 6, to_id: 7, type_id: 3 }, // Type 3 = "work relationship"
      { from_id: 2, to_id: 8, type_id: 1 }, // Type 1 = "friendship"
      { from_id: 8, to_id: 9, type_id: 2 }, // Type 2 = "parent-child"
    ]);
    console.log(`Created ${sampleData.length} sample links`);

    // 2. Query by type using simple search
    console.log('\n2. Finding all friendship links (type_id = 1)...');
    const friendships = await client.findByType(1);
    console.log('Friendship links:', friendships);

    // 3. Complex query using WhereBuilder
    console.log('\n3. Complex query: Find links where from_id > 2 AND type_id = 2...');
    const complexQuery = DoubletsAdapter.where()
      .typeId(2)
      .and(
        DoubletsAdapter.where()
          .fromId(3)
          .or(DoubletsAdapter.where().fromId(4))
      )
      .build();

    const complexResults = await client.getAll({ where: complexQuery });
    console.log('Complex query results:', complexResults);

    // 4. Range queries
    console.log('\n4. Range query: Find links with IDs between specific values...');
    const minId = Math.min(...sampleData.map(link => link.id));
    const maxId = Math.max(...sampleData.map(link => link.id));
    const midId = Math.floor((minId + maxId) / 2);

    const rangeQuery = DoubletsAdapter.where()
      .idGreaterThan(midId)
      .build();

    const rangeResults = await client.getAll({ 
      where: rangeQuery,
      order_by: [{ id: 'asc' }]
    });
    console.log(`Links with ID > ${midId}:`, rangeResults);

    // 5. IN queries
    console.log('\n5. IN query: Find links where from_id is in a specific set...');
    const inQuery = DoubletsAdapter.where()
      .idIn(sampleData.slice(0, 3).map(link => link.id))
      .build();

    const inResults = await client.getAll({ where: inQuery });
    console.log('Links with IDs in set:', inResults);

    // 6. NOT queries
    console.log('\n6. NOT query: Find links that are NOT friendships...');
    const notQuery = DoubletsAdapter.where()
      .not(DoubletsAdapter.where().typeId(1))
      .build();

    const notResults = await client.getAll({ where: notQuery });
    console.log('Non-friendship links:', notResults);

    // 7. Pagination example
    console.log('\n7. Pagination example: Get links in pages...');
    const pageSize = 3;
    let page = 0;
    let hasMore = true;

    while (hasMore) {
      const pageResults = await client.getAll({
        limit: pageSize,
        offset: page * pageSize,
        order_by: [{ id: 'asc' }]
      });

      console.log(`Page ${page + 1}:`, pageResults.map(link => ({ id: link.id, from_id: link.from_id, to_id: link.to_id })));
      
      hasMore = pageResults.length === pageSize;
      page++;
      
      if (page > 5) break; // Safety limit for example
    }

    // 8. Aggregation queries
    console.log('\n8. Aggregation: Count links by type...');
    await countLinksByType(client);

    // 9. Find orphaned nodes (nodes with no incoming or outgoing links)
    console.log('\n9. Finding orphaned nodes...');
    const orphans = await findOrphanedNodes(client);
    console.log('Orphaned nodes:', orphans);

    // 10. Batch update based on condition
    console.log('\n10. Batch update: Change all type 3 links to type 4...');
    const updateCount = await client.updateWhere(
      { type_id: { _eq: 3 } },
      { type_id: 4 }
    );
    console.log(`Updated ${updateCount} links from type 3 to type 4`);

    // 11. Conditional delete
    console.log('\n11. Conditional delete: Remove all type 4 links...');
    const deleteCount = await client.deleteWhere({
      type_id: { _eq: 4 }
    });
    console.log(`Deleted ${deleteCount} type 4 links`);

    console.log('\n=== Advanced queries example completed! ===');

  } catch (error) {
    console.error('Error in advanced queries example:', error);
  }
}

/**
 * Count links by type
 */
async function countLinksByType(client: DoubletsAdapter) {
  const allLinks = await client.getAll();
  const typeCounts = new Map<number, number>();

  for (const link of allLinks) {
    const typeId = link.type_id || 0; // Use 0 for untyped links
    typeCounts.set(typeId, (typeCounts.get(typeId) || 0) + 1);
  }

  console.log('Links by type:');
  for (const [typeId, count] of typeCounts.entries()) {
    const typeName = typeId === 0 ? 'untyped' : `type ${typeId}`;
    console.log(`  ${typeName}: ${count} links`);
  }
}

/**
 * Find nodes that have no connections (neither incoming nor outgoing)
 */
async function findOrphanedNodes(client: DoubletsAdapter): Promise<number[]> {
  const allLinks = await client.getAll();
  const connectedNodes = new Set<number>();

  // Collect all connected nodes
  for (const link of allLinks) {
    connectedNodes.add(link.from_id);
    connectedNodes.add(link.to_id);
  }

  // For this example, we'll check a range of node IDs
  // In a real application, you might have a separate nodes table
  const orphans: number[] = [];
  const maxNodeId = Math.max(...Array.from(connectedNodes));
  
  for (let nodeId = 1; nodeId <= maxNodeId; nodeId++) {
    if (!connectedNodes.has(nodeId)) {
      orphans.push(nodeId);
    }
  }

  return orphans;
}

// Run the example
if (require.main === module) {
  advancedQueriesExample();
}