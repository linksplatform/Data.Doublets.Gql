import * as Index from '../index';
import { DoubletsAdapter } from '../doublets-adapter';

describe('Index exports', () => {
  it('should export DoubletsAdapter', () => {
    expect(Index.DoubletsAdapter).toBeDefined();
    expect(typeof Index.DoubletsAdapter).toBe('function');
  });

  it('should export WhereBuilder', () => {
    expect(Index.WhereBuilder).toBeDefined();
    expect(typeof Index.WhereBuilder).toBe('function');
  });

  it('should export DoubletsGraphQLClient', () => {
    expect(Index.DoubletsGraphQLClient).toBeDefined();
    expect(typeof Index.DoubletsGraphQLClient).toBe('function');
  });

  it('should export OrderBy enum', () => {
    expect(Index.OrderBy).toBeDefined();
    expect(Index.OrderBy.ASC).toBe('asc');
    expect(Index.OrderBy.DESC).toBe('desc');
  });

  it('should export GraphQL queries', () => {
    expect(Index.GET_LINKS).toBeDefined();
    expect(Index.GET_LINK_BY_ID).toBeDefined();
    expect(Index.INSERT_LINKS).toBeDefined();
    expect(Index.DELETE_LINKS).toBeDefined();
  });

  it('should export createDoubletsClient factory function', () => {
    expect(Index.createDoubletsClient).toBeDefined();
    expect(typeof Index.createDoubletsClient).toBe('function');
  });

  it('should have DoubletsAdapter as default export', () => {
    expect(Index.default).toBe(DoubletsAdapter);
  });

  describe('createDoubletsClient', () => {
    it('should create a DoubletsAdapter instance', () => {
      const config = { endpoint: 'http://localhost:4000/graphql' };
      const client = Index.createDoubletsClient(config);
      
      expect(client).toBeInstanceOf(DoubletsAdapter);
    });
  });
});