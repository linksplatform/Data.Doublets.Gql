const { createDoubletsClient } = require('@linksplatform/doublets-gql');

// Create a client instance
const client = createDoubletsClient({
  endpoint: 'http://localhost:4000/graphql',
  headers: {
    // Add authentication headers if needed
    // 'Authorization': 'Bearer your-token-here'
  }
});

async function basicUsageExample() {
  try {
    console.log('=== Basic Doublets Operations Example ===\n');

    // 1. Create a new link
    console.log('1. Creating a new link...');
    const newLink = await client.create(1, 2);
    console.log('Created link:', newLink);

    // 2. Get the link we just created
    console.log('\n2. Retrieving the link...');
    const retrievedLink = await client.get(newLink.id);
    console.log('Retrieved link:', retrievedLink);

    // 3. Check if the link exists
    console.log('\n3. Checking if link exists...');
    const exists = await client.exists(newLink.id);
    console.log('Link exists:', exists);

    // 4. Update the link
    console.log('\n4. Updating the link...');
    const updatedLink = await client.update(newLink.id, 3, 4);
    console.log('Updated link:', updatedLink);

    // 5. Search for links
    console.log('\n5. Searching for links with from_id = 3...');
    const searchResults = await client.search({ from_id: 3 });
    console.log('Search results:', searchResults);

    // 6. Get or create (idempotent operation)
    console.log('\n6. Get or create link (3 -> 4)...');
    const getOrCreated = await client.getOrCreate(3, 4);
    console.log('Get or created link:', getOrCreated);

    // 7. Create multiple links
    console.log('\n7. Creating multiple links in batch...');
    const batchLinks = await client.createBatch([
      { from_id: 5, to_id: 6 },
      { from_id: 6, to_id: 7 },
      { from_id: 7, to_id: 8 }
    ]);
    console.log('Batch created links:', batchLinks);

    // 8. Get all links with pagination
    console.log('\n8. Getting all links (limited to 5)...');
    const allLinks = await client.getAll({ limit: 5 });
    console.log('All links (first 5):', allLinks);

    // 9. Count total links
    console.log('\n9. Counting total links...');
    const totalCount = await client.count();
    console.log('Total links count:', totalCount);

    // 10. Delete the original link
    console.log('\n10. Deleting the original link...');
    const deleted = await client.delete(newLink.id);
    console.log('Link deleted:', deleted);

    console.log('\n=== Example completed successfully! ===');

  } catch (error) {
    console.error('Error in basic usage example:', error);
    
    if (error.response?.errors) {
      console.error('GraphQL errors:', error.response.errors);
    }
  }
}

// Run the example
if (require.main === module) {
  basicUsageExample();
}