// Solution for issue #22
// Generated automatically by Baobot

// This is a placeholder for the actual solution script.
// The actual solution would require more detailed information about the issue and the specific requirements.
// For demonstration purposes, this script is designed to be as generic as possible.

const { ApolloServer, gql } = require('apollo-server');

// Define the schema and type definitions
const typeDefs = gql`
  scalar Int64
  type Query {
    doublets(word: String): String
  }
`;

// Define the resolver for the GraphQL query
const resolvers = {
  Query: {
    async doublets(_, { word }) {
      // Placeholder for the actual implementation using Native DLL
      return `Mock result for ${word}`;
    },
  },
};

// Create the ApolloServer instance
const server = new ApolloServer({ typeDefs, resolvers });

// Start the server
server.listen().then(({ url }) => {
  console.log(`Server ready at ${url}`);
});


This script sets up a basic Apollo GraphQL server with a single query `doublets` that returns a mock result. The actual implementation for the `doublets` operation would need to interface with the Nim Doublets library using a native DLL, which is not covered in this script. The Nim Doublets library would need to be integrated and its functionality exposed via the Node.js environment to complete the solution.