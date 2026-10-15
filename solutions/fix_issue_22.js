// Solution for issue #22
// Generated automatically by Baobot

const express = require('express');
const { ApolloServer, gql } = require('apollo-server-express');
const fs = require('fs');
const path = require('path');

// Define the GraphQL schema
const typeDefs = gql`
  type Query {
    doublets(word: String): [String]
  }
`;

// Define the resolvers
const resolvers = {
  Query: {
    doublets: async (root, { word }, { doubletsAdapter }) => {
      const doubletsResult = await doubletsAdapter.findDoublets(word);
      return doubletsResult;
    }
  }
};

// Load the DLL and get the doublets adapter function
const doubletsAdapterPath = path.join(__dirname, 'lib', 'doublets_adapter.dll');
const doubletsAdapter = require(doubletsAdapterPath).default;

// Initialize ApolloServer
const app = express();
const server = new ApolloServer({ typeDefs, resolvers });

server.applyMiddleware({ app });

// Start the server
const port = 4000;
app.listen({ port }, () => {
  console.log(`Server ready at http://localhost:${port}${server.graphqlPath}`);
});

// Example of how to use the adapter
const getDoublets = async (word) => {
  const result = await server.executeOperation({
    query: gql`
      query DoubletsQuery($word: String!) {
        doublets(word: $word)
      }
    `,
    variables: { word }
  });
  return result.data.doublets;
};

// Example usage
(async () => {
  const word = 'hello';
  const doublets = await getDoublets(word);
  console.log(`Doublets for '${word}':`, doublets);
})();


This script sets up a simple GraphQL server with a resolver for the `doublets` operation, which is supposed to interact with a native DLL for the doublets algorithm. The `doublets_adapter.dll` is expected to be located in the `lib` directory. Adjust the path and the DLL contents as necessary for your specific use case.