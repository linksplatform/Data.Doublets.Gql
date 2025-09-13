# Package
version     = "0.1.0"
author      = "LinksPlatform"
description = "Platform.Data.Doublets.Gql.Client - GraphQL client to doublets"
license     = "LGPL-3.0-or-later"
srcDir      = "src"

# Dependencies
requires "nim >= 1.6.0"
requires "httpclient >= 1.0.0"
requires "json >= 1.0.0"
requires "asyncdispatch >= 1.0.0"

# Tasks
task test, "Runs the test suite":
  exec "nim compile --verbosity:0 --hints:off -r tests/test_gql_client.nim"