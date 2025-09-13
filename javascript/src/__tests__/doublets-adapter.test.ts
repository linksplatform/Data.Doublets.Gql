import { DoubletsAdapter, WhereBuilder } from '../doublets-adapter';
import { DoubletsGraphQLClient } from '../graphql-client';
import type { Link, LinksBooleanExpression } from '../types';

// Mock the GraphQL client
jest.mock('../graphql-client');

const MockedDoubletsGraphQLClient = DoubletsGraphQLClient as jest.MockedClass<typeof DoubletsGraphQLClient>;

describe('DoubletsAdapter', () => {
  let adapter: DoubletsAdapter;
  let mockGraphQLClient: jest.Mocked<DoubletsGraphQLClient>;

  const mockLink: Link = {
    id: 1,
    from_id: 2,
    to_id: 3,
    type_id: 4
  };

  beforeEach(() => {
    jest.clearAllMocks();
    
    mockGraphQLClient = {
      insertLinkOne: jest.fn(),
      getOrCreateLink: jest.fn(),
      updateLinkById: jest.fn(),
      deleteLinkById: jest.fn(),
      getLinkById: jest.fn(),
      linkExists: jest.fn(),
      queryLinks: jest.fn(),
      searchLinks: jest.fn(),
      countLinks: jest.fn(),
      insertLinks: jest.fn(),
      deleteLinks: jest.fn(),
      updateLinks: jest.fn(),
      updateHeaders: jest.fn(),
    } as any;

    MockedDoubletsGraphQLClient.mockImplementation(() => mockGraphQLClient);
    
    adapter = new DoubletsAdapter({
      endpoint: 'http://localhost:4000/graphql'
    });
  });

  describe('create', () => {
    it('should create a new link', async () => {
      mockGraphQLClient.insertLinkOne.mockResolvedValue(mockLink);

      const result = await adapter.create(2, 3);

      expect(mockGraphQLClient.insertLinkOne).toHaveBeenCalledWith({ from_id: 2, to_id: 3 });
      expect(result).toEqual(mockLink);
    });
  });

  describe('getOrCreate', () => {
    it('should get or create a link', async () => {
      mockGraphQLClient.getOrCreateLink.mockResolvedValue(mockLink);

      const result = await adapter.getOrCreate(2, 3);

      expect(mockGraphQLClient.getOrCreateLink).toHaveBeenCalledWith(2, 3);
      expect(result).toEqual(mockLink);
    });
  });

  describe('update', () => {
    it('should update an existing link', async () => {
      const updatedLink = { ...mockLink, from_id: 5, to_id: 6 };
      mockGraphQLClient.updateLinkById.mockResolvedValue(updatedLink);

      const result = await adapter.update(1, 5, 6);

      expect(mockGraphQLClient.updateLinkById).toHaveBeenCalledWith(1, { from_id: 5, to_id: 6 });
      expect(result).toEqual(updatedLink);
    });
  });

  describe('delete', () => {
    it('should delete a link and return true on success', async () => {
      mockGraphQLClient.deleteLinkById.mockResolvedValue(mockLink);

      const result = await adapter.delete(1);

      expect(mockGraphQLClient.deleteLinkById).toHaveBeenCalledWith(1);
      expect(result).toBe(true);
    });

    it('should return false when link not found', async () => {
      mockGraphQLClient.deleteLinkById.mockResolvedValue(null);

      const result = await adapter.delete(999);

      expect(mockGraphQLClient.deleteLinkById).toHaveBeenCalledWith(999);
      expect(result).toBe(false);
    });
  });

  describe('get', () => {
    it('should get a link by ID', async () => {
      mockGraphQLClient.getLinkById.mockResolvedValue(mockLink);

      const result = await adapter.get(1);

      expect(mockGraphQLClient.getLinkById).toHaveBeenCalledWith(1);
      expect(result).toEqual(mockLink);
    });

    it('should return null when link not found', async () => {
      mockGraphQLClient.getLinkById.mockResolvedValue(null);

      const result = await adapter.get(999);

      expect(mockGraphQLClient.getLinkById).toHaveBeenCalledWith(999);
      expect(result).toBeNull();
    });
  });

  describe('exists', () => {
    it('should return true when link exists', async () => {
      mockGraphQLClient.linkExists.mockResolvedValue(true);

      const result = await adapter.exists(1);

      expect(mockGraphQLClient.linkExists).toHaveBeenCalledWith(1);
      expect(result).toBe(true);
    });

    it('should return false when link does not exist', async () => {
      mockGraphQLClient.linkExists.mockResolvedValue(false);

      const result = await adapter.exists(999);

      expect(mockGraphQLClient.linkExists).toHaveBeenCalledWith(999);
      expect(result).toBe(false);
    });
  });

  describe('getAll', () => {
    it('should get all links', async () => {
      const links = [mockLink];
      mockGraphQLClient.queryLinks.mockResolvedValue(links);

      const result = await adapter.getAll();

      expect(mockGraphQLClient.queryLinks).toHaveBeenCalledWith(undefined);
      expect(result).toEqual(links);
    });

    it('should get links with query parameters', async () => {
      const links = [mockLink];
      const query = { limit: 10, offset: 0 };
      mockGraphQLClient.queryLinks.mockResolvedValue(links);

      const result = await adapter.getAll(query);

      expect(mockGraphQLClient.queryLinks).toHaveBeenCalledWith(query);
      expect(result).toEqual(links);
    });
  });

  describe('search', () => {
    it('should search for links', async () => {
      const links = [mockLink];
      const pattern = { from_id: 2 };
      mockGraphQLClient.searchLinks.mockResolvedValue(links);

      const result = await adapter.search(pattern);

      expect(mockGraphQLClient.searchLinks).toHaveBeenCalledWith(pattern);
      expect(result).toEqual(links);
    });
  });

  describe('count', () => {
    it('should count links', async () => {
      const count = 5;
      const where: LinksBooleanExpression = { from_id: { _eq: 2 } };
      mockGraphQLClient.countLinks.mockResolvedValue(count);

      const result = await adapter.count(where);

      expect(mockGraphQLClient.countLinks).toHaveBeenCalledWith(where);
      expect(result).toBe(count);
    });
  });

  describe('getOutgoing', () => {
    it('should get outgoing links', async () => {
      const links = [mockLink];
      mockGraphQLClient.searchLinks.mockResolvedValue(links);

      const result = await adapter.getOutgoing(2);

      expect(mockGraphQLClient.searchLinks).toHaveBeenCalledWith({ from_id: 2 });
      expect(result).toEqual(links);
    });
  });

  describe('getIncoming', () => {
    it('should get incoming links', async () => {
      const links = [mockLink];
      mockGraphQLClient.searchLinks.mockResolvedValue(links);

      const result = await adapter.getIncoming(3);

      expect(mockGraphQLClient.searchLinks).toHaveBeenCalledWith({ to_id: 3 });
      expect(result).toEqual(links);
    });
  });

  describe('getConnected', () => {
    it('should get both incoming and outgoing links', async () => {
      const incomingLinks = [{ ...mockLink, id: 1 }];
      const outgoingLinks = [{ ...mockLink, id: 2 }];
      
      mockGraphQLClient.searchLinks
        .mockResolvedValueOnce(incomingLinks)
        .mockResolvedValueOnce(outgoingLinks);

      const result = await adapter.getConnected(2);

      expect(mockGraphQLClient.searchLinks).toHaveBeenCalledWith({ to_id: 2 });
      expect(mockGraphQLClient.searchLinks).toHaveBeenCalledWith({ from_id: 2 });
      expect(result).toEqual([...incomingLinks, ...outgoingLinks]);
    });

    it('should remove duplicates from connected links', async () => {
      const duplicateLink = { ...mockLink, id: 1 };
      const incomingLinks = [duplicateLink];
      const outgoingLinks = [duplicateLink];
      
      mockGraphQLClient.searchLinks
        .mockResolvedValueOnce(incomingLinks)
        .mockResolvedValueOnce(outgoingLinks);

      const result = await adapter.getConnected(2);

      expect(result).toEqual([duplicateLink]);
      expect(result).toHaveLength(1);
    });
  });

  describe('createTyped', () => {
    it('should create a typed link', async () => {
      const typedLink = { ...mockLink, type_id: 10 };
      mockGraphQLClient.insertLinkOne.mockResolvedValue(typedLink);

      const result = await adapter.createTyped(2, 3, 10);

      expect(mockGraphQLClient.insertLinkOne).toHaveBeenCalledWith({
        from_id: 2,
        to_id: 3,
        type_id: 10
      });
      expect(result).toEqual(typedLink);
    });
  });

  describe('createBatch', () => {
    it('should create multiple links in batch', async () => {
      const links = [mockLink];
      const objects = [{ from_id: 2, to_id: 3 }];
      mockGraphQLClient.insertLinks.mockResolvedValue({
        affected_rows: 1,
        returning: links
      });

      const result = await adapter.createBatch(objects);

      expect(mockGraphQLClient.insertLinks).toHaveBeenCalledWith({ objects });
      expect(result).toEqual(links);
    });
  });
});

describe('WhereBuilder', () => {
  it('should build simple conditions', () => {
    const condition = DoubletsAdapter.where()
      .id(1)
      .fromId(2)
      .toId(3)
      .build();

    expect(condition).toEqual({
      id: { _eq: 1 },
      from_id: { _eq: 2 },
      to_id: { _eq: 3 }
    });
  });

  it('should build complex conditions with AND', () => {
    const condition = DoubletsAdapter.where()
      .id(1)
      .and(DoubletsAdapter.where().fromId(2))
      .build();

    expect(condition).toEqual({
      id: { _eq: 1 },
      _and: [{ from_id: { _eq: 2 } }]
    });
  });

  it('should build complex conditions with OR', () => {
    const condition = DoubletsAdapter.where()
      .id(1)
      .or(DoubletsAdapter.where().fromId(2))
      .build();

    expect(condition).toEqual({
      id: { _eq: 1 },
      _or: [{ from_id: { _eq: 2 } }]
    });
  });

  it('should build conditions with NOT', () => {
    const condition = DoubletsAdapter.where()
      .not(DoubletsAdapter.where().id(1))
      .build();

    expect(condition).toEqual({
      _not: { id: { _eq: 1 } }
    });
  });

  it('should build conditions with range operators', () => {
    const condition = DoubletsAdapter.where()
      .idGreaterThan(10)
      .build();

    expect(condition).toEqual({
      id: { _gt: 10 }
    });
  });

  it('should build conditions with IN operator', () => {
    const condition = DoubletsAdapter.where()
      .idIn([1, 2, 3])
      .build();

    expect(condition).toEqual({
      id: { _in: [1, 2, 3] }
    });
  });
});