## Tests for Platform.Data.Doublets.Gql.Client

import unittest, asyncdispatch
import ../src/platform_data_doublets_gql_client

# Note: These tests show the API structure and usage
# In real usage, you would need a running GraphQL server

suite "DoubletsGqlClient API Tests":
  
  test "DoubletsGqlClient creation and basic structure":
    let client = newDoubletsGqlClient("http://localhost:60341/v1/graphql")
    check(client.graphqlUrl == "http://localhost:60341/v1/graphql")
    check(not client.httpClient.isNil)
    check(not client.asyncHttpClient.isNil)
    client.close()

  test "Link structure validation":
    let link = Link(id: 1, fromId: 2, toId: 3)
    check(link.id == 1)
    check(link.fromId == 2)
    check(link.toId == 3)

  test "LinkIndex type validation":
    let index: LinkIndex = 12345
    check(index == 12345'u64)

  test "GraphQL query building validation":
    # Test that we can create clients and basic structures
    # The actual query builders are private implementation details
    let client = newDoubletsGqlClient("http://localhost:60341/v1/graphql")
    
    # Test that we have the expected GraphQL URL
    check(client.graphqlUrl.contains("graphql"))
    
    client.close()

# Async tests would require a running server
# suite "DoubletsGqlClient Async Tests":
#   test "Async operations":
#     proc testAsync() {.async.} =
#       let client = newDoubletsGqlClient("http://localhost:60341/v1/graphql")
#       try:
#         let linkId = await client.createAsync(1, 2)
#         check(linkId > 0)
#       except DoubletsGqlException:
#         # Expected when server is not available
#         discard
#       finally:
#         client.close()
#     
#     waitFor testAsync()