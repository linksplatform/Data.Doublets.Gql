# -*- coding: utf-8 -*-
"""
Comprehensive tests for the Doublets adapter.

This module tests both the high-level Doublets interface
and the various backend implementations.
"""
import unittest
from unittest.mock import Mock, patch
from typing import Optional

from deepclient import Doublets, Link, MockBackend, GraphQLBackend, DeepClientError


class TestLink(unittest.TestCase):
    """Test the Link data structure."""
    
    def test_link_creation(self):
        """Test creating a Link instance."""
        link = Link(id=1, source=2, target=3)
        self.assertEqual(link.id, 1)
        self.assertEqual(link.source, 2)
        self.assertEqual(link.target, 3)
    
    def test_link_repr(self):
        """Test Link string representation."""
        link = Link(id=1, source=2, target=3)
        self.assertEqual(str(link), "Link(id=1, source=2, target=3)")


class TestMockBackend(unittest.TestCase):
    """Test the MockBackend implementation."""
    
    def setUp(self):
        """Set up test fixtures."""
        self.backend = MockBackend()
    
    def test_create_self_referencing_link(self):
        """Test creating a self-referencing link."""
        link = self.backend.create()
        self.assertEqual(link.id, 1)
        self.assertEqual(link.source, 1)
        self.assertEqual(link.target, 1)
    
    def test_create_link_with_source_target(self):
        """Test creating a link with specified source and target."""
        link = self.backend.create(source=5, target=10)
        self.assertEqual(link.id, 1)
        self.assertEqual(link.source, 5)
        self.assertEqual(link.target, 10)
    
    def test_get_existing_link(self):
        """Test getting an existing link."""
        created = self.backend.create(source=2, target=3)
        retrieved = self.backend.get(created.id)
        
        self.assertIsNotNone(retrieved)
        self.assertEqual(retrieved.id, created.id)
        self.assertEqual(retrieved.source, 2)
        self.assertEqual(retrieved.target, 3)
    
    def test_get_nonexistent_link(self):
        """Test getting a non-existent link."""
        result = self.backend.get(999)
        self.assertIsNone(result)
    
    def test_update_link(self):
        """Test updating an existing link."""
        created = self.backend.create(source=1, target=2)
        updated = self.backend.update(created.id, source=3, target=4)
        
        self.assertEqual(updated.id, created.id)
        self.assertEqual(updated.source, 3)
        self.assertEqual(updated.target, 4)
    
    def test_update_nonexistent_link(self):
        """Test updating a non-existent link."""
        with self.assertRaises(DeepClientError):
            self.backend.update(999, source=1, target=2)
    
    def test_delete_existing_link(self):
        """Test deleting an existing link."""
        created = self.backend.create()
        result = self.backend.delete(created.id)
        
        self.assertTrue(result)
        self.assertIsNone(self.backend.get(created.id))
    
    def test_delete_nonexistent_link(self):
        """Test deleting a non-existent link."""
        result = self.backend.delete(999)
        self.assertFalse(result)
    
    def test_search_all_links(self):
        """Test searching for all links."""
        link1 = self.backend.create(source=1, target=2)
        link2 = self.backend.create(source=2, target=3)
        
        results = self.backend.search()
        self.assertEqual(len(results), 2)
        self.assertIn(link1, results)
        self.assertIn(link2, results)
    
    def test_search_by_source(self):
        """Test searching by source."""
        link1 = self.backend.create(source=1, target=2)
        link2 = self.backend.create(source=1, target=3)
        link3 = self.backend.create(source=2, target=4)
        
        results = self.backend.search(source=1)
        self.assertEqual(len(results), 2)
        self.assertIn(link1, results)
        self.assertIn(link2, results)
        self.assertNotIn(link3, results)
    
    def test_search_by_target(self):
        """Test searching by target."""
        link1 = self.backend.create(source=1, target=2)
        link2 = self.backend.create(source=3, target=2)
        link3 = self.backend.create(source=4, target=5)
        
        results = self.backend.search(target=2)
        self.assertEqual(len(results), 2)
        self.assertIn(link1, results)
        self.assertIn(link2, results)
        self.assertNotIn(link3, results)
    
    def test_search_by_source_and_target(self):
        """Test searching by both source and target."""
        link1 = self.backend.create(source=1, target=2)
        link2 = self.backend.create(source=1, target=3)
        link3 = self.backend.create(source=2, target=2)
        
        results = self.backend.search(source=1, target=2)
        self.assertEqual(len(results), 1)
        self.assertIn(link1, results)
        self.assertNotIn(link2, results)
        self.assertNotIn(link3, results)
    
    def test_search_with_limit(self):
        """Test searching with limit."""
        for i in range(5):
            self.backend.create(source=i, target=i+1)
        
        results = self.backend.search(limit=3)
        self.assertEqual(len(results), 3)
    
    def test_search_with_offset(self):
        """Test searching with offset."""
        links = []
        for i in range(5):
            links.append(self.backend.create(source=i, target=i+1))
        
        results = self.backend.search(offset=2)
        self.assertEqual(len(results), 3)
        # Results should be sorted by ID
        self.assertEqual(results[0].id, links[2].id)
    
    def test_count_all_links(self):
        """Test counting all links."""
        for i in range(3):
            self.backend.create()
        
        count = self.backend.count()
        self.assertEqual(count, 3)
    
    def test_count_by_source(self):
        """Test counting by source."""
        self.backend.create(source=1, target=2)
        self.backend.create(source=1, target=3)
        self.backend.create(source=2, target=4)
        
        count = self.backend.count(source=1)
        self.assertEqual(count, 2)
    
    def test_count_by_target(self):
        """Test counting by target."""
        self.backend.create(source=1, target=2)
        self.backend.create(source=3, target=2)
        self.backend.create(source=4, target=5)
        
        count = self.backend.count(target=2)
        self.assertEqual(count, 2)


class TestDoublets(unittest.TestCase):
    """Test the high-level Doublets interface."""
    
    def setUp(self):
        """Set up test fixtures."""
        self.backend = MockBackend()
        self.doublets = Doublets(self.backend)
    
    def test_create_link(self):
        """Test creating a link through the Doublets interface."""
        link = self.doublets.create(source=1, target=2)
        self.assertEqual(link.source, 1)
        self.assertEqual(link.target, 2)
    
    def test_get_link(self):
        """Test getting a link through the Doublets interface."""
        created = self.doublets.create(source=3, target=4)
        retrieved = self.doublets.get(created.id)
        
        self.assertIsNotNone(retrieved)
        self.assertEqual(retrieved.id, created.id)
    
    def test_update_link(self):
        """Test updating a link through the Doublets interface."""
        created = self.doublets.create(source=1, target=2)
        updated = self.doublets.update(created.id, source=5, target=6)
        
        self.assertEqual(updated.source, 5)
        self.assertEqual(updated.target, 6)
    
    def test_delete_link(self):
        """Test deleting a link through the Doublets interface."""
        created = self.doublets.create()
        result = self.doublets.delete(created.id)
        
        self.assertTrue(result)
        self.assertIsNone(self.doublets.get(created.id))
    
    def test_search_links(self):
        """Test searching for links through the Doublets interface."""
        link1 = self.doublets.create(source=1, target=2)
        link2 = self.doublets.create(source=1, target=3)
        
        results = self.doublets.search(source=1)
        self.assertEqual(len(results), 2)
    
    def test_count_links(self):
        """Test counting links through the Doublets interface."""
        for i in range(3):
            self.doublets.create()
        
        count = self.doublets.count()
        self.assertEqual(count, 3)
    
    def test_each_method(self):
        """Test the each method."""
        for i in range(3):
            self.doublets.create(source=i, target=i+1)
        
        links = []
        self.doublets.each(lambda link: links.append(link))
        
        self.assertEqual(len(links), 3)
    
    def test_iteration(self):
        """Test iteration over all links."""
        created_links = []
        for i in range(3):
            created_links.append(self.doublets.create(source=i, target=i+1))
        
        iterated_links = list(self.doublets)
        self.assertEqual(len(iterated_links), 3)
        
        for link in iterated_links:
            self.assertIn(link, created_links)
    
    def test_len_method(self):
        """Test the len() method."""
        for i in range(5):
            self.doublets.create()
        
        self.assertEqual(len(self.doublets), 5)
    
    def test_contains_method(self):
        """Test the 'in' operator."""
        link = self.doublets.create()
        
        self.assertIn(link.id, self.doublets)
        self.assertNotIn(999, self.doublets)


class TestGraphQLBackend(unittest.TestCase):
    """Test the GraphQLBackend implementation."""
    
    def setUp(self):
        """Set up test fixtures with mocked GraphQL client."""
        with patch('deepclient.backends.DeepClient') as mock_client_class:
            self.mock_client = Mock()
            mock_client_class.return_value = self.mock_client
            self.backend = GraphQLBackend('http://test.com/graphql')
    
    def test_create_with_source_target(self):
        """Test creating a link with source and target."""
        # Mock the GraphQL response
        self.mock_client.query.return_value = {
            'insert_links_one': {
                'id': 1,
                'from_id': 2,
                'to_id': 3
            }
        }
        
        link = self.backend.create(source=2, target=3)
        
        self.assertEqual(link.id, 1)
        self.assertEqual(link.source, 2)
        self.assertEqual(link.target, 3)
    
    def test_get_existing_link(self):
        """Test getting an existing link."""
        # Mock the GraphQL response
        self.mock_client.select.return_value = {
            'links': [{
                'id': 1,
                'from_id': 2,
                'to_id': 3
            }]
        }
        
        link = self.backend.get(1)
        
        self.assertIsNotNone(link)
        self.assertEqual(link.id, 1)
        self.assertEqual(link.source, 2)
        self.assertEqual(link.target, 3)
    
    def test_get_nonexistent_link(self):
        """Test getting a non-existent link."""
        # Mock empty response
        self.mock_client.select.return_value = {'links': []}
        
        link = self.backend.get(999)
        self.assertIsNone(link)
    
    def test_update_link(self):
        """Test updating a link."""
        # Mock the GraphQL response
        self.mock_client.update.return_value = {
            'update_links': {
                'returning': [{
                    'id': 1,
                    'from_id': 5,
                    'to_id': 6
                }]
            }
        }
        
        link = self.backend.update(1, source=5, target=6)
        
        self.assertEqual(link.id, 1)
        self.assertEqual(link.source, 5)
        self.assertEqual(link.target, 6)
    
    def test_delete_link(self):
        """Test deleting a link."""
        # Mock successful deletion
        self.mock_client.delete.return_value = {
            'delete_links': {
                'returning': [{'id': 1}]
            }
        }
        
        result = self.backend.delete(1)
        self.assertTrue(result)
    
    def test_search_all_links(self):
        """Test searching for all links."""
        # Mock the GraphQL response
        self.mock_client.select_with_options.return_value = {
            'links': [
                {'id': 1, 'from_id': 2, 'to_id': 3},
                {'id': 4, 'from_id': 5, 'to_id': 6}
            ]
        }
        
        links = self.backend.search()
        
        self.assertEqual(len(links), 2)
        self.assertEqual(links[0].id, 1)
        self.assertEqual(links[1].id, 4)
    
    def test_count_links(self):
        """Test counting links."""
        # Mock the GraphQL response
        self.mock_client.query.return_value = {
            'links_aggregate': {
                'aggregate': {
                    'count': 42
                }
            }
        }
        
        count = self.backend.count()
        self.assertEqual(count, 42)


if __name__ == '__main__':
    unittest.main()