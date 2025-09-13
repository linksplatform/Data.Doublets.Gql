package platform.data.doublets.client;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class LinkTest {

    @Test
    void testLinkCreation() {
        Link link = new Link(1L, 2L, 3L);
        
        assertEquals(1L, link.getId());
        assertEquals(2L, link.getFromId());
        assertEquals(3L, link.getToId());
    }

    @Test
    void testLinkEquality() {
        Link link1 = new Link(1L, 2L, 3L);
        Link link2 = new Link(1L, 2L, 3L);
        Link link3 = new Link(2L, 2L, 3L);

        assertEquals(link1, link2);
        assertNotEquals(link1, link3);
        assertEquals(link1.hashCode(), link2.hashCode());
    }

    @Test
    void testLinkToString() {
        Link link = new Link(1L, 2L, 3L);
        String expected = "Link{id=1, fromId=2, toId=3}";
        
        assertEquals(expected, link.toString());
    }
}