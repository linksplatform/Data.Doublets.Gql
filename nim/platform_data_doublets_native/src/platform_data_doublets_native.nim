## Platform.Data.Doublets.Native
## 
## This module provides a Nim wrapper for the native Doublets DLL API.
## It implements the core ILinks interface for CRUD operations on doublets.

{.pragma: doubletsdll, cdecl, dynlib: "Platform.Data.Doublets.dll".}

type
  DoubletsHandle* = pointer
  LinkIndex* = uint64

# DLL function declarations
proc doublets_open*(path: cstring): DoubletsHandle {.importc: "doublets_open", doubletsdll.}
proc doublets_close*(handle: DoubletsHandle) {.importc: "doublets_close", doubletsdll.}
proc doublets_create*(handle: DoubletsHandle, fromId: LinkIndex, toId: LinkIndex): LinkIndex {.importc: "doublets_create", doubletsdll.}
proc doublets_update*(handle: DoubletsHandle, linkId: LinkIndex, fromId: LinkIndex, toId: LinkIndex): LinkIndex {.importc: "doublets_update", doubletsdll.}
proc doublets_delete*(handle: DoubletsHandle, linkId: LinkIndex) {.importc: "doublets_delete", doubletsdll.}
proc doublets_get*(handle: DoubletsHandle, linkId: LinkIndex, fromId: ptr LinkIndex, toId: ptr LinkIndex): bool {.importc: "doublets_get", doubletsdll.}
proc doublets_exists*(handle: DoubletsHandle, linkId: LinkIndex): bool {.importc: "doublets_exists", doubletsdll.}
proc doublets_count*(handle: DoubletsHandle): LinkIndex {.importc: "doublets_count", doubletsdll.}
proc doublets_each*(handle: DoubletsHandle, callback: proc(linkId, fromId, toId: LinkIndex): bool {.cdecl.}) {.importc: "doublets_each", doubletsdll.}

# Doublets structure representing a link
type
  Link* = object
    id*: LinkIndex
    fromId*: LinkIndex
    toId*: LinkIndex

  DoubletsException* = object of CatchableError

# High-level Nim API wrapping the DLL
type
  DoubletsClient* = ref object
    handle: DoubletsHandle

proc newDoubletsClient*(databasePath: string): DoubletsClient =
  ## Creates a new DoubletsClient connected to the specified database file
  result = DoubletsClient()
  result.handle = doublets_open(databasePath.cstring)
  if result.handle.isNil:
    raise newException(DoubletsException, "Failed to open doublets database: " & databasePath)

proc close*(client: DoubletsClient) =
  ## Closes the doublets database connection
  if not client.handle.isNil:
    doublets_close(client.handle)
    client.handle = nil

proc create*(client: DoubletsClient, fromId: LinkIndex, toId: LinkIndex): LinkIndex =
  ## Creates a new link with specified fromId and toId
  ## Returns the id of the created link
  result = doublets_create(client.handle, fromId, toId)
  if result == 0:
    raise newException(DoubletsException, "Failed to create link")

proc getOrCreate*(client: DoubletsClient, fromId: LinkIndex, toId: LinkIndex): LinkIndex =
  ## Gets existing link or creates a new one with specified fromId and toId
  ## This is the primary method for creating/retrieving links
  result = client.create(fromId, toId)

proc update*(client: DoubletsClient, linkId: LinkIndex, fromId: LinkIndex, toId: LinkIndex): LinkIndex =
  ## Updates an existing link with new fromId and toId
  ## Returns the id of the updated link
  result = doublets_update(client.handle, linkId, fromId, toId)
  if result == 0:
    raise newException(DoubletsException, "Failed to update link")

proc update*(client: DoubletsClient, oldFromId: LinkIndex, oldToId: LinkIndex, newFromId: LinkIndex, newToId: LinkIndex): LinkIndex =
  ## Updates a link identified by oldFromId and oldToId with new values
  # Find the link first by iterating (simplified implementation)
  var foundLinkId: LinkIndex = 0
  proc findCallback(linkId, fromId, toId: LinkIndex): bool {.cdecl.} =
    if fromId == oldFromId and toId == oldToId:
      foundLinkId = linkId
      return false  # Stop iteration
    return true  # Continue iteration
  
  doublets_each(client.handle, findCallback)
  if foundLinkId != 0:
    result = client.update(foundLinkId, newFromId, newToId)
  else:
    raise newException(DoubletsException, "Link not found for update")

proc delete*(client: DoubletsClient, linkId: LinkIndex) =
  ## Deletes a link by its id
  doublets_delete(client.handle, linkId)

proc get*(client: DoubletsClient, linkId: LinkIndex): Link =
  ## Gets a link by its id
  ## Returns a Link object with id, fromId, and toId
  var fromId, toId: LinkIndex
  if doublets_get(client.handle, linkId, addr fromId, addr toId):
    result = Link(id: linkId, fromId: fromId, toId: toId)
  else:
    raise newException(DoubletsException, "Link not found: " & $linkId)

proc getLink*(client: DoubletsClient, linkId: LinkIndex): seq[LinkIndex] =
  ## Gets a link as a sequence [id, fromId, toId]
  let link = client.get(linkId)
  result = @[link.id, link.fromId, link.toId]

proc exists*(client: DoubletsClient, linkId: LinkIndex): bool =
  ## Checks if a link exists by its id
  result = doublets_exists(client.handle, linkId)

proc count*(client: DoubletsClient): LinkIndex =
  ## Returns the total number of links in the database
  result = doublets_count(client.handle)

iterator each*(client: DoubletsClient): Link =
  ## Iterates over all links in the database
  var links: seq[Link] = @[]
  
  proc collectCallback(linkId, fromId, toId: LinkIndex): bool {.cdecl.} =
    links.add(Link(id: linkId, fromId: fromId, toId: toId))
    return true  # Continue iteration
  
  doublets_each(client.handle, collectCallback)
  
  for link in links:
    yield link

# Cleanup
proc `=destroy`*(client: DoubletsClient) =
  if not client.handle.isNil:
    client.close()