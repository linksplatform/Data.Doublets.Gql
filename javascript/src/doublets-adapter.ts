import { DoubletsGraphQLClient } from './graphql-client';
import type {
  Link,
  LinksBooleanExpression,
  LinksQueryArgs,
  DoubletsClientConfig,
  IDoubletsAdapter
} from './types';

/**
 * Native JavaScript API adapter for Doublets operations
 * Provides a more JavaScript-friendly interface over the GraphQL client
 */
export class DoubletsAdapter implements IDoubletsAdapter {
  private graphqlClient: DoubletsGraphQLClient;

  constructor(config: DoubletsClientConfig) {
    this.graphqlClient = new DoubletsGraphQLClient(config);
  }

  /**
   * Create a new link between two nodes
   */
  async create(from_id: number, to_id: number): Promise<Link> {
    return await this.graphqlClient.insertLinkOne({ from_id, to_id });
  }

  /**
   * Get existing link or create new one if it doesn't exist
   */
  async getOrCreate(from_id: number, to_id: number): Promise<Link> {
    return await this.graphqlClient.getOrCreateLink(from_id, to_id);
  }

  /**
   * Update an existing link
   */
  async update(id: number, from_id: number, to_id: number): Promise<Link> {
    return await this.graphqlClient.updateLinkById(id, { from_id, to_id });
  }

  /**
   * Delete a link by ID
   */
  async delete(id: number): Promise<boolean> {
    const result = await this.graphqlClient.deleteLinkById(id);
    return result !== null;
  }

  /**
   * Get a link by ID
   */
  async get(id: number): Promise<Link | null> {
    return await this.graphqlClient.getLinkById(id);
  }

  /**
   * Check if a link exists
   */
  async exists(id: number): Promise<boolean> {
    return await this.graphqlClient.linkExists(id);
  }

  /**
   * Get all links with optional query parameters
   */
  async getAll(query?: LinksQueryArgs): Promise<Link[]> {
    return await this.graphqlClient.queryLinks(query);
  }

  /**
   * Search for links matching a pattern
   */
  async search(pattern: Partial<Link>): Promise<Link[]> {
    return await this.graphqlClient.searchLinks(pattern);
  }

  /**
   * Count links matching a condition
   */
  async count(where?: LinksBooleanExpression): Promise<number> {
    return await this.graphqlClient.countLinks(where);
  }

  /**
   * Get links where the specified node is the source (from_id)
   */
  async getOutgoing(from_id: number): Promise<Link[]> {
    return await this.search({ from_id });
  }

  /**
   * Get links where the specified node is the target (to_id)
   */
  async getIncoming(to_id: number): Promise<Link[]> {
    return await this.search({ to_id });
  }

  /**
   * Get all links connected to a node (both incoming and outgoing)
   */
  async getConnected(id: number): Promise<Link[]> {
    const [incoming, outgoing] = await Promise.all([
      this.getIncoming(id),
      this.getOutgoing(id)
    ]);
    
    // Remove duplicates if any (though there shouldn't be for incoming/outgoing)
    const seen = new Set<number>();
    const result: Link[] = [];
    
    for (const link of [...incoming, ...outgoing]) {
      if (!seen.has(link.id)) {
        seen.add(link.id);
        result.push(link);
      }
    }
    
    return result;
  }

  /**
   * Find links by type
   */
  async findByType(type_id: number): Promise<Link[]> {
    return await this.search({ type_id });
  }

  /**
   * Create a typed link (with explicit type_id)
   */
  async createTyped(from_id: number, to_id: number, type_id: number): Promise<Link> {
    return await this.graphqlClient.insertLinkOne({ from_id, to_id, type_id });
  }

  /**
   * Get or create a typed link
   */
  async getOrCreateTyped(from_id: number, to_id: number, type_id: number): Promise<Link> {
    const existing = await this.search({ from_id, to_id, type_id });
    if (existing.length > 0) {
      return existing[0];
    }
    return await this.createTyped(from_id, to_id, type_id);
  }

  /**
   * Batch create multiple links
   */
  async createBatch(links: Array<{ from_id: number; to_id: number; type_id?: number }>): Promise<Link[]> {
    const result = await this.graphqlClient.insertLinks({ objects: links });
    return result.returning;
  }

  /**
   * Delete multiple links by condition
   */
  async deleteWhere(where: LinksBooleanExpression): Promise<number> {
    const result = await this.graphqlClient.deleteLinks(where);
    return result.affected_rows;
  }

  /**
   * Update multiple links by condition
   */
  async updateWhere(where: LinksBooleanExpression, set: Partial<Link>): Promise<number> {
    const result = await this.graphqlClient.updateLinks({ where, _set: set });
    return result.affected_rows;
  }

  /**
   * Get the underlying GraphQL client for advanced operations
   */
  getGraphQLClient(): DoubletsGraphQLClient {
    return this.graphqlClient;
  }

  /**
   * Update authentication headers
   */
  setAuth(headers: Record<string, string>): void {
    this.graphqlClient.updateHeaders(headers);
  }

  /**
   * Simple query builder helper for complex conditions
   */
  static where(): WhereBuilder {
    return new WhereBuilder();
  }
}

/**
 * Helper class for building complex where conditions
 */
export class WhereBuilder {
  private condition: LinksBooleanExpression = {};

  id(value: number): this {
    this.condition.id = { _eq: value };
    return this;
  }

  fromId(value: number): this {
    this.condition.from_id = { _eq: value };
    return this;
  }

  toId(value: number): this {
    this.condition.to_id = { _eq: value };
    return this;
  }

  typeId(value: number): this {
    this.condition.type_id = { _eq: value };
    return this;
  }

  idIn(values: number[]): this {
    this.condition.id = { _in: values };
    return this;
  }

  idGreaterThan(value: number): this {
    this.condition.id = { _gt: value };
    return this;
  }

  idLessThan(value: number): this {
    this.condition.id = { _lt: value };
    return this;
  }

  and(builder: WhereBuilder): this {
    this.condition._and = this.condition._and || [];
    this.condition._and.push(builder.build());
    return this;
  }

  or(builder: WhereBuilder): this {
    this.condition._or = this.condition._or || [];
    this.condition._or.push(builder.build());
    return this;
  }

  not(builder: WhereBuilder): this {
    this.condition._not = builder.build();
    return this;
  }

  build(): LinksBooleanExpression {
    return this.condition;
  }
}