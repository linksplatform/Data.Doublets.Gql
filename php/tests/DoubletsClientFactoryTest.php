<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql\Tests;

use LinksPlatform\Data\Doublets\Gql\DoubletsClient;
use LinksPlatform\Data\Doublets\Gql\DoubletsClientFactory;
use PHPUnit\Framework\TestCase;

class DoubletsClientFactoryTest extends TestCase
{
    public function testCreateLocal(): void
    {
        $client = DoubletsClientFactory::createLocal();
        
        $this->assertInstanceOf(DoubletsClient::class, $client);
        $this->assertEquals(DoubletsClientFactory::DEFAULT_LOCAL_ENDPOINT, $client->getEndpoint());
    }

    public function testCreateLocalWithCustomEndpoint(): void
    {
        $customEndpoint = 'http://custom:8080/graphql';
        $client = DoubletsClientFactory::createLocal($customEndpoint);
        
        $this->assertInstanceOf(DoubletsClient::class, $client);
        $this->assertEquals($customEndpoint, $client->getEndpoint());
    }

    public function testCreateDemo(): void
    {
        $client = DoubletsClientFactory::createDemo();
        
        $this->assertInstanceOf(DoubletsClient::class, $client);
        $this->assertEquals(DoubletsClientFactory::DEFAULT_DEMO_ENDPOINT, $client->getEndpoint());
    }

    public function testCreateDemoWithCustomEndpoint(): void
    {
        $customEndpoint = 'http://demo-custom:8080/graphql';
        $client = DoubletsClientFactory::createDemo($customEndpoint);
        
        $this->assertInstanceOf(DoubletsClient::class, $client);
        $this->assertEquals($customEndpoint, $client->getEndpoint());
    }

    public function testCreate(): void
    {
        $endpoint = 'http://example.com/graphql';
        $client = DoubletsClientFactory::create($endpoint);
        
        $this->assertInstanceOf(DoubletsClient::class, $client);
        $this->assertEquals($endpoint, $client->getEndpoint());
    }

    public function testCreateWithToken(): void
    {
        $endpoint = 'http://example.com/graphql';
        $token = 'test-token-123';
        $client = DoubletsClientFactory::createWithToken($endpoint, $token);
        
        $this->assertInstanceOf(DoubletsClient::class, $client);
        $this->assertEquals($endpoint, $client->getEndpoint());
    }

    public function testCreateWithHeaders(): void
    {
        $endpoint = 'http://example.com/graphql';
        $headers = ['X-Custom-Header' => 'custom-value'];
        $client = DoubletsClientFactory::createWithHeaders($endpoint, $headers);
        
        $this->assertInstanceOf(DoubletsClient::class, $client);
        $this->assertEquals($endpoint, $client->getEndpoint());
    }
}