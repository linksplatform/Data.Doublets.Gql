# Data.Doublets.GraphQL

Execute:
```
cd csharp/Platform.Data.Doublets.GraphQL.Server
dotnet run
```

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
