import { GraphQLClient } from 'graphql-request';
import { DoubletsGraphQLClient } from '../graphql-client';
import type { Link, LinksBooleanExpression } from '../types';
import { GET_LINKS, GET_LINK_BY_ID } from '../queries';

// Mock graphql-request
jest.mock('graphql-request');

const MockedGraphQLClient = GraphQLClient as jest.MockedClass<typeof GraphQLClient>;

describe('DoubletsGraphQLClient', () => {
  let client: DoubletsGraphQLClient;
  let mockGraphQLClient: jest.Mocked<GraphQLClient>;

  const mockLink: Link = {
    id: 1,
    from_id: 2,
    to_id: 3,
    type_id: 4
  };

  beforeEach(() => {
    jest.clearAllMocks();
    
    mockGraphQLClient = {
      request: jest.fn(),
      setHeaders: jest.fn(),
    } as any;

    MockedGraphQLClient.mockImplementation(() => mockGraphQLClient);
    
    client = new DoubletsGraphQLClient({
      endpoint: 'http://localhost:4000/graphql',
      headers: { 'Authorization': 'Bearer token' }
    });
  });

  describe('constructor', () => {
    it('should create GraphQL client with correct config', () => {
      expect(MockedGraphQLClient).toHaveBeenCalledWith(
        'http://localhost:4000/graphql',
        { headers: { 'Authorization': 'Bearer token' } }
      );
    });

    it('should create GraphQL client with empty headers if not provided', () => {
      new DoubletsGraphQLClient({ endpoint: 'http://localhost:4000/graphql' });
      
      expect(MockedGraphQLClient).toHaveBeenLastCalledWith(
        'http://localhost:4000/graphql',
        { headers: {} }
      );
    });
  });

  describe('queryLinks', () => {
    it('should query links without arguments', async () => {
      const mockResponse = { links: [mockLink] };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.queryLinks();

      expect(mockGraphQLClient.request).toHaveBeenCalledWith(GET_LINKS, undefined);
      expect(result).toEqual([mockLink]);
    });

    it('should query links with arguments', async () => {
      const mockResponse = { links: [mockLink] };
      const args = { limit: 10, offset: 0 };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.queryLinks(args);

      expect(mockGraphQLClient.request).toHaveBeenCalledWith(GET_LINKS, args);
      expect(result).toEqual([mockLink]);
    });
  });

  describe('getLinkById', () => {
    it('should get link by ID', async () => {
      const mockResponse = { links_by_pk: mockLink };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.getLinkById(1);

      expect(mockGraphQLClient.request).toHaveBeenCalledWith(GET_LINK_BY_ID, { id: 1 });
      expect(result).toEqual(mockLink);
    });

    it('should return null when link not found', async () => {
      const mockResponse = { links_by_pk: null };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.getLinkById(999);

      expect(mockGraphQLClient.request).toHaveBeenCalledWith(GET_LINK_BY_ID, { id: 999 });
      expect(result).toBeNull();
    });
  });

  describe('countLinks', () => {
    it('should count links', async () => {
      const mockResponse = {
        links_aggregate: {
          aggregate: {
            count: 5
          }
        }
      };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.countLinks();

      expect(result).toBe(5);
    });

    it('should count links with where condition', async () => {
      const mockResponse = {
        links_aggregate: {
          aggregate: {
            count: 3
          }
        }
      };
      const where: LinksBooleanExpression = { from_id: { _eq: 2 } };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.countLinks(where);

      expect(mockGraphQLClient.request).toHaveBeenCalledTimes(1);
      expect(result).toBe(3);
    });
  });

  describe('insertLinkOne', () => {
    it('should insert a single link', async () => {
      const mockResponse = { insert_links_one: mockLink };
      const object = { from_id: 2, to_id: 3 };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.insertLinkOne(object);

      expect(mockGraphQLClient.request).toHaveBeenCalledTimes(1);
      expect(result).toEqual(mockLink);
    });
  });

  describe('updateLinkById', () => {
    it('should update link by ID', async () => {
      const updatedLink = { ...mockLink, from_id: 5 };
      const mockResponse = { update_links_by_pk: updatedLink };
      const _set = { from_id: 5 };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.updateLinkById(1, _set);

      expect(mockGraphQLClient.request).toHaveBeenCalledTimes(1);
      expect(result).toEqual(updatedLink);
    });
  });

  describe('deleteLinkById', () => {
    it('should delete link by ID', async () => {
      const mockResponse = { delete_links_by_pk: mockLink };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.deleteLinkById(1);

      expect(mockGraphQLClient.request).toHaveBeenCalledTimes(1);
      expect(result).toEqual(mockLink);
    });
  });

  describe('linkExists', () => {
    it('should return true when link exists', async () => {
      const mockResponse = { links_by_pk: mockLink };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.linkExists(1);

      expect(result).toBe(true);
    });

    it('should return false when link does not exist', async () => {
      const mockResponse = { links_by_pk: null };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.linkExists(999);

      expect(result).toBe(false);
    });
  });

  describe('searchLinks', () => {
    it('should search links with partial pattern', async () => {
      const mockResponse = { links: [mockLink] };
      const pattern = { from_id: 2, to_id: 3 };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.searchLinks(pattern);

      expect(mockGraphQLClient.request).toHaveBeenCalledWith(
        GET_LINKS,
        {
          where: {
            from_id: { _eq: 2 },
            to_id: { _eq: 3 }
          }
        }
      );
      expect(result).toEqual([mockLink]);
    });

    it('should handle empty pattern', async () => {
      const mockResponse = { links: [mockLink] };
      mockGraphQLClient.request.mockResolvedValue(mockResponse);

      const result = await client.searchLinks({});

      expect(mockGraphQLClient.request).toHaveBeenCalledWith(
        GET_LINKS,
        { where: {} }
      );
      expect(result).toEqual([mockLink]);
    });
  });

  describe('getOrCreateLink', () => {
    it('should return existing link if found', async () => {
      // Mock searchLinks to return existing link
      const mockSearchResponse = { links: [mockLink] };
      mockGraphQLClient.request.mockResolvedValueOnce(mockSearchResponse);

      const result = await client.getOrCreateLink(2, 3);

      expect(result).toEqual(mockLink);
      // Should not call insert since link was found
      expect(mockGraphQLClient.request).toHaveBeenCalledTimes(1);
    });

    it('should create new link if not found', async () => {
      // Mock searchLinks to return empty array
      const mockSearchResponse = { links: [] };
      // Mock insertLinkOne to return new link
      const mockInsertResponse = { insert_links_one: mockLink };
      
      mockGraphQLClient.request
        .mockResolvedValueOnce(mockSearchResponse)
        .mockResolvedValueOnce(mockInsertResponse);

      const result = await client.getOrCreateLink(2, 3);

      expect(result).toEqual(mockLink);
      expect(mockGraphQLClient.request).toHaveBeenCalledTimes(2);
    });
  });

  describe('updateHeaders', () => {
    it('should update client headers', () => {
      const newHeaders = { 'X-API-Key': 'new-key' };
      
      client.updateHeaders(newHeaders);

      expect(mockGraphQLClient.setHeaders).toHaveBeenCalledWith(newHeaders);
    });
  });

  describe('getClient', () => {
    it('should return underlying GraphQL client', () => {
      const underlyingClient = client.getClient();

      expect(underlyingClient).toBe(mockGraphQLClient);
    });
  });
});