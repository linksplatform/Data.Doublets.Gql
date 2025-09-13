<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql\Tests;

use LinksPlatform\Data\Doublets\Gql\Link;
use PHPUnit\Framework\TestCase;

class LinkTest extends TestCase
{
    public function testLinkCreation(): void
    {
        $link = new Link(1, 2, 3);
        
        $this->assertEquals(1, $link->getId());
        $this->assertEquals(2, $link->getFromId());
        $this->assertEquals(3, $link->getToId());
    }

    public function testFromArray(): void
    {
        $data = [
            'id' => 10,
            'from_id' => 20,
            'to_id' => 30
        ];
        
        $link = Link::fromArray($data);
        
        $this->assertEquals(10, $link->getId());
        $this->assertEquals(20, $link->getFromId());
        $this->assertEquals(30, $link->getToId());
    }

    public function testToArray(): void
    {
        $link = new Link(1, 2, 3);
        $expected = [
            'id' => 1,
            'from_id' => 2,
            'to_id' => 3
        ];
        
        $this->assertEquals($expected, $link->toArray());
    }

    public function testToJson(): void
    {
        $link = new Link(1, 2, 3);
        $expected = '{"id":1,"from_id":2,"to_id":3}';
        
        $this->assertEquals($expected, $link->toJson());
    }

    public function testToString(): void
    {
        $link = new Link(1, 2, 3);
        $expected = 'Link(id: 1, from: 2, to: 3)';
        
        $this->assertEquals($expected, (string)$link);
    }
}