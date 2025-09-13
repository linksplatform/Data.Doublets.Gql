package platform.data.doublets.client;

/**
 * Represents a link in the doublets data structure.
 * A link connects two nodes with an identifier.
 */
public class Link {
    private final long id;
    private final long fromId;
    private final long toId;

    /**
     * Creates a new Link instance.
     *
     * @param id     the unique identifier of the link
     * @param fromId the identifier of the source node
     * @param toId   the identifier of the target node
     */
    public Link(long id, long fromId, long toId) {
        this.id = id;
        this.fromId = fromId;
        this.toId = toId;
    }

    /**
     * Gets the unique identifier of this link.
     *
     * @return the link identifier
     */
    public long getId() {
        return id;
    }

    /**
     * Gets the identifier of the source node.
     *
     * @return the source node identifier
     */
    public long getFromId() {
        return fromId;
    }

    /**
     * Gets the identifier of the target node.
     *
     * @return the target node identifier
     */
    public long getToId() {
        return toId;
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (obj == null || getClass() != obj.getClass()) return false;
        Link link = (Link) obj;
        return id == link.id && fromId == link.fromId && toId == link.toId;
    }

    @Override
    public int hashCode() {
        return Long.hashCode(id) * 31 + Long.hashCode(fromId) * 17 + Long.hashCode(toId);
    }

    @Override
    public String toString() {
        return String.format("Link{id=%d, fromId=%d, toId=%d}", id, fromId, toId);
    }
}