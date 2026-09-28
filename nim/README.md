# Nim Doublets adapters

Three independent Nimble packages implement the architecture in [issue #22](https://github.com/linksplatform/Data.Doublets.Gql/issues/22). Nim module/package names use underscores because dots are not Nimble package-name separators.

| Requested layer | Nimble package / import | Dependencies |
| --- | --- | --- |
| Platform.Data.Doublets.Native | `platform_data_doublets_native` | Nim standard library; separately supplied native DLL at runtime |
| Platform.Data.Doublets.Gql.Client | `platform_data_doublets_gql_client` | Nim standard library |
| Platform.Data.Doublets.Client | `platform_data_doublets_client` | The two packages above |

## Install

Use Nim 2.0 or newer, Nimble and a C compiler. From this `nim/` directory, install the local packages in dependency order:

```sh
(cd platform_data_doublets_native && nimble install)
(cd platform_data_doublets_gql_client && nimble install)
(cd platform_data_doublets_client && nimble install)
nim c -d:ssl examples/client.nim
```

These are source packages; they have not been published to the Nimble registry. The first two can be installed and used separately. For isolated installation, pass the same `--nimbleDir:/absolute/scratch/directory` to all Nimble commands and use `--NimblePath:/absolute/scratch/directory/pkgs2` when compiling an external consumer. Nimble may need its normal package index on first use. No third-party Nim package is required. `-d:ssl` enables HTTPS using Nim's standard OpenSSL support; HTTP-only programs do not require it.

### Native library

Supply an absolute path to the platform library from the official [Platform.Data.Doublets.FFI 0.9.0 NuGet package](https://www.nuget.org/packages/Platform.Data.Doublets.FFI/0.9.0): `libPlatform.Doublets.so`, `Platform.Doublets.dll`, or `libPlatform.Doublets.dylib`. The library is not bundled. Its license is LGPL-3.0-only; the Nim code follows this repository's Unlicense.

The wrapper targets the package's **UInt64** C ABI: `UInt64UnitedMemoryLinks_New`, `Drop`, `GetConstants`, `Create`, `Each`, `Count`, `Update`, and `Delete`. It resolves symbols at `openNative`, not at import time. A GraphQL-only application can import the unified package without installing the native library. Another ABI requires an adapter, not just renaming the DLL.

Only Linux x86-64 native loading has been exercised. Windows/macOS filenames are provided, but those platforms are not claimed as tested. Use trusted compatible library files and valid Doublets databases. Open a database through one owning client/process at a time; this wrapper does not add a cross-process file lock or repair corrupt databases.

## Use either backend through one API

```nim
import std/options
import platform_data_doublets_client

proc main() =
  let client = openNativeClient("example.links", "/path/to/libPlatform.Doublets.so")
  # Swap only construction to use the server:
  # let client = openGraphqlClient("http://localhost:5000/v1/graphql")
  defer: client.close()

  let row = client.getOrCreate(1, 1)
  let other = client.getOrCreate(2, 2)
  doAssert client.get(row.id).get == row
  discard client.update(row.id, 1, 2)
  client.each(proc(link: Link): bool =
    echo link
    true) # false stops snapshot iteration
  discard client.delete(row.id)
  discard client.delete(other.id)

main()
```

[`examples/client.nim`](examples/client.nim) is a runnable version. It writes data: point it at a disposable database or a server you control.

All three layers expose `create`, `getOrCreate`, `get`, `exists`, `find`, `update`, `delete`, `all`, `count`, `each` and `close`. Their link records contain `id`, `source` and `target` (`uint64`). The standalone constructors are `openNative` and `openGraphql`; their record types are `NativeLink` and `GqlLink`. The unified constructors return the same `DoubletsClient` type and `Link` records.

The GraphQL layer also exposes `execute(query, variables)` for arbitrary supported queries and JSON-object variables. `openGraphql` / `openGraphqlClient` accept a positive `timeoutMs` and optional `std/httpclient.HttpHeaders` for authentication. Pass credentials from your application's secret store, not source files.

### Semantics and boundaries

- Clients are synchronous, single-thread owners. Do not share one instance between threads. Explicitly call `close`, normally with `defer`; close is idempotent and further operations raise an error.
- `create` and `getOrCreate` use pair identity: requesting an existing `(source, target)` returns its link. They do not force a second link for the same pair. An update to another existing pair raises `ValueError`. The original C# server can create duplicate blank rows through raw mutations; the adapter avoids that with a snapshot lookup for zero endpoints and returns the lowest existing matching ID if such duplicates already exist.
- `get`, `find` and `update` return `Option[Link]`; a missing ID produces `none`. `delete` returns whether the backend selected an existing link for deletion. Native deletion rules apply: links referenced elsewhere may be retained/reset rather than physically removed. There is no added cascading-delete policy.
- ID zero means no link; a zero source/target is allowed. GraphQL accepts nonnegative signed 64-bit addresses (`0 .. high(int64)`). The native ABI additionally reserves its six control constants (`high(int64)-5 .. high(int64)`); **`0 .. high(int64)-6` is the common input range**. Native addresses beyond the GraphQL range are not portable between backends.
- Native `find` scans all links, so pair-idempotent create and duplicate-checked update are O(n). GraphQL `find`/`create` also use an O(n) snapshot when either endpoint is zero, avoiding the original C# native index limitation; nonzero pair lookup uses the server's scalar filters. GraphQL `count` requests all links because the original C# schema has no implemented aggregate count. `all` and `each` allocate a full snapshot. A visitor may call the client; newly created rows are not added to the current snapshot.
- The wrapper guards native missing-ID updates and verifies write results. Native create allocates a blank row and then updates it, as required by FFI 0.9.0; it is not an atomic transaction. Remote read/check/write sequences are also not transactions and cannot prevent writes by other clients between requests. Failed remote mutations are never automatically retried.
- Native errors raise `NativeError`, transport/HTTP/GraphQL/malformed-response errors raise `GraphqlError`, and invalid local inputs raise `ValueError`. Partial GraphQL data accompanied by errors is rejected. A lost response can leave a mutation's outcome unknown; inspect the database before manually retrying.
- **Original C# backend limitation:** its underlying storage can fail on writes with a nonzero source and zero target. A real `insert_links_one(1, 0)` returned null while allocating an ID absent from subsequent `links` results; updating a zero-endpoint row also produced resolver errors. The adapter reports these as `GraphqlError` and does not retry or pretend the write succeeded. Use the native FFI backend for reliable zero-target writes, or fix/upgrade the server's storage layer. Empty `(0, 0)` identity and `(0, existingId)` create/read/find/delete are verified against the original C# server. The API cannot provide stronger write semantics than the selected server.

## Compatibility and verification

Verified on **2026-09-28**, Linux x86-64, Nim **2.2.12**, Nimble **0.24.1**, GCC, against repository main **`b11f33b4080a7ef6b6d1c056c40bbf758d6cdd7e`**:

| Backend | Verification |
| --- | --- |
| Official native FFI 0.9.0 | Real DLL CRUD, count, duplicate checks, missing IDs, zero pair, snapshot reentry, Drop/reopen persistence |
| Original C# GraphQL server at the commit above | Real HTTP CRUD through standalone and unified clients; original `net6` build run on .NET 8 with explicit `DOTNET_ROLL_FORWARD=Major` |
| Optional Rust GraphQL implementation | Additional interoperability check only; not a package/runtime dependency |
| Local HTTP fixtures | HTTP failure, response-body timeout, malformed JSON/data, GraphQL errors, invalid addresses, variables and custom headers |

The GraphQL CRUD adapter uses the existing `links(where:)`, `insert_links_one`, `update_links` and `delete_links` resolvers. It does not depend on the original C# server's unimplemented `*_by_pk` or aggregate resolvers. Source and target map to `from_id` and `to_id`.

The native implementation follows the **binary's actual ABI**: 24-byte by-value link records, a 120-byte constants layout with `Continue` before `Break`, callback-derived create IDs, and a follow-up update because Create does not apply a pair. It does not assume the update callback contains the old record. Official NuGet archive SHA-256: `2a462782035bfe839a17c5b5cbcf1f744fcbec74d98c76576a55a1e448f52fb0`.

### Reproduce the tests

Build the original C# server with the repository's pinned `Settings` submodule initialized and its required .NET targeting packs/dependencies restored. Supply an appropriate .NET runtime; the roll-forward switch below is only needed when running its `net6` output with a newer runtime.

From the repository root:

```sh
python3 nim/tests/run_tests.py \
  --native-library /absolute/path/libPlatform.Doublets.so \
  --csharp-server /absolute/path/Platform.Data.Doublets.Gql.Server.dll \
  --dotnet /absolute/path/dotnet \
  --dotnet-roll-forward Major \
  --release --ssl
```

Optional arguments are `--nim /path/to/nim`, `--nim-lib /path/to/nim/lib`, and `--rust-server /path/to/doublets_gql_server`. A normal Nim installation requires no library-path override. Python 3 uses only its standard library. The harness binds loopback ports, creates disposable databases, starts and cleans up its own servers, and saves command/exit-code records plus logs under `nim/build/test-results/`. It refuses to run the writing GraphQL contract against a nonempty database.

Native tests can also run through `nimble test` with `DOUBLETS_FFI_LIBRARY`. Standalone GraphQL tests require `DOUBLETS_GQL_URL` and the local fixture service from the harness (`DOUBLETS_FIXTURE_URL`); use the harness for the complete reproducible run. Unified tests choose `DOUBLETS_TEST_BACKEND=native|graphql` and the corresponding environment variables. The harness sets `DOUBLETS_TEST_SERVER_KIND` to `csharp` or `rust`: only the original C# zero-target behavior has a version-scoped expected-error test, run last before discarding its database. Native/Rust retain the full zero-endpoint contract. With both servers enabled, the harness compiles four test executables and runs 21 test cases: native 4, unified/native 2, standalone GraphQL 5 per server, unified/GraphQL 2 per server, and the original C# known-limitation case 1.

Nim 2.2.12/GCC produced qualifier warnings in generated standard-library C during these builds. No generated C or compiler output is checked into the packages. External services, real user databases, and native libraries for other platforms are outside this verification.
