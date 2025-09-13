<?php

declare(strict_types=1);

namespace LinksPlatform\Data\Doublets\Gql;

/**
 * Represents a single Link in the Doublets system.
 * 
 * A Link is a triplet consisting of:
 * - id: The unique identifier of the link
 * - fromId: The source node ID
 * - toId: The target node ID
 */
class Link
{
    private int $id;
    private int $fromId;
    private int $toId;

    public function __construct(int $id, int $fromId, int $toId)
    {
        $this->id = $id;
        $this->fromId = $fromId;
        $this->toId = $toId;
    }

    /**
     * Create a Link from GraphQL response data.
     * 
     * @param array $data Array containing 'id', 'from_id', 'to_id' keys
     * @return self
     */
    public static function fromArray(array $data): self
    {
        return new self(
            (int)$data['id'],
            (int)$data['from_id'],
            (int)$data['to_id']
        );
    }

    /**
     * Get the link ID.
     * 
     * @return int
     */
    public function getId(): int
    {
        return $this->id;
    }

    /**
     * Get the source node ID.
     * 
     * @return int
     */
    public function getFromId(): int
    {
        return $this->fromId;
    }

    /**
     * Get the target node ID.
     * 
     * @return int
     */
    public function getToId(): int
    {
        return $this->toId;
    }

    /**
     * Convert the link to an array representation.
     * 
     * @return array
     */
    public function toArray(): array
    {
        return [
            'id' => $this->id,
            'from_id' => $this->fromId,
            'to_id' => $this->toId,
        ];
    }

    /**
     * Convert the link to a JSON string.
     * 
     * @return string
     */
    public function toJson(): string
    {
        return json_encode($this->toArray(), JSON_THROW_ON_ERROR);
    }

    /**
     * String representation of the link.
     * 
     * @return string
     */
    public function __toString(): string
    {
        return sprintf('Link(id: %d, from: %d, to: %d)', $this->id, $this->fromId, $this->toId);
    }
}