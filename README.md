# Data.Doublets.Gql

Execute:
```
cd csharp/Platform.Data.Doublets.Gql.Server
dotnet run
```

By default Gql server uses `db.links` as a name of the database.

Navigate to:
```
http://localhost:60341/ui/playground
```

Use query:
```gql
{
  links {
    id
  }
}
```

```gql
{
  links(
    where: { from_id: { _eq: 1 }, to_id: { _eq: 1 } }
    distinct: [FROM_ID]
    order_by: { id: ASC }
    offset: 0
    limit: 1
  ) {
    id
    from_id
    from {
      id
      from_id
      to_id
    }
    out {
      id
      from_id
      to_id
    }
    to_id
    to {
      id
      from_id
      to_id
    }
    in {
      id
      from_id
      to_id
    }
  }
}
```

Use mutation:
```gql
mutation {
  insert_links_one(object: { from_id: 1, to_id: 1 }) {
    id
  }
}
```

```gql
mutation {
  insert_links(objects: [{ from_id: 1, to_id: 1 }, { from_id: 2, to_id: 2 }]) {
    id
  }
}
```
