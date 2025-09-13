# Platform Data Doublets Java Implementation

This is a Java implementation of Platform Data Doublets that provides three packages for different use cases:

## Packages

### 1. Platform.Data.Doublets.Client (platform-data-doublets-client)
Abstract API that provides a standard interface for both GraphQL and native implementations.

**Maven Dependency:**
```xml
<dependency>
    <groupId>platform.data.doublets</groupId>
    <artifactId>platform-data-doublets-client</artifactId>
    <version>1.0.0</version>
</dependency>
```

### 2. Platform.Data.Doublets.Gql.Client (platform-data-doublets-gql-client)
GraphQL client implementation that connects to a Platform Data Doublets GraphQL server.

**Maven Dependency:**
```xml
<dependency>
    <groupId>platform.data.doublets</groupId>
    <artifactId>platform-data-doublets-gql-client</artifactId>
    <version>1.0.0</version>
</dependency>
```

### 3. Platform.Data.Doublets.Native (platform-data-doublets-native)
Native implementation that wraps the C++ library using JNI.

**Maven Dependency:**
```xml
<dependency>
    <groupId>platform.data.doublets</groupId>
    <artifactId>platform-data-doublets-native</artifactId>
    <version>1.0.0</version>
</dependency>
```

## Usage Examples

### Using the GraphQL Client

```java
import platform.data.doublets.client.*;
import platform.data.doublets.gql.client.GraphQLLinksClient;

public class GraphQLExample {
    public static void main(String[] args) throws DoubletsException {
        // Create GraphQL client
        LinksClient client = new GraphQLLinksClient("http://localhost:8080/v1/graphql");
        
        // Create a new link
        Link link = client.getOrCreate(2L, 3L);
        System.out.println("Created link: " + link);
        
        // Search for links
        LinkQuery query = LinkQuery.builder()
                .fromId(2L)
                .limit(10)
                .sortBy(LinkQuery.SortField.ID, LinkQuery.SortOrder.ASC)
                .build();
        
        List<Link> links = client.searchLinks(query);
        System.out.println("Found " + links.size() + " links");
        
        // Get total count
        long count = client.getLinksCount();
        System.out.println("Total links: " + count);
    }
}
```

### Using the Native Client

```java
import platform.data.doublets.client.*;
import platform.data.doublets.native.NativeLinksClient;

public class NativeExample {
    public static void main(String[] args) throws DoubletsException {
        // Create native client
        try (NativeLinksClient client = new NativeLinksClient("my-database.links")) {
            
            // Create a new link
            Link link = client.create(1L, 2L);
            System.out.println("Created link: " + link);
            
            // Update the link
            Link updatedLink = client.update(link.getId(), 3L, 4L);
            System.out.println("Updated link: " + updatedLink);
            
            // Get all links
            List<Link> allLinks = client.getAllLinks();
            System.out.println("All links: " + allLinks);
            
            // Delete the link
            client.delete(link.getId());
            System.out.println("Link deleted");
            
        } // Client automatically closed here
    }
}
```

### Using the Abstract Interface

```java
import platform.data.doublets.client.*;
import platform.data.doublets.gql.client.GraphQLLinksClient;
import platform.data.doublets.native.NativeLinksClient;

public class AbstractExample {
    public static void demonstrateLinks(LinksClient client) throws DoubletsException {
        // This method works with any implementation
        Link link = client.getOrCreate(10L, 20L);
        
        Optional<Link> retrieved = client.getLink(link.getId());
        if (retrieved.isPresent()) {
            System.out.println("Retrieved: " + retrieved.get());
        }
        
        List<Link> fromLinks = client.getLinksByFrom(10L);
        System.out.println("Links from 10: " + fromLinks.size());
    }
    
    public static void main(String[] args) throws DoubletsException {
        // Can switch implementations easily
        LinksClient graphqlClient = new GraphQLLinksClient("http://localhost:8080/v1/graphql");
        LinksClient nativeClient = new NativeLinksClient("db.links");
        
        System.out.println("Using GraphQL client:");
        demonstrateLinks(graphqlClient);
        
        System.out.println("Using Native client:");
        demonstrateLinks(nativeClient);
    }
}
```

## Building

To build all packages:

```bash
cd java
mvn clean install
```

To build a specific package:

```bash
cd java/platform-data-doublets-client
mvn clean install
```

## Running Tests

```bash
cd java
mvn test
```

## Architecture

The architecture follows the dependency pattern described in the issue:

```
┌─────────────────────────┐    ┌─────────────────────────┐
│  GraphQLLinksClient     │    │   NativeLinksClient     │
│ (GraphQL implementation)│    │  (JNI implementation)   │
└───────────┬─────────────┘    └───────────┬─────────────┘
            │                              │
            └──────────────┬───────────────┘
                           │
                           ▼
                  ┌─────────────────┐
                  │  LinksClient    │
                  │ (Abstract API)  │
                  └─────────────────┘
```

This design allows:
- **Swappable implementations**: Easy to switch between GraphQL and native
- **Testability**: Mock implementations can be created for testing
- **Future extensibility**: New implementations can be added easily
- **Separation of concerns**: GraphQL logic separate from native library concerns

## Requirements

- Java 17 or higher
- Maven 3.6 or higher
- For GraphQL client: Access to a Platform Data Doublets GraphQL server
- For native client: Native library (platform-data-doublets-native.dll/so/dylib)