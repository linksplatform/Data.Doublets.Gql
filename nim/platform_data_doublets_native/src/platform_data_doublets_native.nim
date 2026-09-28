## Checked UInt64 bindings to Platform.Data.Doublets.FFI 0.9.0.
## A client belongs to one thread. Always close it, preferably using defer.
import std/[dynlib, options]

type
  NativeError* = object of CatchableError
  NativeLink* {.bycopy.} = object
    id*, source*, target*: uint64
  NativeConstants {.bycopy.} = object
    indexPart, sourcePart, targetPart, nullValue: uint64
    continueValue, breakValue, skipValue, anyValue, itselfValue, errorValue: uint64
    internalMin, internalMax, externalMin, externalMax: uint64
    hasExternal: bool
  ReadCallback = proc(link: NativeLink): uint64 {.cdecl, raises: [].}
  WriteCallback = proc(before, after: NativeLink): uint64 {.cdecl, raises: [].}
  NewFn = proc(path: cstring): pointer {.cdecl.}
  DropFn = proc(handle: pointer) {.cdecl.}
  ConstantsFn = proc(handle: pointer): NativeConstants {.cdecl.}
  EachFn = proc(handle: pointer; query: ptr uint64; length: uint;
                callback: ReadCallback): uint64 {.cdecl.}
  CountFn = proc(handle: pointer; query: ptr uint64; length: uint): uint64 {.cdecl.}
  CreateFn = proc(handle: pointer; query: ptr uint64; length: uint;
                  callback: WriteCallback): uint64 {.cdecl.}
  UpdateFn = proc(handle: pointer; query: ptr uint64; queryLength: uint;
                  replacement: ptr uint64; replacementLength: uint;
                  callback: WriteCallback): uint64 {.cdecl.}
  NativeClient* = ref object
    library: LibHandle
    handle: pointer
    constants: NativeConstants
    dropFn: DropFn
    eachFn: EachFn
    countFn: CountFn
    createFn, deleteFn: CreateFn
    updateFn: UpdateFn
    busy: bool
  CallbackState = object
    rows: seq[NativeLink]
    continuation: uint64

var currentState {.threadvar.}: ptr CallbackState

proc readCallback(link: NativeLink): uint64 {.cdecl, raises: [].} =
  currentState[].rows.add(link)
  currentState[].continuation

proc writeCallback(before, after: NativeLink): uint64 {.cdecl, raises: [].} =
  currentState[].rows.add(after)
  currentState[].continuation

proc symbol[T](library: LibHandle; name: string): T =
  let address = library.symAddr(name)
  if address == nil:
    raise newException(NativeError, "Missing native symbol: " & name)
  cast[T](address)

proc close*(client: NativeClient) =
  ## Idempotent. Never use a client concurrently or close during an operation.
  if client == nil: return
  if client.busy: raise newException(NativeError, "Native client is busy")
  if client.handle != nil:
    client.dropFn(client.handle)
    client.handle = nil
  if client.library != nil:
    unloadLib(client.library)
    client.library = nil

proc defaultLibraryPath*(): string =
  when defined(windows): "Platform.Doublets.dll"
  elif defined(macosx): "libPlatform.Doublets.dylib"
  else: "libPlatform.Doublets.so"

proc openNative*(databasePath: string;
                 libraryPath = defaultLibraryPath()): NativeClient =
  if databasePath.len == 0 or '\0' in databasePath or '\0' in libraryPath:
    raise newException(ValueError, "Database and library paths must be valid C strings")
  result = NativeClient(library: loadLib(libraryPath))
  if result.library == nil:
    raise newException(NativeError, "Cannot load Doublets library: " & libraryPath)
  try:
    let newFn = symbol[NewFn](result.library, "UInt64UnitedMemoryLinks_New")
    result.dropFn = symbol[DropFn](result.library, "UInt64UnitedMemoryLinks_Drop")
    let constantsFn = symbol[ConstantsFn](result.library, "UInt64UnitedMemoryLinks_GetConstants")
    result.eachFn = symbol[EachFn](result.library, "UInt64UnitedMemoryLinks_Each")
    result.countFn = symbol[CountFn](result.library, "UInt64UnitedMemoryLinks_Count")
    result.createFn = symbol[CreateFn](result.library, "UInt64UnitedMemoryLinks_Create")
    result.updateFn = symbol[UpdateFn](result.library, "UInt64UnitedMemoryLinks_Update")
    result.deleteFn = symbol[CreateFn](result.library, "UInt64UnitedMemoryLinks_Delete")
    result.handle = newFn(databasePath.cstring)
    if result.handle == nil: raise newException(NativeError, "Cannot open Doublets database")
    result.constants = constantsFn(result.handle)
    if result.constants.indexPart != 0 or result.constants.sourcePart != 1 or
        result.constants.targetPart != 2 or result.constants.nullValue != 0 or
        result.constants.internalMin != 1:
      raise newException(NativeError, "Unsupported Doublets constants layout")
  except:
    result.close()
    raise

proc checkOpen(client: NativeClient) =
  if client == nil or client.handle == nil:
    raise newException(NativeError, "Native client is closed")
  if client.busy: raise newException(NativeError, "Native client is busy")

proc checkFlow(client: NativeClient; value: uint64) =
  if value != client.constants.continueValue and value != client.constants.breakValue:
    raise newException(NativeError, "Native Doublets operation failed")

proc checkAddress(client: NativeClient; value: uint64) =
  let c = client.constants
  if value in [c.continueValue, c.breakValue, c.skipValue, c.anyValue, c.itselfValue, c.errorValue]:
    raise newException(ValueError, "Reserved Doublets control value is not an address")

proc all*(client: NativeClient): seq[NativeLink] =
  client.checkOpen()
  var state = CallbackState(continuation: client.constants.continueValue)
  let previous = currentState
  currentState = addr state
  client.busy = true
  try:
    client.checkFlow(client.eachFn(client.handle, nil, 0, readCallback))
    result = state.rows
  finally:
    client.busy = false
    currentState = previous

proc get*(client: NativeClient; id: uint64): Option[NativeLink] =
  client.checkOpen()
  if id < client.constants.internalMin or id > client.constants.internalMax:
    return none(NativeLink)
  var state = CallbackState(continuation: client.constants.continueValue)
  var query = [id]
  let previous = currentState
  currentState = addr state
  client.busy = true
  try:
    client.checkFlow(client.eachFn(client.handle, addr query[0], 1, readCallback))
    if state.rows.len != 0: result = some(state.rows[0])
  finally:
    client.busy = false
    currentState = previous

proc exists*(client: NativeClient; id: uint64): bool = client.get(id).isSome
proc count*(client: NativeClient): int =
  client.checkOpen()
  let value = client.countFn(client.handle, nil, 0)
  if value > client.constants.internalMax or value > uint64(high(int)):
    raise newException(NativeError, "Native count failed or exceeds the Nim integer range")
  int(value)

proc find*(client: NativeClient; source, target: uint64): Option[NativeLink] =
  for link in client.all():
    if link.source == source and link.target == target: return some(link)

proc update*(client: NativeClient; id, source, target: uint64): Option[NativeLink] =
  client.checkOpen()
  client.checkAddress(source)
  client.checkAddress(target)
  if client.get(id).isNone: return none(NativeLink)
  let duplicate = client.find(source, target)
  if duplicate.isSome and duplicate.get.id != id:
    raise newException(ValueError, "Another link already has this source/target pair")
  var state = CallbackState(continuation: client.constants.continueValue)
  var query = [id]
  var replacement = [id, source, target]
  let previous = currentState
  currentState = addr state
  client.busy = true
  try:
    client.checkFlow(client.updateFn(client.handle, addr query[0], 1,
      addr replacement[0], 3, writeCallback))
  finally:
    client.busy = false
    currentState = previous
  result = client.get(id)
  if result.isNone or result.get.source != source or result.get.target != target:
    raise newException(NativeError, "Native update did not produce the requested link")

proc create*(client: NativeClient; source = 0'u64; target = 0'u64): NativeLink =
  ## Pair-idempotent, matching GraphQL insert_links_one/GetOrCreate semantics.
  client.checkOpen()
  client.checkAddress(source)
  client.checkAddress(target)
  let existing = client.find(source, target)
  if existing.isSome: return existing.get
  var state = CallbackState(continuation: client.constants.continueValue)
  let previous = currentState
  currentState = addr state
  client.busy = true
  try:
    client.checkFlow(client.createFn(client.handle, nil, 0, writeCallback))
  finally:
    client.busy = false
    currentState = previous
  if state.rows.len != 1 or state.rows[0].id == 0:
    raise newException(NativeError, "Native create did not return a new link")
  result = client.update(state.rows[0].id, source, target).get

proc getOrCreate*(client: NativeClient; source, target: uint64): NativeLink =
  client.create(source, target)

proc delete*(client: NativeClient; id: uint64): bool =
  client.checkOpen()
  if client.get(id).isNone: return false
  var state = CallbackState(continuation: client.constants.continueValue)
  var query = [id]
  let previous = currentState
  currentState = addr state
  client.busy = true
  try:
    client.checkFlow(client.deleteFn(client.handle, addr query[0], 1, writeCallback))
    result = state.rows.len != 0
  finally:
    client.busy = false
    currentState = previous

proc each*(client: NativeClient; visitor: proc(link: NativeLink): bool) =
  ## Snapshot iteration. Return false to stop; callbacks may call this client.
  for link in client.all():
    if not visitor(link): break

static:
  doAssert sizeof(NativeLink) == 24
  doAssert sizeof(NativeConstants) == 120
