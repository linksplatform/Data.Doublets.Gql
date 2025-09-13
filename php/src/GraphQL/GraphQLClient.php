<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql\GraphQL;

use GraphQL\Client;
use GraphQL\Exception\QueryError;
use GraphQL\Query;
use GraphQL\Mutation;
use LinksPlatform\Data\Doublets\Gql\Exception\GraphQLException;

/**
 * GraphQL client wrapper for Doublets operations.
 * 
 * Provides a layer of abstraction over the gmostafa/php-graphql-client
 * library specifically for Doublets GraphQL operations.
 */
class GraphQLClient
{
    private Client $client;
    private string $endpoint;

    public function __construct(string $endpoint, array $authHeaders = [])
    {
        $this->endpoint = $endpoint;
        $this->client = new Client($endpoint, $authHeaders);
    }

    /**
     * Execute a GraphQL query and return the response data.
     * 
     * @param Query $query
     * @return array
     * @throws GraphQLException
     */
    public function runQuery(Query $query): array
    {
        try {
            $response = $this->client->runQuery($query);
            
            if ($response->hasErrors()) {
                throw new GraphQLException(
                    'GraphQL query failed: ' . implode(', ', $response->getErrorMessages())
                );
            }
            
            return $response->getData();
        } catch (QueryError $e) {
            throw new GraphQLException('GraphQL query error: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Execute a GraphQL mutation and return the response data.
     * 
     * @param Mutation $mutation
     * @return array
     * @throws GraphQLException
     */
    public function runMutation(Mutation $mutation): array
    {
        try {
            $response = $this->client->runQuery($mutation);
            
            if ($response->hasErrors()) {
                throw new GraphQLException(
                    'GraphQL mutation failed: ' . implode(', ', $response->getErrorMessages())
                );
            }
            
            return $response->getData();
        } catch (QueryError $e) {
            throw new GraphQLException('GraphQL mutation error: ' . $e->getMessage(), 0, $e);
        }
    }

    /**
     * Get the GraphQL endpoint URL.
     * 
     * @return string
     */
    public function getEndpoint(): string
    {
        return $this->endpoint;
    }
}