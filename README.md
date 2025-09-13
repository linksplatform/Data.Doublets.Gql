# Data.Doublets.Gql

If you need any help, you can ged it real-time on our official discord server: https://discord.gg/eEXJyjWv5e

Comparison of theories:

![Comparison of theories](https://github.com/LinksPlatform/Documentation/raw/master/doc/TheoriesComparison/theories_comparison_en.png)

## Online demo

If you are lucky (our server is up) you can test our GraphQL server online using these urls:
* http://linksplatform.ddns.net:29018/ui/playground
* http://linksplatform.ddns.net:29018/ui/graphiql
* http://linksplatform.ddns.net:29018/ui/altair
* http://linksplatform.ddns.net:29018/ui/voyager

You can use this URL to access the GraphQL server directly from code, browser or any GraphQL client like [Insomnia](https://insomnia.rest/).
```
http://linksplatform.ddns.net:29018/v1/graphql
```

## Start locally

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

Or make request at:
http://localhost:60341/v1/graphql

You can change the port like this:
```
dotnet run -f net5 -c Release db.links --urls http://0.0.0.0:29018
```

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
    distinct_on: [from_id]
    order_by: { id: asc }
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
    distinct_on: [from_id]
    order_by: { id: asc }
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
      distinct_on: [from_id]
      order_by: { id: asc }
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
      distinct_on: [from_id]
      order_by: { id: asc }
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
  insert_links_one(object: {from_id: 1, to_id: 1}) {
    id
    from_id
    to_id
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

## Java Implementation

This repository now includes a complete Java implementation with three packages:

1. **Platform.Data.Doublets.Client** - Abstract API for both GraphQL and native implementations
2. **Platform.Data.Doublets.Gql.Client** - GraphQL client that connects to the server
3. **Platform.Data.Doublets.Native** - Native library wrapper using JNI

### Quick Start (Java)

The Java implementation provides a clean, type-safe API that can swap between GraphQL and native backends:

```java
import platform.data.doublets.client.*;
import platform.data.doublets.gql.client.GraphQLLinksClient;

// Create GraphQL client
LinksClient client = new GraphQLLinksClient("http://localhost:60341/v1/graphql");

// Create a link
Link link = client.getOrCreate(1L, 2L);

// Search with query builder
LinkQuery query = LinkQuery.builder()
    .fromId(1L)
    .limit(10)
    .sortBy(LinkQuery.SortField.ID, LinkQuery.SortOrder.ASC)
    .build();
    
List<Link> results = client.searchLinks(query);
```

For detailed documentation and examples, see the [Java README](java/README.md).
