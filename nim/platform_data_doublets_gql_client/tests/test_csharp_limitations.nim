## Version-specific original C# storage regression, run LAST on disposable data.
import std/[unittest, os, strutils]
import platform_data_doublets_gql_client

suite "original C# b11f33b zero-target storage limitation":
  test "null insert result is reported without retries or fabricated success":
    doAssert getEnv("DOUBLETS_TEST_SERVER_KIND") == "csharp"
    let client = openGraphql(getEnv("DOUBLETS_GQL_URL"))
    defer: client.close()
    doAssert client.count() == 0, "Refusing to modify a nonempty test database"
    var failed = false
    try:
      discard client.create(1, 0)
    except GraphqlError as error:
      failed = true
      check error.msg.contains("Malformed GraphQL field: insert_links_one")
    check failed
    # The server can consume a native ID without exposing a readable row.
    # The harness discards this database immediately after the test.
    check client.all().len == 0
