<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql;

/**
 * Interface defining the core Doublets operations for Links management.
 * 
 * A Link represents a triplet with id, from_id, and to_id values.
 * This interface provides standard CRUD operations in native PHP style.
 */
interface LinksInterface
{
    /**
     * Create a new link between two nodes.
     * 
     * @param int $fromId The source node ID
     * @param int $toId The target node ID
     * @return Link The created link
     */
    public function create(int $fromId, int $toId): Link;

    /**
     * Get or create a link between two nodes.
     * If a link already exists, return it; otherwise create a new one.
     * 
     * @param int $fromId The source node ID
     * @param int $toId The target node ID
     * @return Link The existing or newly created link
     */
    public function getOrCreate(int $fromId, int $toId): Link;

    /**
     * Get a link by its ID.
     * 
     * @param int $id The link ID
     * @return Link|null The link if found, null otherwise
     */
    public function get(int $id): ?Link;

    /**
     * Update an existing link.
     * 
     * @param int $id The link ID to update
     * @param int $fromId The new source node ID
     * @param int $toId The new target node ID
     * @return Link The updated link
     */
    public function update(int $id, int $fromId, int $toId): Link;

    /**
     * Delete a link by its ID.
     * 
     * @param int $id The link ID to delete
     * @return bool True if deletion was successful, false otherwise
     */
    public function delete(int $id): bool;

    /**
     * Find links by criteria.
     * 
     * @param LinksQuery $query The query criteria
     * @return LinksCollection Collection of matching links
     */
    public function find(LinksQuery $query): LinksCollection;

    /**
     * Get all links.
     * 
     * @param int|null $limit Maximum number of links to return
     * @param int|null $offset Number of links to skip
     * @return LinksCollection Collection of all links
     */
    public function all(?int $limit = null, ?int $offset = null): LinksCollection;

    /**
     * Count links matching criteria.
     * 
     * @param LinksQuery|null $query The query criteria (null for count all)
     * @return int Number of matching links
     */
    public function count(?LinksQuery $query = null): int;
}