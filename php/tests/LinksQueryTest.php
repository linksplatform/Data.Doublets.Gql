<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql\Tests;

use LinksPlatform\Data\Doublets\Gql\LinksQuery;
use PHPUnit\Framework\TestCase;

class LinksQueryTest extends TestCase
{
    public function testWhereId(): void
    {
        $query = new LinksQuery();
        $query->whereId(123);
        
        $this->assertEquals(123, $query->getId());
    }

    public function testWhereFromId(): void
    {
        $query = new LinksQuery();
        $query->whereFromId(456);
        
        $this->assertEquals(456, $query->getFromId());
    }

    public function testWhereToId(): void
    {
        $query = new LinksQuery();
        $query->whereToId(789);
        
        $this->assertEquals(789, $query->getToId());
    }

    public function testLimit(): void
    {
        $query = new LinksQuery();
        $query->limit(10);
        
        $this->assertEquals(10, $query->getLimit());
    }

    public function testOffset(): void
    {
        $query = new LinksQuery();
        $query->offset(5);
        
        $this->assertEquals(5, $query->getOffset());
    }

    public function testOrderBy(): void
    {
        $query = new LinksQuery();
        $query->orderBy('id', 'desc');
        
        $expected = [['id' => 'desc']];
        $this->assertEquals($expected, $query->getOrderBy());
    }

    public function testDistinctOn(): void
    {
        $query = new LinksQuery();
        $query->distinctOn('from_id');
        
        $expected = ['from_id'];
        $this->assertEquals($expected, $query->getDistinctOn());
    }

    public function testFluentInterface(): void
    {
        $query = (new LinksQuery())
            ->whereFromId(10)
            ->whereToId(20)
            ->limit(5)
            ->offset(2)
            ->orderBy('id', 'asc');
        
        $this->assertEquals(10, $query->getFromId());
        $this->assertEquals(20, $query->getToId());
        $this->assertEquals(5, $query->getLimit());
        $this->assertEquals(2, $query->getOffset());
        $this->assertEquals([['id' => 'asc']], $query->getOrderBy());
    }

    public function testToWhereClause(): void
    {
        $query = (new LinksQuery())
            ->whereId(1)
            ->whereFromId(10)
            ->whereToId(20);
        
        $expected = [
            'id' => ['_eq' => 1],
            'from_id' => ['_eq' => 10],
            'to_id' => ['_eq' => 20]
        ];
        
        $this->assertEquals($expected, $query->toWhereClause());
    }

    public function testToWhereClauseEmpty(): void
    {
        $query = new LinksQuery();
        
        $this->assertEquals([], $query->toWhereClause());
    }

    public function testToOrderByClause(): void
    {
        $query = (new LinksQuery())
            ->orderBy('id', 'asc')
            ->orderBy('from_id', 'desc');
        
        $expected = [
            ['id' => 'asc'],
            ['from_id' => 'desc']
        ];
        
        $this->assertEquals($expected, $query->toOrderByClause());
    }
}