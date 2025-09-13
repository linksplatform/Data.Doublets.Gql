<?php

require_once __DIR__ . '/../vendor/autoload.php';

use LinksPlatform\Data\Doublets\Gql\DoubletsClientFactory;
use LinksPlatform\Data\Doublets\Gql\DoubletsClient;

echo "=== PHP Doublets Client - Configuration Examples ===\n\n";

// Example 1: Local development configuration
echo "1. Local Development Configuration\n";
echo "==================================\n";

$localClient = DoubletsClientFactory::createLocal();
echo "Local endpoint: " . $localClient->getEndpoint() . "\n";
echo "Use this for local development when running the server locally.\n\n";

// Example 2: Demo server configuration
echo "2. Demo Server Configuration\n";
echo "============================\n";

$demoClient = DoubletsClientFactory::createDemo();
echo "Demo endpoint: " . $demoClient->getEndpoint() . "\n";
echo "Use this to test against the public demo server.\n\n";

// Example 3: Custom endpoint
echo "3. Custom Endpoint Configuration\n";
echo "=================================\n";

$customEndpoint = 'http://my-custom-server:8080/v1/graphql';
$customClient = DoubletsClientFactory::create($customEndpoint);
echo "Custom endpoint: " . $customClient->getEndpoint() . "\n";
echo "Use this when you have your own server deployment.\n\n";

// Example 4: Authentication with Bearer token
echo "4. Authentication Configuration\n";
echo "===============================\n";

$securedEndpoint = 'https://secured-server.example.com/v1/graphql';
$apiToken = 'your-api-token-here';
$authenticatedClient = DoubletsClientFactory::createWithToken($securedEndpoint, $apiToken);
echo "Secured endpoint: " . $authenticatedClient->getEndpoint() . "\n";
echo "This configuration includes Bearer token authentication.\n\n";

// Example 5: Custom headers configuration
echo "5. Custom Headers Configuration\n";
echo "================================\n";

$customHeaders = [
    'Authorization' => 'Bearer custom-token-123',
    'X-API-Key' => 'api-key-456',
    'X-Client-Version' => '1.0.0',
    'X-Request-ID' => uniqid('req_'),
    'User-Agent' => 'DoubletsClient-PHP/1.0'
];

$headerClient = DoubletsClientFactory::createWithHeaders($securedEndpoint, $customHeaders);
echo "Endpoint with custom headers: " . $headerClient->getEndpoint() . "\n";
echo "Custom headers included:\n";
foreach ($customHeaders as $name => $value) {
    echo "  - {$name}: {$value}\n";
}
echo "\n";

// Example 6: Environment-based configuration
echo "6. Environment-Based Configuration\n";
echo "===================================\n";

function createClientFromEnvironment(): DoubletsClient
{
    // In a real application, you'd use $_ENV or getenv()
    // For this example, we'll simulate environment variables
    $env = [
        'DOUBLETS_ENDPOINT' => 'http://localhost:60341/v1/graphql',
        'DOUBLETS_API_TOKEN' => null,
        'DOUBLETS_ENVIRONMENT' => 'development'
    ];
    
    $endpoint = $env['DOUBLETS_ENDPOINT'];
    $token = $env['DOUBLETS_API_TOKEN'];
    $environment = $env['DOUBLETS_ENVIRONMENT'];
    
    echo "Environment: {$environment}\n";
    echo "Endpoint: {$endpoint}\n";
    
    if ($token) {
        echo "Using API token authentication\n";
        return DoubletsClientFactory::createWithToken($endpoint, $token);
    } else {
        echo "No authentication configured\n";
        return DoubletsClientFactory::create($endpoint);
    }
}

$envClient = createClientFromEnvironment();
echo "\n";

// Example 7: Configuration validation
echo "7. Configuration Validation\n";
echo "============================\n";

function validateClientConfiguration(DoubletsClient $client): array
{
    $issues = [];
    $endpoint = $client->getEndpoint();
    
    // Check if endpoint is valid URL
    if (!filter_var($endpoint, FILTER_VALIDATE_URL)) {
        $issues[] = "Invalid endpoint URL: {$endpoint}";
    }
    
    // Check if endpoint uses HTTPS in production
    if (!str_starts_with($endpoint, 'https://') && 
        !str_starts_with($endpoint, 'http://localhost') &&
        !str_starts_with($endpoint, 'http://127.0.0.1')) {
        $issues[] = "Consider using HTTPS for production: {$endpoint}";
    }
    
    // Check if endpoint path looks correct
    if (!str_contains($endpoint, '/graphql')) {
        $issues[] = "Endpoint might be missing GraphQL path: {$endpoint}";
    }
    
    return $issues;
}

$clients = [
    'Local' => $localClient,
    'Demo' => $demoClient,
    'Custom' => $customClient,
    'Authenticated' => $authenticatedClient
];

foreach ($clients as $name => $client) {
    $issues = validateClientConfiguration($client);
    echo "{$name} client: ";
    if (empty($issues)) {
        echo "✅ Configuration looks good\n";
    } else {
        echo "⚠️  Issues found:\n";
        foreach ($issues as $issue) {
            echo "  - {$issue}\n";
        }
    }
}
echo "\n";

// Example 8: Production-ready configuration helper
echo "8. Production Configuration Helper\n";
echo "===================================\n";

class DoubletsConfig
{
    public static function fromEnvironment(): DoubletsClient
    {
        $endpoint = $_ENV['DOUBLETS_ENDPOINT'] ?? 'http://localhost:60341/v1/graphql';
        $token = $_ENV['DOUBLETS_API_TOKEN'] ?? null;
        $timeout = (int)($_ENV['DOUBLETS_TIMEOUT'] ?? 30);
        $retries = (int)($_ENV['DOUBLETS_RETRIES'] ?? 3);
        
        $headers = [];
        
        if ($token) {
            $headers['Authorization'] = "Bearer {$token}";
        }
        
        // Add operational headers
        $headers['X-Client-Name'] = 'DoubletsClient-PHP';
        $headers['X-Client-Version'] = '1.0.0';
        $headers['X-Request-Timeout'] = (string)$timeout;
        
        return DoubletsClientFactory::createWithHeaders($endpoint, $headers);
    }
    
    public static function forDevelopment(): DoubletsClient
    {
        return DoubletsClientFactory::createLocal();
    }
    
    public static function forTesting(): DoubletsClient
    {
        return DoubletsClientFactory::createDemo();
    }
    
    public static function forProduction(string $endpoint, string $apiToken): DoubletsClient
    {
        if (!str_starts_with($endpoint, 'https://')) {
            throw new InvalidArgumentException('Production endpoint must use HTTPS');
        }
        
        return DoubletsClientFactory::createWithHeaders($endpoint, [
            'Authorization' => "Bearer {$apiToken}",
            'X-Client-Name' => 'DoubletsClient-PHP',
            'X-Client-Version' => '1.0.0',
            'X-Environment' => 'production'
        ]);
    }
}

// Demo the config helper
echo "Configuration helper examples:\n";
echo "- Development: " . DoubletsConfig::forDevelopment()->getEndpoint() . "\n";
echo "- Testing: " . DoubletsConfig::forTesting()->getEndpoint() . "\n";

// Production example (would throw exception due to non-HTTPS)
try {
    $prodClient = DoubletsConfig::forProduction('http://prod.example.com/graphql', 'token');
} catch (Exception $e) {
    echo "- Production: " . $e->getMessage() . "\n";
}

echo "\n";

echo "=== Configuration Examples Complete ===\n";
echo "Choose the configuration method that best fits your use case!\n";