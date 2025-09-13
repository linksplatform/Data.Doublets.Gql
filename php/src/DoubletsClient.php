<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql;

use LinksPlatform\Data\Doublets\Gql\Exception\GraphQLException;
use LinksPlatform\Data\Doublets\Gql\Exception\LinksException;
use LinksPlatform\Data\Doublets\Gql\GraphQL\GraphQLClient;
use LinksPlatform\Data\Doublets\Gql\GraphQL\QueryBuilder;

/**
 * Main Doublets client that provides native PHP API for Doublets operations.
 * 
 * This class implements the LinksInterface and provides a native PHP style API
 * that wraps the GraphQL operations. It can be easily swapped with a native
 * library implementation when available.
 */
class DoubletsClient implements LinksInterface
{
    private GraphQLClient $graphqlClient;
    private QueryBuilder $queryBuilder;

    public function __construct(string $graphqlEndpoint, array $authHeaders = [])
    {
        $this->graphqlClient = new GraphQLClient($graphqlEndpoint, $authHeaders);
        $this->queryBuilder = new QueryBuilder();
    }

    /**
     * Create a new link between two nodes.
     * 
     * @param int $fromId The source node ID
     * @param int $toId The target node ID
     * @return Link The created link
     * @throws LinksException
     */
    public function create(int $fromId, int $toId): Link
    {
        try {
            $mutation = $this->queryBuilder->buildInsertLinkMutation($fromId, $toId);
            $response = $this->graphqlClient->runMutation($mutation);
            
            if (!isset($response['insert_links_one'])) {
                throw new LinksException('Failed to create link: invalid response structure');
            }
            
            return Link::fromArray($response['insert_links_one']);
        } catch (GraphQLException $e) {
            throw new LinksException('Failed to create link: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Get or create a link between two nodes.
     * If a link already exists, return it; otherwise create a new one.
     * 
     * @param int $fromId The source node ID
     * @param int $toId The target node ID
     * @return Link The existing or newly created link
     * @throws LinksException
     */
    public function getOrCreate(int $fromId, int $toId): Link
    {
        // First try to find existing link
        $query = (new LinksQuery())
            ->whereFromId($fromId)
            ->whereToId($toId)
            ->limit(1);
        
        $existing = $this->find($query);
        
        if (!$existing->isEmpty()) {
            return $existing->first();
        }
        
        // Create new link if none exists
        return $this->create($fromId, $toId);
    }

    /**
     * Get a link by its ID.
     * 
     * @param int $id The link ID
     * @return Link|null The link if found, null otherwise
     * @throws LinksException
     */
    public function get(int $id): ?Link
    {
        try {
            $query = $this->queryBuilder->buildLinkByIdQuery($id);
            $response = $this->graphqlClient->runQuery($query);
            
            if (!isset($response['links_by_pk']) || $response['links_by_pk'] === null) {
                return null;
            }
            
            return Link::fromArray($response['links_by_pk']);
        } catch (GraphQLException $e) {
            throw new LinksException('Failed to get link: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Update an existing link.
     * 
     * @param int $id The link ID to update
     * @param int $fromId The new source node ID
     * @param int $toId The new target node ID
     * @return Link The updated link
     * @throws LinksException
     */
    public function update(int $id, int $fromId, int $toId): Link
    {
        try {
            $whereQuery = (new LinksQuery())->whereId($id);
            $mutation = $this->queryBuilder->buildUpdateLinksMutation($whereQuery, $fromId, $toId);
            $response = $this->graphqlClient->runMutation($mutation);
            
            if (!isset($response['update_links']['returning']) || empty($response['update_links']['returning'])) {
                throw new LinksException('Failed to update link: no link found with ID ' . $id);
            }
            
            return Link::fromArray($response['update_links']['returning'][0]);
        } catch (GraphQLException $e) {
            throw new LinksException('Failed to update link: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Delete a link by its ID.
     * 
     * @param int $id The link ID to delete
     * @return bool True if deletion was successful, false otherwise
     * @throws LinksException
     */
    public function delete(int $id): bool
    {
        try {
            $whereQuery = (new LinksQuery())->whereId($id);
            $mutation = $this->queryBuilder->buildDeleteLinksMutation($whereQuery);
            $response = $this->graphqlClient->runMutation($mutation);
            
            return isset($response['delete_links']['returning']) && !empty($response['delete_links']['returning']);
        } catch (GraphQLException $e) {
            throw new LinksException('Failed to delete link: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Find links by criteria.
     * 
     * @param LinksQuery $query The query criteria
     * @return LinksCollection Collection of matching links
     * @throws LinksException
     */
    public function find(LinksQuery $query): LinksCollection
    {
        try {
            $graphqlQuery = $this->queryBuilder->buildLinksQuery($query);
            $response = $this->graphqlClient->runQuery($graphqlQuery);
            
            if (!isset($response['links'])) {
                throw new LinksException('Failed to find links: invalid response structure');
            }
            
            return LinksCollection::fromArray($response['links']);
        } catch (GraphQLException $e) {
            throw new LinksException('Failed to find links: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Get all links.
     * 
     * @param int|null $limit Maximum number of links to return
     * @param int|null $offset Number of links to skip
     * @return LinksCollection Collection of all links
     * @throws LinksException
     */
    public function all(?int $limit = null, ?int $offset = null): LinksCollection
    {
        $query = new LinksQuery();
        
        if ($limit !== null) {
            $query->limit($limit);
        }
        
        if ($offset !== null) {
            $query->offset($offset);
        }
        
        return $this->find($query);
    }

    /**
     * Count links matching criteria.
     * 
     * @param LinksQuery|null $query The query criteria (null for count all)
     * @return int Number of matching links
     * @throws LinksException
     */
    public function count(?LinksQuery $query = null): int
    {
        try {
            $graphqlQuery = $this->queryBuilder->buildLinksAggregateQuery($query);
            $response = $this->graphqlClient->runQuery($graphqlQuery);
            
            if (!isset($response['links_aggregate']['aggregate']['count'])) {
                throw new LinksException('Failed to count links: invalid response structure');
            }
            
            return (int)$response['links_aggregate']['aggregate']['count'];
        } catch (GraphQLException $e) {
            throw new LinksException('Failed to count links: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Create multiple links in a single operation.
     * 
     * @param array $links Array of ['from_id' => int, 'to_id' => int]
     * @return LinksCollection Collection of created links
     * @throws LinksException
     */
    public function createMany(array $links): LinksCollection
    {
        try {
            $mutation = $this->queryBuilder->buildInsertLinksMutation($links);
            $response = $this->graphqlClient->runMutation($mutation);
            
            if (!isset($response['insert_links']['returning'])) {
                throw new LinksException('Failed to create links: invalid response structure');
            }
            
            return LinksCollection::fromArray($response['insert_links']['returning']);
        } catch (GraphQLException $e) {
            throw new LinksException('Failed to create links: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Delete multiple links matching criteria.
     * 
     * @param LinksQuery $query The query criteria for links to delete
     * @return LinksCollection Collection of deleted links
     * @throws LinksException
     */
    public function deleteMany(LinksQuery $query): LinksCollection
    {
        try {
            $mutation = $this->queryBuilder->buildDeleteLinksMutation($query);
            $response = $this->graphqlClient->runMutation($mutation);
            
            if (!isset($response['delete_links']['returning'])) {
                throw new LinksException('Failed to delete links: invalid response structure');
            }
            
            return LinksCollection::fromArray($response['delete_links']['returning']);
        } catch (GraphQLException $e) {
            throw new LinksException('Failed to delete links: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Get the GraphQL endpoint URL.
     * 
     * @return string
     */
    public function getEndpoint(): string
    {
        return $this->graphqlClient->getEndpoint();
    }
}