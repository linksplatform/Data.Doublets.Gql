/**
 * Core Link/Doublet interface representing the basic structure
 */
export interface Link {
  id: number;
  from_id: number;
  to_id: number;
  type_id?: number;
}

/**
 * Extended Link with relationship data
 */
export interface ExtendedLink extends Link {
  from?: Link;
  to?: Link;
  type?: Link;
  in?: Link[];
  out?: Link[];
}

/**
 * Input type for creating/updating links
 */
export interface LinkInput {
  from_id?: number;
  to_id?: number;
  type_id?: number;
}

/**
 * Boolean expression for filtering links
 */
export interface LinksBooleanExpression {
  _and?: LinksBooleanExpression[];
  _not?: LinksBooleanExpression;
  _or?: LinksBooleanExpression[];
  id?: NumberBooleanExpression;
  from_id?: NumberBooleanExpression;
  to_id?: NumberBooleanExpression;
  type_id?: NumberBooleanExpression;
}

/**
 * Number comparison expressions
 */
export interface NumberBooleanExpression {
  _eq?: number;
  _gt?: number;
  _gte?: number;
  _in?: number[];
  _is_null?: boolean;
  _lt?: number;
  _lte?: number;
  _neq?: number;
  _nin?: number[];
}

/**
 * Order by options
 */
export enum OrderBy {
  ASC = 'asc',
  DESC = 'desc'
}

/**
 * Link ordering input
 */
export interface LinksOrderBy {
  id?: OrderBy;
  from_id?: OrderBy;
  to_id?: OrderBy;
  type_id?: OrderBy;
}

/**
 * Query arguments for link operations
 */
export interface LinksQueryArgs {
  distinct_on?: string[];
  limit?: number;
  offset?: number;
  order_by?: LinksOrderBy[];
  where?: LinksBooleanExpression;
  [key: string]: any; // Allow additional properties for GraphQL compatibility
}

/**
 * Mutation response interface
 */
export interface LinksMutationResponse {
  affected_rows: number;
  returning: Link[];
}

/**
 * Insert input with conflict resolution
 */
export interface LinksInsertInput {
  objects: LinkInput[];
  on_conflict?: LinksOnConflict;
  [key: string]: any; // Allow additional properties for GraphQL compatibility
}

/**
 * Conflict resolution options
 */
export interface LinksOnConflict {
  constraint?: string;
  update_columns?: string[];
  where?: LinksBooleanExpression;
}

/**
 * Update input
 */
export interface LinksUpdateInput {
  _set?: Partial<LinkInput>;
  where: LinksBooleanExpression;
  [key: string]: any; // Allow additional properties for GraphQL compatibility
}

/**
 * GraphQL client configuration
 */
export interface DoubletsClientConfig {
  endpoint: string;
  headers?: Record<string, string>;
}

/**
 * Native API interface for JavaScript-style usage
 */
export interface IDoubletsAdapter {
  // Basic CRUD operations
  create(from_id: number, to_id: number): Promise<Link>;
  getOrCreate(from_id: number, to_id: number): Promise<Link>;
  update(id: number, from_id: number, to_id: number): Promise<Link>;
  delete(id: number): Promise<boolean>;
  
  // Query operations
  get(id: number): Promise<Link | null>;
  exists(id: number): Promise<boolean>;
  getAll(query?: LinksQueryArgs): Promise<Link[]>;
  
  // Search operations
  search(pattern: Partial<Link>): Promise<Link[]>;
  count(where?: LinksBooleanExpression): Promise<number>;
}