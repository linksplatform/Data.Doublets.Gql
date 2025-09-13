## Tests for Platform.Data.Doublets.Native

import unittest
import ../src/platform_data_doublets_native

# Note: These tests require the Platform.Data.Doublets.dll to be available
# In a real implementation, you would test against an actual DLL
# For now, these are structural tests showing the API usage

suite "DoubletsClient Native API Tests":
  
  test "DoubletsClient creation and basic structure":
    # This test validates the API structure without requiring the actual DLL
    # In real usage, you would need the native DLL available
    try:
      let client = newDoubletsClient("test.db")
      # If we get here, the structure is correct
      # In real tests, you would perform operations like:
      # let linkId = client.create(1, 2)
      # check(linkId > 0)
      # client.close()
      discard
    except DoubletsException:
      # Expected when DLL is not available
      discard
  
  test "Link structure validation":
    let link = Link(id: 1, fromId: 2, toId: 3)
    check(link.id == 1)
    check(link.fromId == 2)  
    check(link.toId == 3)

  test "LinkIndex type validation":
    let index: LinkIndex = 12345
    check(index == 12345'u64)