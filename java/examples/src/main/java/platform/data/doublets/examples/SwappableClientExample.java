package platform.data.doublets.examples;

import platform.data.doublets.client.*;
import platform.data.doublets.gql.client.GraphQLLinksClient;
import platform.data.doublets.native.NativeLinksClient;

import java.util.List;

/**
 * Example demonstrating how to swap between GraphQL and Native implementations.
 * This shows the power of the abstract API - the same code works with different backends.
 */
public class SwappableClientExample {
    
    private static final String GRAPHQL_ENDPOINT = "http://localhost:8080/v1/graphql";
    private static final String DATABASE_FILE = "example.links";
    
    public static void main(String[] args) {
        System.out.println("=== Platform Data Doublets Swappable Client Example ===");
        
        // Try GraphQL client first
        try {
            System.out.println("\n--- Using GraphQL Client ---");
            LinksClient graphqlClient = new GraphQLLinksClient(GRAPHQL_ENDPOINT);
            demonstrateClient(graphqlClient, "GraphQL");
        } catch (Exception e) {
            System.out.println("GraphQL client not available: " + e.getMessage());
        }
        
        // Try Native client
        try {
            System.out.println("\n--- Using Native Client ---");
            LinksClient nativeClient = new NativeLinksClient(DATABASE_FILE);
            demonstrateClient(nativeClient, "Native");
        } catch (Exception e) {
            System.out.println("Native client not available: " + e.getMessage());
        }
        
        System.out.println("\n=== Example completed ===");
    }
    
    /**
     * Demonstrates common operations using any LinksClient implementation.
     * This method is implementation-agnostic and works with both GraphQL and Native clients.
     */
    private static void demonstrateClient(LinksClient client, String clientType) throws DoubletsException {
        System.out.println("Client type: " + clientType);
        
        // Create some test links
        System.out.println("Creating test links...");
        Link link1 = client.getOrCreate(100L, 200L);
        Link link2 = client.getOrCreate(200L, 300L);
        Link link3 = client.getOrCreate(100L, 300L);
        
        System.out.println("  Created: " + link1);
        System.out.println("  Created: " + link2);
        System.out.println("  Created: " + link3);
        
        // Query operations
        System.out.println("Querying links...");
        long totalCount = client.getLinksCount();
        System.out.println("  Total links: " + totalCount);
        
        List<Link> linksFrom100 = client.getLinksByFrom(100L);
        System.out.println("  Links from 100: " + linksFrom100.size());
        
        List<Link> linksTo300 = client.getLinksByTo(300L);
        System.out.println("  Links to 300: " + linksTo300.size());
        
        // Advanced query with builder
        LinkQuery query = LinkQuery.builder()
                .fromId(100L)
                .limit(10)
                .sortBy(LinkQuery.SortField.TO_ID, LinkQuery.SortOrder.ASC)
                .build();
        
        List<Link> searchResults = client.searchLinks(query);
        System.out.println("  Advanced search results: " + searchResults.size());
        
        // Update operation
        if (!searchResults.isEmpty()) {
            Link linkToUpdate = searchResults.get(0);
            System.out.println("Updating link " + linkToUpdate.getId() + "...");
            Link updatedLink = client.update(linkToUpdate.getId(), 400L, 500L);
            System.out.println("  Updated to: " + updatedLink);
            
            // Delete the updated link to clean up
            System.out.println("Cleaning up - deleting updated link...");
            client.delete(updatedLink.getId());
            System.out.println("  Link deleted");
        }
        
        System.out.println(clientType + " client demonstration completed.");
    }
}