package platform.data.doublets.client;

import java.util.Optional;

/**
 * Represents a query for searching links.
 * Allows filtering by link properties and pagination.
 */
public class LinkQuery {
    private final Optional<Long> id;
    private final Optional<Long> fromId;
    private final Optional<Long> toId;
    private final Optional<Integer> limit;
    private final Optional<Integer> offset;
    private final Optional<SortOrder> sortOrder;
    private final Optional<SortField> sortField;

    private LinkQuery(Builder builder) {
        this.id = Optional.ofNullable(builder.id);
        this.fromId = Optional.ofNullable(builder.fromId);
        this.toId = Optional.ofNullable(builder.toId);
        this.limit = Optional.ofNullable(builder.limit);
        this.offset = Optional.ofNullable(builder.offset);
        this.sortOrder = Optional.ofNullable(builder.sortOrder);
        this.sortField = Optional.ofNullable(builder.sortField);
    }

    public Optional<Long> getId() {
        return id;
    }

    public Optional<Long> getFromId() {
        return fromId;
    }

    public Optional<Long> getToId() {
        return toId;
    }

    public Optional<Integer> getLimit() {
        return limit;
    }

    public Optional<Integer> getOffset() {
        return offset;
    }

    public Optional<SortOrder> getSortOrder() {
        return sortOrder;
    }

    public Optional<SortField> getSortField() {
        return sortField;
    }

    /**
     * Creates a new builder for constructing LinkQuery instances.
     *
     * @return a new builder
     */
    public static Builder builder() {
        return new Builder();
    }

    /**
     * Builder class for creating LinkQuery instances.
     */
    public static class Builder {
        private Long id;
        private Long fromId;
        private Long toId;
        private Integer limit;
        private Integer offset;
        private SortOrder sortOrder;
        private SortField sortField;

        public Builder id(long id) {
            this.id = id;
            return this;
        }

        public Builder fromId(long fromId) {
            this.fromId = fromId;
            return this;
        }

        public Builder toId(long toId) {
            this.toId = toId;
            return this;
        }

        public Builder limit(int limit) {
            this.limit = limit;
            return this;
        }

        public Builder offset(int offset) {
            this.offset = offset;
            return this;
        }

        public Builder sortBy(SortField field, SortOrder order) {
            this.sortField = field;
            this.sortOrder = order;
            return this;
        }

        public LinkQuery build() {
            return new LinkQuery(this);
        }
    }

    /**
     * Enumeration of sort orders.
     */
    public enum SortOrder {
        ASC, DESC
    }

    /**
     * Enumeration of sortable fields.
     */
    public enum SortField {
        ID, FROM_ID, TO_ID
    }
}