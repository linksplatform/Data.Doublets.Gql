import { GraphQLClient } from 'graphql-request';
import type {
  Link,
  LinkInput,
  LinksBooleanExpression,
  LinksQueryArgs,
  LinksMutationResponse,
  LinksInsertInput,
  LinksUpdateInput,
  DoubletsClientConfig
} from './types';
import {
  GET_LINKS,
  GET_LINK_BY_ID,
  GET_LINKS_AGGREGATE,
  INSERT_LINKS,
  INSERT_LINK_ONE,
  UPDATE_LINKS,
  UPDATE_LINK_BY_ID,
  DELETE_LINKS,
  DELETE_LINK_BY_ID
} from './queries';

/**
 * GraphQL client for Doublets operations
 */
export class DoubletsGraphQLClient {
  private client: GraphQLClient;

  constructor(config: DoubletsClientConfig) {
    this.client = new GraphQLClient(config.endpoint, {
      headers: config.headers || {}
    });
  }

  /**
   * Query links with optional filtering, ordering, and pagination
   */
  async queryLinks(args?: LinksQueryArgs): Promise<Link[]> {
    const response = await this.client.request<{ links: Link[] }>(GET_LINKS, args);
    return response.links;
  }

  /**
   * Get a single link by ID
   */
  async getLinkById(id: number): Promise<Link | null> {
    const response = await this.client.request<{ links_by_pk: Link | null }>(GET_LINK_BY_ID, { id });
    return response.links_by_pk;
  }

  /**
   * Count links matching a condition
   */
  async countLinks(where?: LinksBooleanExpression): Promise<number> {
    const response = await this.client.request<{ links_aggregate: { aggregate: { count: number } } }>(GET_LINKS_AGGREGATE, { where });
    return response.links_aggregate.aggregate.count;
  }

  /**
   * Insert multiple links
   */
  async insertLinks(input: LinksInsertInput): Promise<LinksMutationResponse> {
    const response = await this.client.request<{ insert_links: LinksMutationResponse }>(INSERT_LINKS, input);
    return response.insert_links;
  }

  /**
   * Insert a single link
   */
  async insertLinkOne(object: LinkInput, on_conflict?: any): Promise<Link> {
    const response = await this.client.request<{ insert_links_one: Link }>(INSERT_LINK_ONE, { object, on_conflict });
    return response.insert_links_one;
  }

  /**
   * Update links matching a condition
   */
  async updateLinks(input: LinksUpdateInput): Promise<LinksMutationResponse> {
    const response = await this.client.request<{ update_links: LinksMutationResponse }>(UPDATE_LINKS, input);
    return response.update_links;
  }

  /**
   * Update a single link by ID
   */
  async updateLinkById(id: number, _set: Partial<LinkInput>): Promise<Link> {
    const response = await this.client.request<{ update_links_by_pk: Link }>(UPDATE_LINK_BY_ID, { id, _set });
    return response.update_links_by_pk;
  }

  /**
   * Delete links matching a condition
   */
  async deleteLinks(where: LinksBooleanExpression): Promise<LinksMutationResponse> {
    const response = await this.client.request<{ delete_links: LinksMutationResponse }>(DELETE_LINKS, { where });
    return response.delete_links;
  }

  /**
   * Delete a single link by ID
   */
  async deleteLinkById(id: number): Promise<Link | null> {
    const response = await this.client.request<{ delete_links_by_pk: Link | null }>(DELETE_LINK_BY_ID, { id });
    return response.delete_links_by_pk;
  }

  /**
   * Check if a link exists
   */
  async linkExists(id: number): Promise<boolean> {
    const link = await this.getLinkById(id);
    return link !== null;
  }

  /**
   * Search for links with a partial match
   */
  async searchLinks(pattern: Partial<Link>): Promise<Link[]> {
    const where: LinksBooleanExpression = {};
    
    if (pattern.id !== undefined) {
      where.id = { _eq: pattern.id };
    }
    if (pattern.from_id !== undefined) {
      where.from_id = { _eq: pattern.from_id };
    }
    if (pattern.to_id !== undefined) {
      where.to_id = { _eq: pattern.to_id };
    }
    if (pattern.type_id !== undefined) {
      where.type_id = { _eq: pattern.type_id };
    }

    return this.queryLinks({ where });
  }

  /**
   * Get or create a link (find existing or create new)
   */
  async getOrCreateLink(from_id: number, to_id: number): Promise<Link> {
    // First try to find existing link
    const existing = await this.searchLinks({ from_id, to_id });
    if (existing.length > 0) {
      return existing[0];
    }

    // Create new link if not found
    return this.insertLinkOne({ from_id, to_id });
  }

  /**
   * Update the GraphQL client headers (e.g., for authentication)
   */
  updateHeaders(headers: Record<string, string>): void {
    this.client.setHeaders(headers);
  }

  /**
   * Get the underlying GraphQL client for advanced operations
   */
  getClient(): GraphQLClient {
    return this.client;
  }
}