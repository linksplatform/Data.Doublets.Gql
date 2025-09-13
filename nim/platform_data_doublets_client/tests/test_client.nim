## Tests for Platform.Data.Doublets.Client

import unittest
import ../src/platform_data_doublets_client

suite "DoubletsClient Abstract API Tests":
  
  test "Link structure validation":
    let link = Link(id: 1, fromId: 2, toId: 3)
    check(link.id == 1)
    check(link.fromId == 2)
    check(link.toId == 3)

  test "LinkIndex type validation":
    let index: LinkIndex = 12345
    check(index == 12345'u64)

  test "DoubletsClientConfig structure":
    let nativeConfig = DoubletsClientConfig(
      clientType: Native,
      databasePath: "test.db"
    )
    check(nativeConfig.clientType == Native)
    check(nativeConfig.databasePath == "test.db")
    
    let gqlConfig = DoubletsClientConfig(
      clientType: GraphQL,
      graphqlUrl: "http://localhost:60341/v1/graphql",
      headers: @[("Authorization", "Bearer token")]
    )
    check(gqlConfig.clientType == GraphQL)
    check(gqlConfig.graphqlUrl == "http://localhost:60341/v1/graphql")
    check(gqlConfig.headers.len == 1)

  test "Client type enumeration":
    check(Native != GraphQL)
    
  test "Batch operations helpers":
    # Test the helper functions structure
    # In real usage, these would work with actual client instances
    let links = @[(1.LinkIndex, 2.LinkIndex), (3.LinkIndex, 4.LinkIndex)]
    check(links.len == 2)
    check(links[0] == (1.LinkIndex, 2.LinkIndex))
    
    let linkIds = @[1.LinkIndex, 2.LinkIndex, 3.LinkIndex]
    check(linkIds.len == 3)

# Note: Tests for actual client operations would require either:
# 1. Mock implementations of the abstract methods, or  
# 2. The actual native/GraphQL packages to be available
# 
# Example of how tests would look with real implementations:
# 
# suite "DoubletsClient Integration Tests":
#   test "Native client integration":
#     try:
#       let client = newNativeDoubletsClient("test.db")
#       check(client.isNative())
#       check(client.getClientType() == Native)
#       # Additional operations...
#       client.close()
#     except DoubletsException:
#       # Expected when native library is not available
#       skip()
#   
#   test "GraphQL client integration":
#     try:
#       let client = newGraphQLDoubletsClient("http://localhost:60341/v1/graphql")
#       check(client.isGraphQL())
#       check(client.getClientType() == GraphQL)
#       # Additional operations...
#       client.close()
#     except DoubletsException:
#       # Expected when GraphQL server is not available
#       skip()