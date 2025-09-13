# -*- coding: utf-8 -*-
"""
Backend implementations for the Doublets adapter.

This module provides concrete implementations of DoubletsBackend
for different storage systems (GraphQL, native library, etc.).
"""
from typing import Optional, List, Dict, Any
from .doublets import DoubletsBackend, Link
from .exceptions import DeepClientError

# Try to import GraphQL client, but don't fail if not available
try:
    from .client import DeepClient
    _GRAPHQL_AVAILABLE = True
except ImportError:
    DeepClient = None
    _GRAPHQL_AVAILABLE = False


class GraphQLBackend(DoubletsBackend):
    """
    GraphQL backend implementation for Doublets operations.
    
    This backend uses the existing DeepClient to communicate with
    a GraphQL server that provides Doublets operations.
    """
    
    def __init__(self, graphql_url: str, headers: Optional[Dict[str, Any]] = None):
        """
        Initialize GraphQL backend.
        
        Args:
            graphql_url: URL of the GraphQL server
            headers: Optional HTTP headers (e.g., authorization)
        """
        if not _GRAPHQL_AVAILABLE:
            raise ImportError(
                "GraphQL backend requires 'gql' and 'aiohttp' packages. "
                "Install with: pip install gql aiohttp"
            )
        self._client = DeepClient(graphql_url, headers)
    
    def create(self, source: Optional[int] = None, target: Optional[int] = None) -> Link:
        """Create a new link via GraphQL."""
        # If no source/target specified, create self-referencing link
        if source is None and target is None:
            # Create a new link first, then update it to self-reference
            response = self._client.query('''
                mutation {
                    insert_links_one(object: {}) {
                        id
                        from_id
                        to_id
                    }
                }
            ''')
            link_data = response['insert_links_one']
            link_id = link_data['id']
            
            # Update to self-reference
            return self.update(link_id, link_id, link_id)
        
        else:
            # Create link with specified source/target
            from_id = source if source is not None else 0
            to_id = target if target is not None else 0
            
            response = self._client.query(f'''
                mutation {{
                    insert_links_one(object: {{from_id: {from_id}, to_id: {to_id}}}) {{
                        id
                        from_id
                        to_id
                    }}
                }}
            ''')
            link_data = response['insert_links_one']
            return Link(
                id=link_data['id'],
                source=link_data['from_id'],
                target=link_data['to_id']
            )
    
    def get(self, link_id: int) -> Optional[Link]:
        """Get a link by its ID via GraphQL."""
        try:
            response = self._client.select(link_id, 'from_id', 'to_id')
            links = response.get('links', [])
            
            if not links:
                return None
            
            link_data = links[0]
            return Link(
                id=link_data['id'],
                source=link_data['from_id'],
                target=link_data['to_id']
            )
        except Exception:
            return None
    
    def update(self, link_id: int, source: int, target: int) -> Link:
        """Update an existing link via GraphQL."""
        response = self._client.update(
            {'id': {'_eq': link_id}},
            {'from_id': source, 'to_id': target},
            'id', 'from_id', 'to_id'
        )
        
        if not response.get('update_links', {}).get('returning'):
            raise DeepClientError(f'Failed to update link {link_id}')
        
        link_data = response['update_links']['returning'][0]
        return Link(
            id=link_data['id'],
            source=link_data['from_id'],
            target=link_data['to_id']
        )
    
    def delete(self, link_id: int) -> bool:
        """Delete a link by its ID via GraphQL."""
        try:
            response = self._client.delete(
                {'id': {'_eq': link_id}},
                'id'
            )
            return bool(response.get('delete_links', {}).get('returning'))
        except Exception:
            return False
    
    def search(self, source: Optional[int] = None, target: Optional[int] = None,
               limit: Optional[int] = None, offset: Optional[int] = None) -> List[Link]:
        """Search for links by source and/or target via GraphQL."""
        where_conditions = []
        
        if source is not None:
            where_conditions.append(f'from_id: {{_eq: {source}}}')
        if target is not None:
            where_conditions.append(f'to_id: {{_eq: {target}}}')
        
        where_clause = ''
        if where_conditions:
            where_clause = f'where: {{{", ".join(where_conditions)}}}'
        
        options = []
        if where_clause:
            options.append(where_clause)
        if limit is not None:
            options.append(f'limit: {limit}')
        if offset is not None:
            options.append(f'offset: {offset}')
        
        options_str = ', '.join(options)
        
        response = self._client.select_with_options(
            options_str,
            'id', 'from_id', 'to_id'
        )
        
        links = []
        for link_data in response.get('links', []):
            links.append(Link(
                id=link_data['id'],
                source=link_data['from_id'],
                target=link_data['to_id']
            ))
        
        return links
    
    def count(self, source: Optional[int] = None, target: Optional[int] = None) -> int:
        """Count links matching the criteria via GraphQL."""
        where_conditions = []
        
        if source is not None:
            where_conditions.append(f'from_id: {{_eq: {source}}}')
        if target is not None:
            where_conditions.append(f'to_id: {{_eq: {target}}}')
        
        where_clause = ''
        if where_conditions:
            where_clause = f'where: {{{", ".join(where_conditions)}}}'
        
        # Use aggregate count query
        query = f'''
            query {{
                links_aggregate({where_clause}) {{
                    aggregate {{
                        count
                    }}
                }}
            }}
        '''
        
        response = self._client.query(query)
        return response['links_aggregate']['aggregate']['count']


class MockBackend(DoubletsBackend):
    """
    Mock backend implementation for testing and development.
    
    This backend stores links in memory and is useful for testing
    without requiring a GraphQL server.
    """
    
    def __init__(self):
        """Initialize mock backend with empty storage."""
        self._links: Dict[int, Link] = {}
        self._next_id = 1
    
    def create(self, source: Optional[int] = None, target: Optional[int] = None) -> Link:
        """Create a new link in memory."""
        link_id = self._next_id
        self._next_id += 1
        
        # Default to self-reference if not specified
        if source is None:
            source = link_id
        if target is None:
            target = link_id
        
        link = Link(id=link_id, source=source, target=target)
        self._links[link_id] = link
        return link
    
    def get(self, link_id: int) -> Optional[Link]:
        """Get a link by its ID from memory."""
        return self._links.get(link_id)
    
    def update(self, link_id: int, source: int, target: int) -> Link:
        """Update an existing link in memory."""
        if link_id not in self._links:
            raise DeepClientError(f'Link {link_id} does not exist')
        
        link = Link(id=link_id, source=source, target=target)
        self._links[link_id] = link
        return link
    
    def delete(self, link_id: int) -> bool:
        """Delete a link by its ID from memory."""
        if link_id in self._links:
            del self._links[link_id]
            return True
        return False
    
    def search(self, source: Optional[int] = None, target: Optional[int] = None,
               limit: Optional[int] = None, offset: Optional[int] = None) -> List[Link]:
        """Search for links by source and/or target in memory."""
        results = []
        
        for link in self._links.values():
            if source is not None and link.source != source:
                continue
            if target is not None and link.target != target:
                continue
            results.append(link)
        
        # Sort by ID for consistent results
        results.sort(key=lambda x: x.id)
        
        # Apply offset and limit
        if offset is not None:
            results = results[offset:]
        if limit is not None:
            results = results[:limit]
        
        return results
    
    def count(self, source: Optional[int] = None, target: Optional[int] = None) -> int:
        """Count links matching the criteria in memory."""
        count = 0
        
        for link in self._links.values():
            if source is not None and link.source != source:
                continue
            if target is not None and link.target != target:
                continue
            count += 1
        
        return count