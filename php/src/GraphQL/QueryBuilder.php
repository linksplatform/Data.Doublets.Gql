<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql\GraphQL;

use GraphQL\Query;
use GraphQL\Mutation;
use GraphQL\Variable;
use LinksPlatform\Data\Doublets\Gql\LinksQuery;

/**
 * Builds GraphQL queries and mutations for Doublets operations.
 * 
 * Converts PHP query objects to GraphQL query/mutation objects
 * compatible with the Doublets GraphQL schema.
 */
class QueryBuilder
{
    /**
     * Build a GraphQL query to select links.
     * 
     * @param LinksQuery $linksQuery
     * @return Query
     */
    public function buildLinksQuery(LinksQuery $linksQuery): Query
    {
        $query = new Query('links');
        
        // Add where clause if filters are specified
        $whereClause = $linksQuery->toWhereClause();
        if (!empty($whereClause)) {
            $query->setArguments(['where' => $whereClause]);
        }
        
        // Add limit if specified
        if ($linksQuery->getLimit() !== null) {
            $query->setArguments(['limit' => $linksQuery->getLimit()]);
        }
        
        // Add offset if specified
        if ($linksQuery->getOffset() !== null) {
            $query->setArguments(['offset' => $linksQuery->getOffset()]);
        }
        
        // Add order by if specified
        if (!empty($linksQuery->getOrderBy())) {
            $query->setArguments(['order_by' => $linksQuery->getOrderBy()]);
        }
        
        // Add distinct on if specified
        if (!empty($linksQuery->getDistinctOn())) {
            $query->setArguments(['distinct_on' => $linksQuery->getDistinctOn()]);
        }
        
        // Select fields
        $query->setSelectionSet([
            'id',
            'from_id',
            'to_id'
        ]);
        
        return $query;
    }

    /**
     * Build a GraphQL query to count links.
     * 
     * @param LinksQuery|null $linksQuery
     * @return Query
     */
    public function buildLinksAggregateQuery(?LinksQuery $linksQuery = null): Query
    {
        $query = new Query('links_aggregate');
        
        // Add where clause if filters are specified
        if ($linksQuery !== null) {
            $whereClause = $linksQuery->toWhereClause();
            if (!empty($whereClause)) {
                $query->setArguments(['where' => $whereClause]);
            }
        }
        
        // Select aggregate fields
        $query->setSelectionSet([
            'aggregate' => [
                'count'
            ]
        ]);
        
        return $query;
    }

    /**
     * Build a GraphQL query to get a single link by ID.
     * 
     * @param int $id
     * @return Query
     */
    public function buildLinkByIdQuery(int $id): Query
    {
        $query = new Query('links_by_pk');
        $query->setArguments(['id' => $id]);
        $query->setSelectionSet([
            'id',
            'from_id',
            'to_id'
        ]);
        
        return $query;
    }

    /**
     * Build a GraphQL mutation to insert a single link.
     * 
     * @param int $fromId
     * @param int $toId
     * @return Mutation
     */
    public function buildInsertLinkMutation(int $fromId, int $toId): Mutation
    {
        $mutation = new Mutation('insert_links_one');
        $mutation->setArguments([
            'object' => [
                'from_id' => $fromId,
                'to_id' => $toId
            ]
        ]);
        $mutation->setSelectionSet([
            'id',
            'from_id',
            'to_id'
        ]);
        
        return $mutation;
    }

    /**
     * Build a GraphQL mutation to insert multiple links.
     * 
     * @param array $links Array of ['from_id' => int, 'to_id' => int]
     * @return Mutation
     */
    public function buildInsertLinksMutation(array $links): Mutation
    {
        $mutation = new Mutation('insert_links');
        $mutation->setArguments(['objects' => $links]);
        $mutation->setSelectionSet([
            'returning' => [
                'id',
                'from_id',
                'to_id'
            ]
        ]);
        
        return $mutation;
    }

    /**
     * Build a GraphQL mutation to update links.
     * 
     * @param LinksQuery $whereQuery
     * @param int $newFromId
     * @param int $newToId
     * @return Mutation
     */
    public function buildUpdateLinksMutation(LinksQuery $whereQuery, int $newFromId, int $newToId): Mutation
    {
        $mutation = new Mutation('update_links');
        $mutation->setArguments([
            'where' => $whereQuery->toWhereClause(),
            '_set' => [
                'from_id' => $newFromId,
                'to_id' => $newToId
            ]
        ]);
        $mutation->setSelectionSet([
            'returning' => [
                'id',
                'from_id',
                'to_id'
            ]
        ]);
        
        return $mutation;
    }

    /**
     * Build a GraphQL mutation to delete links.
     * 
     * @param LinksQuery $whereQuery
     * @return Mutation
     */
    public function buildDeleteLinksMutation(LinksQuery $whereQuery): Mutation
    {
        $mutation = new Mutation('delete_links');
        $mutation->setArguments([
            'where' => $whereQuery->toWhereClause()
        ]);
        $mutation->setSelectionSet([
            'returning' => [
                'id',
                'from_id',
                'to_id'
            ]
        ]);
        
        return $mutation;
    }
}