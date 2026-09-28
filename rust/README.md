# Rust GraphQL server

A standalone `async-graphql` / Actix Web server backed by the native Rust
`doublets` store. It implements the core links queries and mutations documented
in the repository README.

## Run

Requires Rust 1.89 or later. From the repository root:

```sh
cargo run --locked --manifest-path rust/Cargo.toml -- ./doublets-data 127.0.0.1:8000
```

Both arguments are optional; these are their defaults. The first is a **database
directory**, containing `db.links` and a writer lock. Open
<http://127.0.0.1:8000/ui/playground>, or POST GraphQL JSON to
<http://127.0.0.1:8000/v1/graphql>. POST `/` is also supported.

```sh
curl http://127.0.0.1:8000/v1/graphql \
  -H 'Content-Type: application/json' \
  -d '{"query":"mutation { insert_links_one(object: {from_id: 1, to_id: 1}) { id from_id to_id } }"}'

curl http://127.0.0.1:8000/v1/graphql \
  -H 'Content-Type: application/json' \
  -d '{"query":"{ links(order_by: {id: asc}) { id from_id to_id from { id } out { id } } }"}'
```

Stop the server normally (Ctrl-C) before moving or backing up its database. The
same directory can be reopened on the next run. An exclusive file lock rejects a
second server using the same directory. Use a fresh directory: no migration or
binary compatibility with legacy C# or old Rust database files is promised.

## Supported schema

| Area | Implemented |
| --- | --- |
| Queries | `links`, `links_by_pk(id)` |
| Link fields | `id`, `from_id`, `to_id`, `from`, `to`, `in`, `out` |
| Selection arguments | `where`, `order_by`, `distinct_on`, `offset`, `limit` on `links`, `in`, and `out` |
| Scalar predicates | `_eq`, `_neq`, `_gt`, `_gte`, `_lt`, `_lte`, `_in`, `_nin`, `_is_null` |
| Logical predicates | `_and`, `_or`, `_not`, with conjunctive sibling fields |
| Relationship predicates | `from`, `to`, `in`, `out`; nested matches are existential |
| Insert | `insert_links_one(object)`, `insert_links(objects)` |
| Update | `update_links(where, _set, _inc)`, `update_links_by_pk(pk_columns, _set, _inc)` |
| Delete | `delete_links(where)`, `delete_links_by_pk(id)` |
| Mutation results | `affected_rows`, `returning`; single-row mutations return a link or `null` |

`order_by` supports `asc` and `desc` for `id`, `from_id`, and `to_id`. Sorting
precedes distinct selection and pagination; ID is the deterministic tie breaker.
`distinct_on` keeps the first ordered row for each selected column tuple. Empty
`_and` matches all rows; empty `_or` matches none.

Addresses accept nonnegative signed 64-bit integers, including zero for a null
reference. IDs must be positive. `_is_null` checks whether an address is zero.
Negative comparisons behave numerically; negative mutation addresses and
arithmetic overflow are rejected. `from` and `to` return `null` when the address
has no stored link. Deleting a referenced link preserves the other links and
their numeric addresses. The native store can reuse a deleted ID on later insert.

Inserts use pair identity: inserting an existing `(from_id, to_id)` returns its
existing ID, including `(0,0)`. Batch insert returns one result per input;
`affected_rows` counts processed input objects, including existing pairs, rather
than newly allocated links. Updates reject duplicate **final** pairs while
allowing bulk increments whose intermediate pairs would collide. A column cannot
be both set and incremented. Validation covers an entire mutation batch before
any changes. This is not a transactional write-ahead-log engine: an I/O failure
or process crash during writes has no rollback guarantee.

The schema exposes links only. Generated authentication, value-table,
materialized-path, aggregate, subscription, nested-insert, and `on_conflict`
placeholders are deliberately not registered; requests for those fields fail
GraphQL validation. This is not a complete Hasura replacement. Query depth is
limited to 32, complexity to 1,000, and filter nesting to 32 levels. The server
has no authentication and defaults to loopback; put an authenticated gateway in
front of it before exposing a shared deployment. Queries currently scan a
snapshot, so this implementation prioritizes correct graph semantics over large
scale query optimization.

The current safe capacity is **1,048,574 stored links**. Insert batches are
checked against the number of distinct new pairs before any write; duplicate
inputs consume no new capacity, and deleted slots can be reused. This cap stays
below a growth/reopening boundary defect in the pinned native dependency. The
native file reserves 64 MiB. Startup checks its size, header, free list, index
bounds, tree structure, and link pairs before mapping it. Unsupported or
inconsistent files fail to open without modification; keep backups for recovery.

## Storage and dependency compatibility

The original `doublets` beta dependency depends on removed nightly Rust
features and does not build on stable Rust. This server pins `doublets = 0.5.0`
and uses its native single-memory (`unit`) store. Its indexes support zero-ended
links and external addresses, including addresses larger than the current
allocated ID range.

`StoreMemory` in `src/store.rs` adapts two upstream memory API mismatches:
`doublets::mem::resize_mem` needs the entire allocation after growth, while
`platform-mem` returns the appended tail; and default `grow_filled` overwrites
already initialized file bytes. The adapter returns the full allocation and
uses `grow_filled_exact` to preserve those bytes. It retains the native file
format, allocation, indexes, and link operations. The store lives on one worker
thread; GraphQL requests send operations to it and receive results asynchronously.
No unsafe `Send` or `Sync` implementation is added, and nested relationship
resolution does not retain a database lock.

## Verify

```sh
cargo fmt --manifest-path rust/Cargo.toml --check
cargo test --locked --manifest-path rust/Cargo.toml
cargo clippy --locked --manifest-path rust/Cargo.toml --all-targets -- -D warnings
cargo build --locked --manifest-path rust/Cargo.toml
```

Tests use temporary databases and local HTTP handlers. They cover documented
CRUD, primary keys, predicates, nested relationships, ordering/distinct/pagination,
validation, bulk update collisions, duplicate inserts, persistence, concurrent
requests, large external addresses, and exclusive directory ownership.
