package platform.data.doublets.examples;

import platform.data.doublets.client.*;
import platform.data.doublets.gql.client.GraphQLLinksClient;

import java.util.List;
import java.util.Optional;

/**
 * Example demonstrating GraphQL client usage.
 */
public class GraphQLExample {
    
    private static final String GRAPHQL_ENDPOINT = "http://localhost:8080/v1/graphql";
    
    public static void main(String[] args) {
        try {
            LinksClient client = new GraphQLLinksClient(GRAPHQL_ENDPOINT);
            
            System.out.println("=== Platform Data Doublets GraphQL Client Example ===");
            
            // Create some links
            System.out.println("\n1. Creating links...");
            Link link1 = client.getOrCreate(1L, 2L);
            Link link2 = client.getOrCreate(2L, 3L);
            Link link3 = client.getOrCreate(1L, 3L);
            
            System.out.println("Created link: " + link1);
            System.out.println("Created link: " + link2);
            System.out.println("Created link: " + link3);
            
            // Get total count
            System.out.println("\n2. Getting total link count...");
            long totalCount = client.getLinksCount();
            System.out.println("Total links: " + totalCount);
            
            // Search for specific links
            System.out.println("\n3. Searching for links from node 1...");
            LinkQuery query = LinkQuery.builder()
                    .fromId(1L)
                    .sortBy(LinkQuery.SortField.TO_ID, LinkQuery.SortOrder.ASC)
                    .build();
            
            List<Link> linksFromOne = client.searchLinks(query);
            System.out.println("Found " + linksFromOne.size() + " links from node 1:");
            linksFromOne.forEach(System.out::println);
            
            // Get links by target
            System.out.println("\n4. Getting links to node 3...");
            List<Link> linksToThree = client.getLinksByTo(3L);
            System.out.println("Found " + linksToThree.size() + " links to node 3:");
            linksToThree.forEach(System.out::println);
            
            // Update a link
            System.out.println("\n5. Updating a link...");
            if (!linksFromOne.isEmpty()) {
                Link linkToUpdate = linksFromOne.get(0);
                Link updatedLink = client.update(linkToUpdate.getId(), 4L, 5L);
                System.out.println("Updated link: " + updatedLink);
            }
            
            // Get all links with pagination
            System.out.println("\n6. Getting all links with pagination...");
            LinkQuery paginatedQuery = LinkQuery.builder()
                    .limit(5)
                    .offset(0)
                    .sortBy(LinkQuery.SortField.ID, LinkQuery.SortOrder.ASC)
                    .build();
            
            List<Link> paginatedLinks = client.searchLinks(paginatedQuery);
            System.out.println("First 5 links (sorted by ID):");
            paginatedLinks.forEach(System.out::println);
            
            // Get a specific link
            System.out.println("\n7. Getting a specific link...");
            if (!paginatedLinks.isEmpty()) {
                long linkId = paginatedLinks.get(0).getId();
                Optional<Link> specificLink = client.getLink(linkId);
                if (specificLink.isPresent()) {
                    System.out.println("Found link: " + specificLink.get());
                } else {
                    System.out.println("Link not found");
                }
            }
            
            System.out.println("\n=== Example completed successfully ===");
            
        } catch (DoubletsException e) {
            System.err.println("Error: " + e.getMessage());
            e.printStackTrace();
        }
    }
}