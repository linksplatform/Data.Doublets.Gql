# Package
version     = "0.1.0"
author      = "LinksPlatform"
description = "Platform.Data.Doublets.Native - DLL API wrapped in Nim"
license     = "LGPL-3.0-or-later"
srcDir      = "src"

# Dependencies  
requires "nim >= 1.6.0"

# Tasks
task test, "Runs the test suite":
  exec "nim compile --verbosity:0 --hints:off -r tests/test_native.nim"