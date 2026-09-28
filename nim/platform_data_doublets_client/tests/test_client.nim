import std/[unittest, options, os, tempfiles]
import platform_data_doublets_client

proc main() =
  let mode = getEnv("DOUBLETS_TEST_BACKEND", "native")
  let directory = createTempDir("doublets-nim-unified-", "")
  defer: removeDir(directory)
  let client = if mode == "native": openNativeClient(directory / "test.links", getEnv("DOUBLETS_FFI_LIBRARY"))
               else: openGraphqlClient(getEnv("DOUBLETS_GQL_URL"))
  defer: client.close()

  suite "unified API contract - " & mode:
    test "same CRUD and iteration code works for either backend":
      doAssert client.count() == 0, "Refusing to modify a nonempty test database"
      check client.backend == (if mode == "native": nativeBackend else: graphqlBackend)
      let first = client.create(1, 1)
      let second = client.create(2, 2)
      check client.getOrCreate(1, 1) == first
      check client.exists(first.id)
      let changed = client.update(first.id, 1, 2).get
      check client.find(1, 2).get == changed
      check client.get(999).isNone
      check client.update(999, 3, 4).isNone
      check client.count() == 2
      var visited = 0
      client.each(proc(row: Link): bool =
        inc visited
        check client.get(row.id).isSome
        true)
      check visited == 2
      check client.delete(first.id)
      check client.delete(second.id)
      check not client.delete(999)
      check client.count() == 0
      let zero = client.create()
      check client.getOrCreate(0, 0) == zero
      check client.get(zero.id).get == zero
      check client.count() == 1
      let sourceZero = client.create(0, zero.id)
      check client.getOrCreate(0, zero.id) == sourceZero
      check client.find(0, 0).get == zero
      check client.find(0, zero.id).get == sourceZero
      expect ValueError: discard client.update(sourceZero.id, 0, 0)
      check client.count() == 2
      # Original C# has a separately tested storage defect for a zero target.
      if mode == "native" or getEnv("DOUBLETS_TEST_SERVER_KIND") != "csharp":
        let targetZero = client.create(zero.id, 0)
        check client.getOrCreate(zero.id, 0) == targetZero
        check client.find(zero.id, 0).get == targetZero
        expect ValueError: discard client.update(targetZero.id, 0, zero.id)
        check client.delete(targetZero.id)
      check client.delete(sourceZero.id)
      check client.delete(zero.id)
      check client.count() == 0

    test "close is idempotent and use after close fails":
      client.close()
      client.close()
      expect CatchableError: discard client.count()

main()
