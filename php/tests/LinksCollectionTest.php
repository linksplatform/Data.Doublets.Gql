<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql\Tests;

use LinksPlatform\Data\Doublets\Gql\Link;
use LinksPlatform\Data\Doublets\Gql\LinksCollection;
use PHPUnit\Framework\TestCase;

class LinksCollectionTest extends TestCase
{
    private LinksCollection $collection;
    private Link $link1;
    private Link $link2;
    private Link $link3;

    protected function setUp(): void
    {
        $this->link1 = new Link(1, 10, 20);
        $this->link2 = new Link(2, 20, 30);
        $this->link3 = new Link(3, 10, 30);
        
        $this->collection = new LinksCollection([
            $this->link1,
            $this->link2,
            $this->link3
        ]);
    }

    public function testCount(): void
    {
        $this->assertEquals(3, $this->collection->count());
        $this->assertEquals(3, count($this->collection));
    }

    public function testGet(): void
    {
        $this->assertEquals($this->link1, $this->collection->get(0));
        $this->assertEquals($this->link2, $this->collection->get(1));
        $this->assertEquals($this->link3, $this->collection->get(2));
        $this->assertNull($this->collection->get(5));
    }

    public function testFirst(): void
    {
        $this->assertEquals($this->link1, $this->collection->first());
        
        $emptyCollection = new LinksCollection();
        $this->assertNull($emptyCollection->first());
    }

    public function testLast(): void
    {
        $this->assertEquals($this->link3, $this->collection->last());
        
        $emptyCollection = new LinksCollection();
        $this->assertNull($emptyCollection->last());
    }

    public function testFindById(): void
    {
        $this->assertEquals($this->link2, $this->collection->findById(2));
        $this->assertNull($this->collection->findById(999));
    }

    public function testFindByFromId(): void
    {
        $results = $this->collection->findByFromId(10);
        
        $this->assertEquals(2, $results->count());
        $this->assertEquals($this->link1, $results->get(0));
        $this->assertEquals($this->link3, $results->get(1));
    }

    public function testFindByToId(): void
    {
        $results = $this->collection->findByToId(30);
        
        $this->assertEquals(2, $results->count());
        $this->assertEquals($this->link2, $results->get(0));
        $this->assertEquals($this->link3, $results->get(1));
    }

    public function testFilter(): void
    {
        $filtered = $this->collection->filter(fn(Link $link) => $link->getFromId() === 10);
        
        $this->assertEquals(2, $filtered->count());
        $this->assertEquals($this->link1, $filtered->get(0));
        $this->assertEquals($this->link3, $filtered->get(1));
    }

    public function testMap(): void
    {
        $ids = $this->collection->map(fn(Link $link) => $link->getId());
        
        $this->assertEquals([1, 2, 3], $ids);
    }

    public function testIsEmpty(): void
    {
        $this->assertFalse($this->collection->isEmpty());
        
        $emptyCollection = new LinksCollection();
        $this->assertTrue($emptyCollection->isEmpty());
    }

    public function testIteration(): void
    {
        $links = [];
        foreach ($this->collection as $link) {
            $links[] = $link;
        }
        
        $this->assertEquals([$this->link1, $this->link2, $this->link3], $links);
    }

    public function testFromArray(): void
    {
        $data = [
            ['id' => 1, 'from_id' => 10, 'to_id' => 20],
            ['id' => 2, 'from_id' => 20, 'to_id' => 30]
        ];
        
        $collection = LinksCollection::fromArray($data);
        
        $this->assertEquals(2, $collection->count());
        $this->assertEquals(1, $collection->get(0)->getId());
        $this->assertEquals(2, $collection->get(1)->getId());
    }

    public function testToArray(): void
    {
        $expected = [
            ['id' => 1, 'from_id' => 10, 'to_id' => 20],
            ['id' => 2, 'from_id' => 20, 'to_id' => 30],
            ['id' => 3, 'from_id' => 10, 'to_id' => 30]
        ];
        
        $this->assertEquals($expected, $this->collection->toArray());
    }

    public function testJsonSerialization(): void
    {
        $expected = [
            ['id' => 1, 'from_id' => 10, 'to_id' => 20],
            ['id' => 2, 'from_id' => 20, 'to_id' => 30],
            ['id' => 3, 'from_id' => 10, 'to_id' => 30]
        ];
        
        $this->assertEquals($expected, $this->collection->jsonSerialize());
    }
}