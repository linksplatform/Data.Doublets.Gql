import { DoubletsAdapter, createDoubletsClient } from '@linksplatform/doublets-gql';

const client = createDoubletsClient({
  endpoint: 'http://localhost:4000/graphql'
});

async function graphTraversalExample() {
  try {
    console.log('=== Graph Traversal Example ===\n');

    // Create a small graph structure
    console.log('1. Creating a graph structure...');
    
    // Create nodes: 1 -> 2 -> 3 -> 4
    //               1 -> 5 -> 6
    //               2 -> 7
    const links = await client.createBatch([
      { from_id: 1, to_id: 2 }, // 1 points to 2
      { from_id: 2, to_id: 3 }, // 2 points to 3  
      { from_id: 3, to_id: 4 }, // 3 points to 4
      { from_id: 1, to_id: 5 }, // 1 also points to 5
      { from_id: 5, to_id: 6 }, // 5 points to 6
      { from_id: 2, to_id: 7 }, // 2 also points to 7
    ]);
    
    console.log('Created graph links:', links);

    // 2. Explore outgoing connections from node 1
    console.log('\n2. Getting outgoing connections from node 1...');
    const outgoingFrom1 = await client.getOutgoing(1);
    console.log('Node 1 points to:', outgoingFrom1.map(link => link.to_id));

    // 3. Explore incoming connections to node 2
    console.log('\n3. Getting incoming connections to node 2...');
    const incomingTo2 = await client.getIncoming(2);
    console.log('Nodes pointing to 2:', incomingTo2.map(link => link.from_id));

    // 4. Get all connections for node 2 (both directions)
    console.log('\n4. Getting all connections for node 2...');
    const allConnections2 = await client.getConnected(2);
    console.log('Node 2 is connected to:', allConnections2);

    // 5. Traverse the graph: find all nodes reachable from node 1
    console.log('\n5. Finding all nodes reachable from node 1...');
    const reachableNodes = await findReachableNodes(client, 1);
    console.log('Nodes reachable from 1:', reachableNodes);

    // 6. Find shortest path between two nodes
    console.log('\n6. Finding path from node 1 to node 4...');
    const path = await findPath(client, 1, 4);
    console.log('Path from 1 to 4:', path);

    // 7. Analyze graph statistics
    console.log('\n7. Graph statistics...');
    await analyzeGraph(client);

    console.log('\n=== Graph traversal example completed! ===');

  } catch (error) {
    console.error('Error in graph traversal example:', error);
  }
}

/**
 * Find all nodes reachable from a starting node using BFS
 */
async function findReachableNodes(client: DoubletsAdapter, startNode: number): Promise<number[]> {
  const visited = new Set<number>();
  const queue = [startNode];
  const reachable: number[] = [];

  while (queue.length > 0) {
    const currentNode = queue.shift()!;
    
    if (visited.has(currentNode)) {
      continue;
    }
    
    visited.add(currentNode);
    reachable.push(currentNode);

    // Get all outgoing connections
    const outgoing = await client.getOutgoing(currentNode);
    
    for (const link of outgoing) {
      if (!visited.has(link.to_id)) {
        queue.push(link.to_id);
      }
    }
  }

  return reachable.slice(1); // Remove the starting node
}

/**
 * Find shortest path between two nodes using BFS
 */
async function findPath(client: DoubletsAdapter, start: number, end: number): Promise<number[]> {
  if (start === end) {
    return [start];
  }

  const visited = new Set<number>();
  const queue: Array<{node: number, path: number[]}> = [{node: start, path: [start]}];

  while (queue.length > 0) {
    const { node: currentNode, path } = queue.shift()!;
    
    if (visited.has(currentNode)) {
      continue;
    }
    
    visited.add(currentNode);

    // Get all outgoing connections
    const outgoing = await client.getOutgoing(currentNode);
    
    for (const link of outgoing) {
      const nextNode = link.to_id;
      const newPath = [...path, nextNode];
      
      if (nextNode === end) {
        return newPath;
      }
      
      if (!visited.has(nextNode)) {
        queue.push({node: nextNode, path: newPath});
      }
    }
  }

  return []; // No path found
}

/**
 * Analyze basic graph statistics
 */
async function analyzeGraph(client: DoubletsAdapter) {
  // Count total links
  const totalLinks = await client.count();
  console.log(`Total links in graph: ${totalLinks}`);

  // Find nodes with most outgoing connections
  const nodeStats = new Map<number, {outgoing: number, incoming: number}>();
  const allLinks = await client.getAll();

  for (const link of allLinks) {
    // Count outgoing for from_id
    if (!nodeStats.has(link.from_id)) {
      nodeStats.set(link.from_id, {outgoing: 0, incoming: 0});
    }
    nodeStats.get(link.from_id)!.outgoing++;

    // Count incoming for to_id
    if (!nodeStats.has(link.to_id)) {
      nodeStats.set(link.to_id, {outgoing: 0, incoming: 0});
    }
    nodeStats.get(link.to_id)!.incoming++;
  }

  // Find most connected nodes
  const sortedByOutgoing = Array.from(nodeStats.entries())
    .sort((a, b) => b[1].outgoing - a[1].outgoing);
  
  const sortedByIncoming = Array.from(nodeStats.entries())
    .sort((a, b) => b[1].incoming - a[1].incoming);

  console.log('Top nodes by outgoing connections:');
  sortedByOutgoing.slice(0, 3).forEach(([node, stats]) => {
    console.log(`  Node ${node}: ${stats.outgoing} outgoing`);
  });

  console.log('Top nodes by incoming connections:');
  sortedByIncoming.slice(0, 3).forEach(([node, stats]) => {
    console.log(`  Node ${node}: ${stats.incoming} incoming`);
  });
}

// Run the example
if (require.main === module) {
  graphTraversalExample();
}