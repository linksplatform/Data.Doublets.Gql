<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql;

/**
 * Factory class for creating DoubletsClient instances.
 * 
 * Provides convenience methods for creating clients with common configurations.
 */
class DoubletsClientFactory
{
    public const DEFAULT_LOCAL_ENDPOINT = 'http://localhost:60341/v1/graphql';
    public const DEFAULT_DEMO_ENDPOINT = 'http://linksplatform.ddns.net:29018/v1/graphql';

    /**
     * Create a client for local development.
     * 
     * @param string|null $endpoint Custom endpoint URL (defaults to localhost)
     * @param array $authHeaders Optional authorization headers
     * @return DoubletsClient
     */
    public static function createLocal(?string $endpoint = null, array $authHeaders = []): DoubletsClient
    {
        return new DoubletsClient($endpoint ?? self::DEFAULT_LOCAL_ENDPOINT, $authHeaders);
    }

    /**
     * Create a client for the online demo server.
     * 
     * @param string|null $endpoint Custom endpoint URL (defaults to demo server)
     * @param array $authHeaders Optional authorization headers
     * @return DoubletsClient
     */
    public static function createDemo(?string $endpoint = null, array $authHeaders = []): DoubletsClient
    {
        return new DoubletsClient($endpoint ?? self::DEFAULT_DEMO_ENDPOINT, $authHeaders);
    }

    /**
     * Create a client with custom endpoint.
     * 
     * @param string $endpoint GraphQL endpoint URL
     * @param array $authHeaders Optional authorization headers
     * @return DoubletsClient
     */
    public static function create(string $endpoint, array $authHeaders = []): DoubletsClient
    {
        return new DoubletsClient($endpoint, $authHeaders);
    }

    /**
     * Create a client with API token authentication.
     * 
     * @param string $endpoint GraphQL endpoint URL
     * @param string $token API token
     * @return DoubletsClient
     */
    public static function createWithToken(string $endpoint, string $token): DoubletsClient
    {
        return new DoubletsClient($endpoint, [
            'Authorization' => 'Bearer ' . $token
        ]);
    }

    /**
     * Create a client with custom headers.
     * 
     * @param string $endpoint GraphQL endpoint URL
     * @param array $headers Custom headers
     * @return DoubletsClient
     */
    public static function createWithHeaders(string $endpoint, array $headers): DoubletsClient
    {
        return new DoubletsClient($endpoint, $headers);
    }
}