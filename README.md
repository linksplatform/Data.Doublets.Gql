# Data.Doublets.Gql

Execute:
```
cd csharp/Platform.Data.Doublets.Gql.Server
```

By default Gql server uses `db.links` as a name of the database:
```
dotnet run
```

You can set any path to a database file:
```
dotnet run path/to/db.links
```

Navigate to:
* http://localhost:60341/ui/playground
* http://localhost:60341/ui/graphiql
* http://localhost:60341/ui/altair
* http://localhost:60341/ui/voyager


## Supported query examples:
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
    where: { id: { _eq: 1 }, from_id: { _eq: 1 }, to_id: { _eq: 1 } }
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

```gql
{
  links(
    where: { id: { _eq: 1 }, from_id: { _eq: 1 }, to_id: { _eq: 1 } }
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
    out(
      where: { from_id: { _eq: 1 }, to_id: { _eq: 1 } }
      distinct: [FROM_ID]
      order_by: { id: ASC }
      offset: 0
      limit: 1
    ) {
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
    in(
      where: { from_id: { _eq: 1 }, to_id: { _eq: 1 } }
      distinct: [FROM_ID]
      order_by: { id: ASC }
      offset: 0
      limit: 1
    ) {
      id
      from_id
      to_id
    }
  }
}
```

## Supported mutation examples:
```gql
mutation {
  insert_links_one(object: { from_id: 1, to_id: 1 }) {
    returning {
      id
      from_id
      to_id
    }
  }
}
```

```gql
mutation {
  insert_links(objects: [{ from_id: 1, to_id: 1 }, { from_id: 2, to_id: 2 }]) {
    returning {
      id
      from_id
      to_id
    }
  }
}
```

```gql
mutation {
  update_links(_set: { from_id: 1, to_id: 2 }, where: { from_id: { _eq: 2 }, to_id: { _eq: 2 } }) {
    returning {
      id
      from_id
      to_id
    }
  }
}
```

```gql
mutation {
  delete_links(where: { from_id: { _eq: 1 }, to_id: { _eq: 1 } }) {
    returning {
      id
      from_id
      to_id
    }
  }
}
```
