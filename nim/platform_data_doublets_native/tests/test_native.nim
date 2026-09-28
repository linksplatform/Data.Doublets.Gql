import std/[unittest, options, os, tempfiles]
import platform_data_doublets_native

let library = getEnv("DOUBLETS_FFI_LIBRARY")
doAssert library.len > 0, "Set DOUBLETS_FFI_LIBRARY to the real FFI 0.9.0 library"

suite "real native Doublets FFI":
  setup:
    let directory = createTempDir("doublets-nim-native-", "")
    let database = directory / "test.links"
    var client = openNative(database, library)
  teardown:
    client.close()
    removeDir(directory)

  test "CRUD, pair identity, count and persistence":
    check client.count() == 0
    let row = client.create(1, 1)
    check row == NativeLink(id: 1, source: 1, target: 1)
    check client.get(1).get == row
    check client.getOrCreate(1, 1) == row
    check client.count() == 1
    let changed = client.update(1, 1, 0).get
    check changed.source == 1 and changed.target == 0
    client.close()
    client = openNative(database, library)
    check client.get(1).get == changed
    check client.delete(1)
    check client.get(1).isNone
    check client.count() == 0
    check not client.delete(1)

  test "zero pair, missing IDs and duplicate update are safe":
    let zero = client.create()
    check client.create() == zero
    let other = client.create(2, 2)
    check client.update(999, 3, 3).isNone
    check client.get(999).isNone
    check client.count() == 2
    expect ValueError: discard client.update(other.id, 0, 0)
    check client.get(other.id).get == other
    expect ValueError: discard client.create(9223372036854775804'u64, 0)
    check client.count() == 2

  test "snapshot callbacks allow reentry and stop early":
    discard client.create(1, 1)
    discard client.create(2, 2)
    var visited = 0
    client.each(proc(row: NativeLink): bool =
      inc visited
      check client.exists(row.id)
      discard client.create(3, 3)
      false)
    check visited == 1
    check client.count() == 3

  test "closed clients and invalid library paths raise errors":
    client.close()
    client.close()
    expect NativeError: discard client.all()
    expect NativeError: discard openNative(database, directory / "missing-library")
    expect ValueError: discard openNative("bad\0path", library)
