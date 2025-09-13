package platform.data.doublets.gql.client;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import platform.data.doublets.client.*;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.time.Duration;
import java.util.*;
import java.util.stream.Collectors;

/**
 * GraphQL implementation of the LinksClient interface.
 * Communicates with a Platform Data Doublets GraphQL server.
 */
public class GraphQLLinksClient implements LinksClient {
    
    private final String endpoint;
    private final HttpClient httpClient;
    private final ObjectMapper objectMapper;
    
    /**
     * Creates a new GraphQL client with the specified endpoint.
     *
     * @param endpoint the GraphQL server endpoint URL
     */
    public GraphQLLinksClient(String endpoint) {
        this.endpoint = endpoint;
        this.httpClient = HttpClient.newBuilder()
                .connectTimeout(Duration.ofSeconds(30))
                .build();
        this.objectMapper = new ObjectMapper();
    }
    
    /**
     * Creates a new GraphQL client with custom HTTP client.
     *
     * @param endpoint   the GraphQL server endpoint URL
     * @param httpClient the HTTP client to use
     */
    public GraphQLLinksClient(String endpoint, HttpClient httpClient) {
        this.endpoint = endpoint;
        this.httpClient = httpClient;
        this.objectMapper = new ObjectMapper();
    }

    @Override
    public Link getOrCreate(long fromId, long toId) throws DoubletsException {
        String mutation = """
            mutation InsertLink($fromId: Long!, $toId: Long!) {
              insert_links_one(object: {from_id: $fromId, to_id: $toId}) {
                id
                from_id
                to_id
              }
            }
            """;
        
        Map<String, Object> variables = Map.of(
            "fromId", fromId,
            "toId", toId
        );
        
        JsonNode result = executeGraphQL(mutation, variables);
        JsonNode linkData = result.path("data").path("insert_links_one");
        
        if (linkData.isMissingNode()) {
            throw new DoubletsException("Failed to create link");
        }
        
        return parseLink(linkData);
    }

    @Override
    public Link create(long fromId, long toId) throws DoubletsException {
        return getOrCreate(fromId, toId);
    }

    @Override
    public Link update(long linkId, long newFromId, long newToId) throws DoubletsException {
        String mutation = """
            mutation UpdateLink($linkId: Long!, $fromId: Long!, $toId: Long!) {
              update_links_by_pk(
                pk_columns: {id: $linkId}
                _set: {from_id: $fromId, to_id: $toId}
              ) {
                id
                from_id
                to_id
              }
            }
            """;
        
        Map<String, Object> variables = Map.of(
            "linkId", linkId,
            "fromId", newFromId,
            "toId", newToId
        );
        
        JsonNode result = executeGraphQL(mutation, variables);
        JsonNode linkData = result.path("data").path("update_links_by_pk");
        
        if (linkData.isMissingNode()) {
            throw new DoubletsException("Failed to update link with id: " + linkId);
        }
        
        return parseLink(linkData);
    }

    @Override
    public void delete(long linkId) throws DoubletsException {
        String mutation = """
            mutation DeleteLink($linkId: Long!) {
              delete_links_by_pk(id: $linkId) {
                id
              }
            }
            """;
        
        Map<String, Object> variables = Map.of("linkId", linkId);
        
        JsonNode result = executeGraphQL(mutation, variables);
        JsonNode deletedLink = result.path("data").path("delete_links_by_pk");
        
        if (deletedLink.isMissingNode()) {
            throw new DoubletsException("Failed to delete link with id: " + linkId);
        }
    }

    @Override
    public Optional<Link> getLink(long linkId) throws DoubletsException {
        String query = """
            query GetLink($linkId: Long!) {
              links_by_pk(id: $linkId) {
                id
                from_id
                to_id
              }
            }
            """;
        
        Map<String, Object> variables = Map.of("linkId", linkId);
        
        JsonNode result = executeGraphQL(query, variables);
        JsonNode linkData = result.path("data").path("links_by_pk");
        
        if (linkData.isMissingNode() || linkData.isNull()) {
            return Optional.empty();
        }
        
        return Optional.of(parseLink(linkData));
    }

    @Override
    public List<Link> searchLinks(LinkQuery query) throws DoubletsException {
        StringBuilder queryBuilder = new StringBuilder();
        queryBuilder.append("query SearchLinks(");
        
        Map<String, Object> variables = new HashMap<>();
        List<String> whereConditions = new ArrayList<>();
        List<String> orderByConditions = new ArrayList<>();
        
        // Build variables and where conditions
        if (query.getId().isPresent()) {
            queryBuilder.append("$id: Long!, ");
            variables.put("id", query.getId().get());
            whereConditions.add("id: {_eq: $id}");
        }
        
        if (query.getFromId().isPresent()) {
            queryBuilder.append("$fromId: Long!, ");
            variables.put("fromId", query.getFromId().get());
            whereConditions.add("from_id: {_eq: $fromId}");
        }
        
        if (query.getToId().isPresent()) {
            queryBuilder.append("$toId: Long!, ");
            variables.put("toId", query.getToId().get());
            whereConditions.add("to_id: {_eq: $toId}");
        }
        
        if (query.getLimit().isPresent()) {
            queryBuilder.append("$limit: Int!, ");
            variables.put("limit", query.getLimit().get());
        }
        
        if (query.getOffset().isPresent()) {
            queryBuilder.append("$offset: Int!, ");
            variables.put("offset", query.getOffset().get());
        }
        
        // Remove trailing comma and space
        if (queryBuilder.toString().endsWith(", ")) {
            queryBuilder.setLength(queryBuilder.length() - 2);
        }
        
        queryBuilder.append(") {\n  links(");
        
        // Add where clause
        if (!whereConditions.isEmpty()) {
            queryBuilder.append("where: {").append(String.join(", ", whereConditions)).append("}, ");
        }
        
        // Add order by clause
        if (query.getSortField().isPresent() && query.getSortOrder().isPresent()) {
            String field = query.getSortField().get().name().toLowerCase();
            String order = query.getSortOrder().get().name().toLowerCase();
            queryBuilder.append("order_by: {").append(field).append(": ").append(order).append("}, ");
        }
        
        // Add limit and offset
        if (query.getLimit().isPresent()) {
            queryBuilder.append("limit: $limit, ");
        }
        
        if (query.getOffset().isPresent()) {
            queryBuilder.append("offset: $offset, ");
        }
        
        // Remove trailing comma and space
        if (queryBuilder.toString().endsWith(", ")) {
            queryBuilder.setLength(queryBuilder.length() - 2);
        }
        
        queryBuilder.append(") {\n    id\n    from_id\n    to_id\n  }\n}");
        
        JsonNode result = executeGraphQL(queryBuilder.toString(), variables);
        JsonNode linksData = result.path("data").path("links");
        
        return parseLinks(linksData);
    }

    @Override
    public List<Link> getAllLinks() throws DoubletsException {
        String query = """
            query GetAllLinks {
              links {
                id
                from_id
                to_id
              }
            }
            """;
        
        JsonNode result = executeGraphQL(query, Collections.emptyMap());
        JsonNode linksData = result.path("data").path("links");
        
        return parseLinks(linksData);
    }

    @Override
    public List<Link> getLinksByFrom(long fromId) throws DoubletsException {
        LinkQuery query = LinkQuery.builder()
                .fromId(fromId)
                .build();
        return searchLinks(query);
    }

    @Override
    public List<Link> getLinksByTo(long toId) throws DoubletsException {
        LinkQuery query = LinkQuery.builder()
                .toId(toId)
                .build();
        return searchLinks(query);
    }

    @Override
    public long getLinksCount() throws DoubletsException {
        String query = """
            query GetLinksCount {
              links_aggregate {
                aggregate {
                  count
                }
              }
            }
            """;
        
        JsonNode result = executeGraphQL(query, Collections.emptyMap());
        JsonNode countData = result.path("data").path("links_aggregate").path("aggregate").path("count");
        
        if (countData.isMissingNode()) {
            throw new DoubletsException("Failed to get links count");
        }
        
        return countData.asLong();
    }
    
    private JsonNode executeGraphQL(String query, Map<String, Object> variables) throws DoubletsException {
        try {
            Map<String, Object> requestBody = Map.of(
                "query", query,
                "variables", variables
            );
            
            String requestBodyJson = objectMapper.writeValueAsString(requestBody);
            
            HttpRequest request = HttpRequest.newBuilder()
                    .uri(URI.create(endpoint))
                    .header("Content-Type", "application/json")
                    .POST(HttpRequest.BodyPublishers.ofString(requestBodyJson))
                    .build();
            
            HttpResponse<String> response = httpClient.send(request, HttpResponse.BodyHandlers.ofString());
            
            if (response.statusCode() != 200) {
                throw new DoubletsException("HTTP error: " + response.statusCode() + " - " + response.body());
            }
            
            JsonNode responseJson = objectMapper.readTree(response.body());
            
            if (responseJson.has("errors")) {
                JsonNode errors = responseJson.get("errors");
                throw new DoubletsException("GraphQL errors: " + errors.toString());
            }
            
            return responseJson;
            
        } catch (IOException | InterruptedException e) {
            throw new DoubletsException("Failed to execute GraphQL request", e);
        }
    }
    
    private Link parseLink(JsonNode linkData) {
        long id = linkData.path("id").asLong();
        long fromId = linkData.path("from_id").asLong();
        long toId = linkData.path("to_id").asLong();
        return new Link(id, fromId, toId);
    }
    
    private List<Link> parseLinks(JsonNode linksData) {
        if (!linksData.isArray()) {
            return Collections.emptyList();
        }
        
        List<Link> links = new ArrayList<>();
        for (JsonNode linkData : linksData) {
            links.add(parseLink(linkData));
        }
        return links;
    }
}