<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql;

/**
 * Query builder for Links operations.
 * 
 * Provides a fluent interface for building complex queries
 * that will be converted to GraphQL queries.
 */
class LinksQuery
{
    private ?int $id = null;
    private ?int $fromId = null;
    private ?int $toId = null;
    private ?int $limit = null;
    private ?int $offset = null;
    private array $orderBy = [];
    private array $distinctOn = [];

    /**
     * Filter by link ID.
     * 
     * @param int $id The link ID
     * @return self
     */
    public function whereId(int $id): self
    {
        $this->id = $id;
        return $this;
    }

    /**
     * Filter by source node ID.
     * 
     * @param int $fromId The source node ID
     * @return self
     */
    public function whereFromId(int $fromId): self
    {
        $this->fromId = $fromId;
        return $this;
    }

    /**
     * Filter by target node ID.
     * 
     * @param int $toId The target node ID
     * @return self
     */
    public function whereToId(int $toId): self
    {
        $this->toId = $toId;
        return $this;
    }

    /**
     * Limit the number of results.
     * 
     * @param int $limit Maximum number of results
     * @return self
     */
    public function limit(int $limit): self
    {
        $this->limit = $limit;
        return $this;
    }

    /**
     * Skip a number of results.
     * 
     * @param int $offset Number of results to skip
     * @return self
     */
    public function offset(int $offset): self
    {
        $this->offset = $offset;
        return $this;
    }

    /**
     * Order results by a field.
     * 
     * @param string $field Field name (id, from_id, to_id)
     * @param string $direction Direction (asc, desc)
     * @return self
     */
    public function orderBy(string $field, string $direction = 'asc'): self
    {
        $this->orderBy[] = [$field => $direction];
        return $this;
    }

    /**
     * Select distinct values based on a field.
     * 
     * @param string $field Field name
     * @return self
     */
    public function distinctOn(string $field): self
    {
        $this->distinctOn[] = $field;
        return $this;
    }

    /**
     * Get the ID filter.
     * 
     * @return int|null
     */
    public function getId(): ?int
    {
        return $this->id;
    }

    /**
     * Get the from_id filter.
     * 
     * @return int|null
     */
    public function getFromId(): ?int
    {
        return $this->fromId;
    }

    /**
     * Get the to_id filter.
     * 
     * @return int|null
     */
    public function getToId(): ?int
    {
        return $this->toId;
    }

    /**
     * Get the limit.
     * 
     * @return int|null
     */
    public function getLimit(): ?int
    {
        return $this->limit;
    }

    /**
     * Get the offset.
     * 
     * @return int|null
     */
    public function getOffset(): ?int
    {
        return $this->offset;
    }

    /**
     * Get the order by clauses.
     * 
     * @return array
     */
    public function getOrderBy(): array
    {
        return $this->orderBy;
    }

    /**
     * Get the distinct on fields.
     * 
     * @return array
     */
    public function getDistinctOn(): array
    {
        return $this->distinctOn;
    }

    /**
     * Convert query to GraphQL where clause array.
     * 
     * @return array
     */
    public function toWhereClause(): array
    {
        $where = [];
        
        if ($this->id !== null) {
            $where['id'] = ['_eq' => $this->id];
        }
        
        if ($this->fromId !== null) {
            $where['from_id'] = ['_eq' => $this->fromId];
        }
        
        if ($this->toId !== null) {
            $where['to_id'] = ['_eq' => $this->toId];
        }
        
        return $where;
    }

    /**
     * Convert order by to GraphQL format.
     * 
     * @return array
     */
    public function toOrderByClause(): array
    {
        return $this->orderBy;
    }
}