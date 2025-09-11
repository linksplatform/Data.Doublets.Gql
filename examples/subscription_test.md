# GraphQL Subscriptions Test

This file demonstrates how to test the newly implemented GraphQL subscriptions in the Data.Doublets.Gql project.

## Available Subscriptions

### 1. Link Created Subscription
Subscribe to new link creation events:

```graphql
subscription LinkCreated {
  link_created {
    id
    from_id
    to_id
    type_id
  }
}
```

With filtering:
```graphql
subscription LinkCreatedFiltered {
  link_created(where: { from_id: { _eq: 1 } }) {
    id
    from_id
    to_id
    type_id
  }
}
```

### 2. Link Updated Subscription
Subscribe to link update events:

```graphql
subscription LinkUpdated {
  link_updated {
    id
    from_id
    to_id
    type_id
  }
}
```

### 3. Link Deleted Subscription
Subscribe to link deletion events:

```graphql
subscription LinkDeleted {
  link_deleted
}
```

With ID filtering:
```graphql
subscription LinkDeletedFiltered {
  link_deleted(id: 123)
}
```

### 4. Link Changed Subscription
Subscribe to all link changes (created and updated):

```graphql
subscription LinkChanged {
  link_changed {
    id
    from_id
    to_id
    type_id
  }
}
```

## Testing Instructions

1. Start the GraphQL server
2. Open GraphQL Playground or similar tool
3. Set up a subscription using one of the queries above
4. In another tab/window, perform mutations:
   - Insert new links
   - Update existing links
   - Delete links
5. Observe that the subscription receives real-time notifications

## WebSocket Connection

Subscriptions require a WebSocket connection. The server is already configured to support WebSocket subscriptions through the `GraphQL.Server.Transports.Subscriptions.WebSockets` package.

The WebSocket endpoint is available at the same GraphQL endpoint with the WebSocket protocol.