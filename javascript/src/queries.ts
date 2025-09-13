import { gql } from 'graphql-request';

/**
 * GraphQL query to get links with optional filtering, ordering, and pagination
 */
export const GET_LINKS = gql`
  query GetLinks(
    $distinct_on: [links_select_column!]
    $limit: Int
    $offset: Int
    $order_by: [links_order_by!]
    $where: links_bool_exp
  ) {
    links(
      distinct_on: $distinct_on
      limit: $limit
      offset: $offset
      order_by: $order_by
      where: $where
    ) {
      id
      from_id
      to_id
      type_id
      from {
        id
        from_id
        to_id
        type_id
      }
      to {
        id
        from_id
        to_id
        type_id
      }
      type {
        id
        from_id
        to_id
        type_id
      }
    }
  }
`;

/**
 * GraphQL query to get a single link by ID
 */
export const GET_LINK_BY_ID = gql`
  query GetLinkById($id: bigint!) {
    links_by_pk(id: $id) {
      id
      from_id
      to_id
      type_id
      from {
        id
        from_id
        to_id
        type_id
      }
      to {
        id
        from_id
        to_id
        type_id
      }
      type {
        id
        from_id
        to_id
        type_id
      }
    }
  }
`;

/**
 * GraphQL query to get links aggregate (for counting)
 */
export const GET_LINKS_AGGREGATE = gql`
  query GetLinksAggregate($where: links_bool_exp) {
    links_aggregate(where: $where) {
      aggregate {
        count
      }
    }
  }
`;

/**
 * GraphQL mutation to insert links
 */
export const INSERT_LINKS = gql`
  mutation InsertLinks($objects: [links_insert_input!]!, $on_conflict: links_on_conflict) {
    insert_links(objects: $objects, on_conflict: $on_conflict) {
      affected_rows
      returning {
        id
        from_id
        to_id
        type_id
      }
    }
  }
`;

/**
 * GraphQL mutation to insert a single link
 */
export const INSERT_LINK_ONE = gql`
  mutation InsertLinkOne($object: links_insert_input!, $on_conflict: links_on_conflict) {
    insert_links_one(object: $object, on_conflict: $on_conflict) {
      id
      from_id
      to_id
      type_id
    }
  }
`;

/**
 * GraphQL mutation to update links
 */
export const UPDATE_LINKS = gql`
  mutation UpdateLinks($_set: links_set_input, $where: links_bool_exp!) {
    update_links(_set: $_set, where: $where) {
      affected_rows
      returning {
        id
        from_id
        to_id
        type_id
      }
    }
  }
`;

/**
 * GraphQL mutation to update a single link by ID
 */
export const UPDATE_LINK_BY_ID = gql`
  mutation UpdateLinkById($id: bigint!, $_set: links_set_input!) {
    update_links_by_pk(pk_columns: { id: $id }, _set: $_set) {
      id
      from_id
      to_id
      type_id
    }
  }
`;

/**
 * GraphQL mutation to delete links
 */
export const DELETE_LINKS = gql`
  mutation DeleteLinks($where: links_bool_exp!) {
    delete_links(where: $where) {
      affected_rows
      returning {
        id
        from_id
        to_id
        type_id
      }
    }
  }
`;

/**
 * GraphQL mutation to delete a single link by ID
 */
export const DELETE_LINK_BY_ID = gql`
  mutation DeleteLinkById($id: bigint!) {
    delete_links_by_pk(id: $id) {
      id
      from_id
      to_id
      type_id
    }
  }
`;