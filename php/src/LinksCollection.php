<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql;

use ArrayIterator;
use Countable;
use IteratorAggregate;
use JsonSerializable;

/**
 * Collection of Links with iteration and utility methods.
 * 
 * Provides a native PHP interface for working with collections
 * of links returned from GraphQL queries.
 */
class LinksCollection implements IteratorAggregate, Countable, JsonSerializable
{
    private array $links;

    /**
     * @param Link[] $links Array of Link objects
     */
    public function __construct(array $links = [])
    {
        $this->links = $links;
    }

    /**
     * Create a collection from GraphQL response data.
     * 
     * @param array $data Array of link data
     * @return self
     */
    public static function fromArray(array $data): self
    {
        $links = [];
        foreach ($data as $linkData) {
            $links[] = Link::fromArray($linkData);
        }
        return new self($links);
    }

    /**
     * Add a link to the collection.
     * 
     * @param Link $link
     * @return self
     */
    public function add(Link $link): self
    {
        $this->links[] = $link;
        return $this;
    }

    /**
     * Get a link by its index in the collection.
     * 
     * @param int $index
     * @return Link|null
     */
    public function get(int $index): ?Link
    {
        return $this->links[$index] ?? null;
    }

    /**
     * Get the first link in the collection.
     * 
     * @return Link|null
     */
    public function first(): ?Link
    {
        return $this->links[0] ?? null;
    }

    /**
     * Get the last link in the collection.
     * 
     * @return Link|null
     */
    public function last(): ?Link
    {
        $count = count($this->links);
        return $count > 0 ? $this->links[$count - 1] : null;
    }

    /**
     * Filter the collection by a callback.
     * 
     * @param callable $callback
     * @return self
     */
    public function filter(callable $callback): self
    {
        return new self(array_filter($this->links, $callback));
    }

    /**
     * Map the collection to a new array.
     * 
     * @param callable $callback
     * @return array
     */
    public function map(callable $callback): array
    {
        return array_map($callback, $this->links);
    }

    /**
     * Find a link by ID.
     * 
     * @param int $id
     * @return Link|null
     */
    public function findById(int $id): ?Link
    {
        foreach ($this->links as $link) {
            if ($link->getId() === $id) {
                return $link;
            }
        }
        return null;
    }

    /**
     * Find links by from_id.
     * 
     * @param int $fromId
     * @return self
     */
    public function findByFromId(int $fromId): self
    {
        return $this->filter(fn(Link $link) => $link->getFromId() === $fromId);
    }

    /**
     * Find links by to_id.
     * 
     * @param int $toId
     * @return self
     */
    public function findByToId(int $toId): self
    {
        return $this->filter(fn(Link $link) => $link->getToId() === $toId);
    }

    /**
     * Check if the collection is empty.
     * 
     * @return bool
     */
    public function isEmpty(): bool
    {
        return empty($this->links);
    }

    /**
     * Convert collection to array.
     * 
     * @return array
     */
    public function toArray(): array
    {
        return array_map(fn(Link $link) => $link->toArray(), $this->links);
    }

    /**
     * Get raw links array.
     * 
     * @return Link[]
     */
    public function getLinks(): array
    {
        return $this->links;
    }

    /**
     * Count the number of links.
     * 
     * @return int
     */
    public function count(): int
    {
        return count($this->links);
    }

    /**
     * Get iterator for foreach loops.
     * 
     * @return ArrayIterator
     */
    public function getIterator(): ArrayIterator
    {
        return new ArrayIterator($this->links);
    }

    /**
     * JSON serialization.
     * 
     * @return array
     */
    public function jsonSerialize(): array
    {
        return $this->toArray();
    }
}