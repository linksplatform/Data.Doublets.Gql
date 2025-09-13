// Core exports
export { DoubletsAdapter, WhereBuilder } from './doublets-adapter';
export { DoubletsGraphQLClient } from './graphql-client';

// Import for factory function
import { DoubletsAdapter } from './doublets-adapter';
import type { DoubletsClientConfig } from './types';

// Type exports
export type {
  Link,
  ExtendedLink,
  LinkInput,
  LinksBooleanExpression,
  NumberBooleanExpression,
  LinksOrderBy,
  LinksQueryArgs,
  LinksMutationResponse,
  LinksInsertInput,
  LinksOnConflict,
  LinksUpdateInput,
  DoubletsClientConfig,
  IDoubletsAdapter
} from './types';

// Enum exports
export { OrderBy } from './types';

// Query exports (for advanced users who want to use custom queries)
export {
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

// Convenience factory function
export function createDoubletsClient(config: DoubletsClientConfig): DoubletsAdapter {
  return new DoubletsAdapter(config);
}

// Default export for easier importing
export default DoubletsAdapter;