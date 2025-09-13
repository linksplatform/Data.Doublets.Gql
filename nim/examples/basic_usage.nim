## Basic usage example for Platform.Data.Doublets Nim packages
## This demonstrates how to use all three packages

import platform_data_doublets_client

proc demonstrateNativeUsage() =
  echo "=== Native Client Usage ==="
  try:
    # Create native client
    let client = newNativeDoubletsClient("example.db")
    echo "Native client created successfully"
    
    # Example operations (would work with actual DLL)
    echo "Client type: ", client.getClientType()
    echo "Is native: ", client.isNative()
    
    # Close connection
    client.close()
    echo "Native client closed"
  except DoubletsException as e:
    echo "Native client error (expected without DLL): ", e.msg

proc demonstrateGraphQLUsage() =
  echo "\n=== GraphQL Client Usage ==="
  try:
    # Create GraphQL client  
    let client = newGraphQLDoubletsClient("http://localhost:60341/v1/graphql")
    echo "GraphQL client created successfully"
    
    # Example operations (would work with running server)
    echo "Client type: ", client.getClientType()
    echo "Is GraphQL: ", client.isGraphQL()
    
    # Close connection
    client.close() 
    echo "GraphQL client closed"
  except DoubletsException as e:
    echo "GraphQL client error (expected without server): ", e.msg

proc demonstrateUnifiedAPI() =
  echo "\n=== Unified API Usage ==="
  
  # This shows how the same code works with different backends
  proc useClient(client: IDoubletsClient, name: string) =
    echo "Using ", name, " client:"
    echo "  Client type: ", client.getClientType()
    echo "  Is native: ", client.isNative()  
    echo "  Is GraphQL: ", client.isGraphQL()
    
    # In real usage, you would do operations like:
    # let linkId = client.create(1, 2)
    # let count = client.count()
    # etc.
    
    client.close()
    echo "  Client closed"

  try:
    let nativeClient = newNativeDoubletsClient("example.db")
    useClient(nativeClient, "Native")
  except DoubletsException:
    echo "Native client not available (expected)"
  
  try:
    let gqlClient = newGraphQLDoubletsClient("http://localhost:60341/v1/graphql")
    useClient(gqlClient, "GraphQL")
  except DoubletsException:
    echo "GraphQL client not available (expected)"

proc demonstrateBatchOperations() =
  echo "\n=== Batch Operations Usage ==="
  
  # Example of batch operations (structure demonstration)
  let linksToCreate = @[(1.LinkIndex, 2.LinkIndex), (3.LinkIndex, 4.LinkIndex)]
  echo "Batch create input: ", linksToCreate
  
  let linkIds = @[1.LinkIndex, 2.LinkIndex, 3.LinkIndex]
  echo "Batch delete input: ", linkIds

proc demonstrateDataStructures() =
  echo "\n=== Data Structures ==="
  
  # Link structure
  let link = Link(id: 123, fromId: 456, toId: 789)
  echo "Link: id=", link.id, ", fromId=", link.fromId, ", toId=", link.toId
  
  # Configuration structures
  let nativeConfig = DoubletsClientConfig(
    clientType: Native,
    databasePath: "test.db"
  )
  echo "Native config: ", nativeConfig
  
  let gqlConfig = DoubletsClientConfig(
    clientType: GraphQL, 
    graphqlUrl: "http://localhost:60341/v1/graphql",
    headers: @[("Authorization", "Bearer token")]
  )
  echo "GraphQL config: ", gqlConfig

# Main demonstration
when isMainModule:
  echo "Platform.Data.Doublets Nim Packages Demo"
  echo "========================================"
  
  demonstrateDataStructures()
  demonstrateNativeUsage()
  demonstrateGraphQLUsage()
  demonstrateUnifiedAPI()
  demonstrateBatchOperations()
  
  echo "\nDemo completed!"