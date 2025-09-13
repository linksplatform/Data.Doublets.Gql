package platform.data.doublets.client;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class LinkQueryTest {

    @Test
    void testEmptyQuery() {
        LinkQuery query = LinkQuery.builder().build();
        
        assertTrue(query.getId().isEmpty());
        assertTrue(query.getFromId().isEmpty());
        assertTrue(query.getToId().isEmpty());
        assertTrue(query.getLimit().isEmpty());
        assertTrue(query.getOffset().isEmpty());
        assertTrue(query.getSortField().isEmpty());
        assertTrue(query.getSortOrder().isEmpty());
    }

    @Test
    void testQueryWithId() {
        LinkQuery query = LinkQuery.builder()
                .id(123L)
                .build();
        
        assertTrue(query.getId().isPresent());
        assertEquals(123L, query.getId().get());
    }

    @Test
    void testQueryWithFromAndTo() {
        LinkQuery query = LinkQuery.builder()
                .fromId(1L)
                .toId(2L)
                .build();
        
        assertTrue(query.getFromId().isPresent());
        assertTrue(query.getToId().isPresent());
        assertEquals(1L, query.getFromId().get());
        assertEquals(2L, query.getToId().get());
    }

    @Test
    void testQueryWithPagination() {
        LinkQuery query = LinkQuery.builder()
                .limit(10)
                .offset(20)
                .build();
        
        assertTrue(query.getLimit().isPresent());
        assertTrue(query.getOffset().isPresent());
        assertEquals(10, query.getLimit().get());
        assertEquals(20, query.getOffset().get());
    }

    @Test
    void testQueryWithSorting() {
        LinkQuery query = LinkQuery.builder()
                .sortBy(LinkQuery.SortField.ID, LinkQuery.SortOrder.DESC)
                .build();
        
        assertTrue(query.getSortField().isPresent());
        assertTrue(query.getSortOrder().isPresent());
        assertEquals(LinkQuery.SortField.ID, query.getSortField().get());
        assertEquals(LinkQuery.SortOrder.DESC, query.getSortOrder().get());
    }

    @Test
    void testComplexQuery() {
        LinkQuery query = LinkQuery.builder()
                .fromId(1L)
                .toId(2L)
                .limit(5)
                .offset(10)
                .sortBy(LinkQuery.SortField.FROM_ID, LinkQuery.SortOrder.ASC)
                .build();
        
        assertEquals(1L, query.getFromId().get());
        assertEquals(2L, query.getToId().get());
        assertEquals(5, query.getLimit().get());
        assertEquals(10, query.getOffset().get());
        assertEquals(LinkQuery.SortField.FROM_ID, query.getSortField().get());
        assertEquals(LinkQuery.SortOrder.ASC, query.getSortOrder().get());
    }
}