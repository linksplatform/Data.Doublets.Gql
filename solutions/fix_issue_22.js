// Solution for issue #22
// Generated automatically by Baobot

const { ApolloClient, InMemoryCache } = require('@apollo/client');
const doubletsAdapter = require('doublets-adapter');

// Initialize Apollo Client with the InMemoryCache
const client = new ApolloClient({
  cache: new InMemoryCache(),
  uri: 'https://your-graphql-endpoint.com/graphql' // Replace with your GraphQL endpoint
});

// Function to initialize the doublets adapter with Apollo Client
async function initializeDoubletsAdapter() {
  const doubletsAdapterInstance = doubletsAdapter(client);
  // Example: You can add Doublets functionality here
  // For example, you might want to call a specific Doublets query or mutation
  // doubletsAdapterInstance.doubletsQuery('YOUR_DOUBLETS_QUERY_VARIABLE');
  console.log('Doublets adapter initialized with Apollo Client');
}

initializeDoubletsAdapter();


This script sets up an `ApolloClient` with a simple in-memory cache and initializes the Nim Doublets Adapter with it. Replace `'https://your-graphql-endpoint.com/graphql'` with the actual GraphQL endpoint provided in the issue. The `initializeDoubletsAdapter` function is a placeholder where you would integrate the Doublets functionality according to the specific requirements of the issue.