import std/[unittest, options, os, json, httpclient]
import platform_data_doublets_gql_client

let endpoint = getEnv("DOUBLETS_GQL_URL")
let fixtures = getEnv("DOUBLETS_FIXTURE_URL")
doAssert endpoint.len > 0 and fixtures.len > 0, "Run with a disposable GraphQL server and local HTTP fixtures"

suite "real GraphQL integration":
  test "standalone client performs complete CRUD through documented core fields":
    let client = openGraphql(endpoint)
    defer: client.close()
    doAssert client.count() == 0, "Refusing to modify a nonempty test database"
    let first = client.create(1, 1)
    let second = client.create(2, 2)
    check client.get(first.id).get == first
    check client.create(1, 1) == first
    check client.count() == 2
    let changed = client.update(first.id, 1, 2).get
    check changed.id == first.id and changed.source == 1 and changed.target == 2
    check client.find(1, 2).get == changed
    expect ValueError: discard client.update(first.id, 2, 2)
    check client.update(999, 1, 1).isNone
    var visited = 0
    client.each(proc(row: GqlLink): bool =
      inc visited
      check client.exists(row.id)
      false)
    check visited == 1
    check client.delete(first.id)
    check client.delete(second.id)
    check not client.delete(999)
    check client.count() == 0

  test "raw query variables and closed/range validation":
    let client = openGraphql(endpoint)
    let data = client.execute("query($offset:Int!){links(offset:$offset){id}}", %* {"offset": 0})
    check data["links"].len == 0
    expect ValueError: discard client.create(high(uint64), 0)
    expect ValueError: discard client.execute("{links{id}}", %* [1, 2])
    client.close()
    client.close()
    expect GraphqlError: discard client.all()
    expect ValueError: discard openGraphql("not-an-endpoint")
    expect ValueError: discard openGraphql(endpoint, 0)

suite "local protocol error fixtures":
  test "a response-body timeout is a GraphqlError":
    let client = openGraphql(fixtures & "/slow-body", timeoutMs = 50)
    defer: client.close()
    expect GraphqlError: discard client.get(1)

  test "HTTP, malformed JSON, GraphQL partial errors and malformed link data raise":
    for route in ["http-error", "invalid-json", "graphql-error", "missing-data", "wrong-id", "negative-address", "wrong-shape"]:
      let client = openGraphql(fixtures & "/" & route)
      defer: client.close()
      expect GraphqlError: discard client.get(1)

  test "caller headers and request variables are preserved":
    let customHeaders = newHttpHeaders()
    customHeaders["X-Test"] = "example-header"
    let client = openGraphql(fixtures & "/echo", headers = customHeaders)
    defer: client.close()
    let data = client.execute("query { echo }", %* {"sample": 42})
    check data["header"].getStr == "example-header"
    check data["variables"]["sample"].getInt == 42
