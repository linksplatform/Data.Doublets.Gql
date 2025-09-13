package platform.data.doublets.client;

import java.util.List;
import java.util.Optional;

/**
 * Abstract interface for Platform Data Doublets operations.
 * Provides standard CRUD operations for managing links in a doublets data structure.
 */
public interface LinksClient {
    
    /**
     * Creates a new link or returns existing link if it already exists.
     *
     * @param fromId the identifier of the source node
     * @param toId   the identifier of the target node
     * @return the created or existing link
     * @throws DoubletsException if the operation fails
     */
    Link getOrCreate(long fromId, long toId) throws DoubletsException;
    
    /**
     * Creates a new link.
     *
     * @param fromId the identifier of the source node
     * @param toId   the identifier of the target node
     * @return the created link
     * @throws DoubletsException if the operation fails
     */
    Link create(long fromId, long toId) throws DoubletsException;
    
    /**
     * Updates an existing link.
     *
     * @param linkId the identifier of the link to update
     * @param newFromId the new source node identifier
     * @param newToId   the new target node identifier
     * @return the updated link
     * @throws DoubletsException if the operation fails
     */
    Link update(long linkId, long newFromId, long newToId) throws DoubletsException;
    
    /**
     * Deletes a link by its identifier.
     *
     * @param linkId the identifier of the link to delete
     * @throws DoubletsException if the operation fails
     */
    void delete(long linkId) throws DoubletsException;
    
    /**
     * Gets a link by its identifier.
     *
     * @param linkId the identifier of the link
     * @return the link if found, empty otherwise
     * @throws DoubletsException if the operation fails
     */
    Optional<Link> getLink(long linkId) throws DoubletsException;
    
    /**
     * Searches for links matching the specified criteria.
     *
     * @param query the search query
     * @return list of matching links
     * @throws DoubletsException if the operation fails
     */
    List<Link> searchLinks(LinkQuery query) throws DoubletsException;
    
    /**
     * Gets all links in the data structure.
     *
     * @return list of all links
     * @throws DoubletsException if the operation fails
     */
    List<Link> getAllLinks() throws DoubletsException;
    
    /**
     * Gets links by source node identifier.
     *
     * @param fromId the source node identifier
     * @return list of links originating from the specified node
     * @throws DoubletsException if the operation fails
     */
    List<Link> getLinksByFrom(long fromId) throws DoubletsException;
    
    /**
     * Gets links by target node identifier.
     *
     * @param toId the target node identifier
     * @return list of links targeting the specified node
     * @throws DoubletsException if the operation fails
     */
    List<Link> getLinksByTo(long toId) throws DoubletsException;
    
    /**
     * Gets the total number of links.
     *
     * @return the count of links
     * @throws DoubletsException if the operation fails
     */
    long getLinksCount() throws DoubletsException;
}