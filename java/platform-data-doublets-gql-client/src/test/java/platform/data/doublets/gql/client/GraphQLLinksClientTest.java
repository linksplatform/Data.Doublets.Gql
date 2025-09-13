package platform.data.doublets.gql.client;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import platform.data.doublets.client.*;

import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.util.List;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

class GraphQLLinksClientTest {

    private GraphQLLinksClient client;
    private HttpClient mockHttpClient;
    private HttpResponse<String> mockResponse;

    @BeforeEach
    void setUp() {
        mockHttpClient = mock(HttpClient.class);
        mockResponse = mock(HttpResponse.class);
        client = new GraphQLLinksClient("http://localhost:8080/v1/graphql", mockHttpClient);
    }

    @Test
    void testClientCreation() {
        GraphQLLinksClient client = new GraphQLLinksClient("http://localhost:8080/v1/graphql");
        assertNotNull(client);
    }

    @Test
    void testGetOrCreateSuccess() throws Exception {
        // Mock HTTP response for successful link creation
        String responseBody = """
            {
              "data": {
                "insert_links_one": {
                  "id": 1,
                  "from_id": 2,
                  "to_id": 3
                }
              }
            }
            """;

        when(mockResponse.statusCode()).thenReturn(200);
        when(mockResponse.body()).thenReturn(responseBody);
        when(mockHttpClient.send(any(HttpRequest.class), any(HttpResponse.BodyHandler.class)))
                .thenReturn(mockResponse);

        Link result = client.getOrCreate(2L, 3L);

        assertNotNull(result);
        assertEquals(1L, result.getId());
        assertEquals(2L, result.getFromId());
        assertEquals(3L, result.getToId());
    }

    @Test
    void testGetLinkFound() throws Exception {
        String responseBody = """
            {
              "data": {
                "links_by_pk": {
                  "id": 1,
                  "from_id": 2,
                  "to_id": 3
                }
              }
            }
            """;

        when(mockResponse.statusCode()).thenReturn(200);
        when(mockResponse.body()).thenReturn(responseBody);
        when(mockHttpClient.send(any(HttpRequest.class), any(HttpResponse.BodyHandler.class)))
                .thenReturn(mockResponse);

        Optional<Link> result = client.getLink(1L);

        assertTrue(result.isPresent());
        assertEquals(1L, result.get().getId());
        assertEquals(2L, result.get().getFromId());
        assertEquals(3L, result.get().getToId());
    }

    @Test
    void testGetLinkNotFound() throws Exception {
        String responseBody = """
            {
              "data": {
                "links_by_pk": null
              }
            }
            """;

        when(mockResponse.statusCode()).thenReturn(200);
        when(mockResponse.body()).thenReturn(responseBody);
        when(mockHttpClient.send(any(HttpRequest.class), any(HttpResponse.BodyHandler.class)))
                .thenReturn(mockResponse);

        Optional<Link> result = client.getLink(999L);

        assertTrue(result.isEmpty());
    }

    @Test
    void testGetAllLinks() throws Exception {
        String responseBody = """
            {
              "data": {
                "links": [
                  {
                    "id": 1,
                    "from_id": 2,
                    "to_id": 3
                  },
                  {
                    "id": 4,
                    "from_id": 5,
                    "to_id": 6
                  }
                ]
              }
            }
            """;

        when(mockResponse.statusCode()).thenReturn(200);
        when(mockResponse.body()).thenReturn(responseBody);
        when(mockHttpClient.send(any(HttpRequest.class), any(HttpResponse.BodyHandler.class)))
                .thenReturn(mockResponse);

        List<Link> result = client.getAllLinks();

        assertEquals(2, result.size());
        assertEquals(1L, result.get(0).getId());
        assertEquals(4L, result.get(1).getId());
    }

    @Test
    void testHttpError() throws Exception {
        when(mockResponse.statusCode()).thenReturn(500);
        when(mockResponse.body()).thenReturn("Internal Server Error");
        when(mockHttpClient.send(any(HttpRequest.class), any(HttpResponse.BodyHandler.class)))
                .thenReturn(mockResponse);

        DoubletsException exception = assertThrows(DoubletsException.class, () -> {
            client.getOrCreate(2L, 3L);
        });

        assertTrue(exception.getMessage().contains("HTTP error: 500"));
    }

    @Test
    void testGraphQLError() throws Exception {
        String responseBody = """
            {
              "errors": [
                {
                  "message": "Validation error",
                  "extensions": {
                    "code": "validation-failed"
                  }
                }
              ]
            }
            """;

        when(mockResponse.statusCode()).thenReturn(200);
        when(mockResponse.body()).thenReturn(responseBody);
        when(mockHttpClient.send(any(HttpRequest.class), any(HttpResponse.BodyHandler.class)))
                .thenReturn(mockResponse);

        DoubletsException exception = assertThrows(DoubletsException.class, () -> {
            client.getOrCreate(2L, 3L);
        });

        assertTrue(exception.getMessage().contains("GraphQL errors"));
    }

    @Test
    void testSearchLinksWithQuery() throws Exception {
        String responseBody = """
            {
              "data": {
                "links": [
                  {
                    "id": 1,
                    "from_id": 2,
                    "to_id": 3
                  }
                ]
              }
            }
            """;

        when(mockResponse.statusCode()).thenReturn(200);
        when(mockResponse.body()).thenReturn(responseBody);
        when(mockHttpClient.send(any(HttpRequest.class), any(HttpResponse.BodyHandler.class)))
                .thenReturn(mockResponse);

        LinkQuery query = LinkQuery.builder()
                .fromId(2L)
                .limit(10)
                .build();

        List<Link> result = client.searchLinks(query);

        assertEquals(1, result.size());
        assertEquals(1L, result.get(0).getId());
        assertEquals(2L, result.get(0).getFromId());
        assertEquals(3L, result.get(0).getToId());
    }
}