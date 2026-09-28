# Deep `where` predicates

The C# links server evaluates nested predicates for queries and for selecting
rows in `update_links` and `delete_links`.

```graphql
{
  links(where: {
    _or: [{ from_id: { _eq: 1 } }, { to_id: { _eq: 2 } }]
    id: { _gt: 1 }
    from: { out: { to: { id: { _eq: 2 } } } }
  }) {
    id
    from_id
    to_id
  }
}
```

## Semantics

- Sibling fields are AND conditions, including siblings of `_and`, `_or`, and
  `_not`. Comparison operators in one numeric predicate are also conjunctive.
- `_and: []` is true; `_or: []` is false. A missing or null `where` is unrestricted.
  Null entries within logical lists are rejected explicitly.
- `id`, `from_id`, and `to_id` support `_eq`, `_neq`, `_gt`, `_gte`, `_lt`, `_lte`,
  `_in`, `_nin`, and `_is_null`. Comparisons retain signed integer semantics;
  negative values do not become unsigned native wildcard addresses.
- `_is_null` uses the native **zero-address sentinel** (or an absent address),
  not SQL column-null semantics. A nonzero address whose referenced link is
  absent is not a null address; its `from`/`to` relationship does not match.
- `from` and `to` require an existing referenced link, even with an empty nested
  predicate. `in` and `out` match when at least one related link satisfies the
  nested predicate. Cycles are allowed: evaluation follows the finite predicate,
  not an unbounded traversal of the graph.
- Nested `in`/`out` result selections always retain their parent link constraint,
  including when `where` is absent or contains an OR group.
- Filtering happens before the existing ordering/pagination pipeline. Each
  selection takes one native graph snapshot and builds ID/source/target lookup
  tables. Top-level equalities can narrow candidate rows without discarding OR
  alternatives. Snapshot creation remains linear in the stored link count.
- Mutation target selection is fully materialized against the graph before the
  mutation begins. Changing/deleting an earlier referenced row cannot change
  whether a later row is selected. The existing native update/delete semantics
  are preserved; this does not add transaction or rollback guarantees.

Input nesting is limited to 32 nested levels below the root predicate. Validation
also examines logical branches which happen to match no rows, and happens before
mutation writes. The triple-backed store has no persisted type, materialized-path,
or value-table data: those auxiliary predicates are rejected instead of silently
matching rows. This change does not implement the generated auxiliary schema.

## Verification

Initialize the pinned Settings submodule (it supplies the tracked `.editorconfig`)
and install a .NET 8 SDK. From the repository root:

```sh
git submodule update --init -- Settings
dotnet restore csharp/Platform.Data.Doublets.Gql.sln
dotnet test csharp/Platform.Data.Doublets.Gql.sln --no-restore -c Release -f net8
dotnet build csharp/Platform.Data.Doublets.Gql.sln --no-restore -c Release
```

Tests execute local schemas against temporary native stores. New cases seed
actual graph rows and assert result IDs/values, including multiple relationship
levels, logical siblings, negative comparisons, zero/missing references, parent
constraints, GraphQL variables, mutation snapshots, and validation before writes.
Schema/server projects still target their existing `net5;net6` frameworks; the
test project targets `net8`. Existing end-of-support and nullable warnings are
not suppressed by this change.
