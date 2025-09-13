#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Basic usage examples for the Python Doublets Adapter.

This script demonstrates how to use the high-level Doublets interface
with different backends.
"""
from deepclient import Doublets, MockBackend, GraphQLBackend


def mock_backend_example():
    """Example using the MockBackend for testing and development."""
    print("=== Mock Backend Example ===")
    
    # Create a Doublets instance with mock backend
    doublets = Doublets(MockBackend())
    
    # Create some links
    print("Creating links...")
    link1 = doublets.create()  # Self-referencing link
    link2 = doublets.create(source=1, target=2)
    link3 = doublets.create(source=link1.id, target=link2.id)
    
    print(f"Created link1: {link1}")
    print(f"Created link2: {link2}")
    print(f"Created link3: {link3}")
    
    # Search for links
    print(f"\nTotal links: {len(doublets)}")
    print("All links:")
    for link in doublets:
        print(f"  {link}")
    
    # Search by source
    print(f"\nLinks from {link1.id}:")
    for link in doublets.search(source=link1.id):
        print(f"  {link}")
    
    # Update a link
    print(f"\nUpdating link {link2.id}...")
    updated = doublets.update(link2.id, source=10, target=20)
    print(f"Updated: {updated}")
    
    # Delete a link
    print(f"\nDeleting link {link1.id}...")
    success = doublets.delete(link1.id)
    print(f"Deleted: {success}")
    print(f"Remaining links: {len(doublets)}")


def graphql_backend_example():
    """Example using the GraphQLBackend (requires running server)."""
    print("\n=== GraphQL Backend Example ===")
    
    try:
        # Create a Doublets instance with GraphQL backend
        # Note: This requires a running GraphQL server
        doublets = Doublets(GraphQLBackend(
            'http://localhost:60341/v1/graphql',
            headers={'Authorization': 'Bearer your-token-here'}  # If needed
        ))
        
        print("Connected to GraphQL server")
        
        # Get total link count
        total = doublets.count()
        print(f"Total links in database: {total}")
        
        # Create a new link
        new_link = doublets.create(source=1, target=2)
        print(f"Created new link: {new_link}")
        
        # Search for recent links
        recent_links = doublets.search(limit=5)
        print(f"Recent links:")
        for link in recent_links:
            print(f"  {link}")
        
    except Exception as e:
        print(f"GraphQL backend example failed: {e}")
        print("Make sure the GraphQL server is running at localhost:60341")


def pythonic_patterns_example():
    """Example demonstrating Pythonic usage patterns."""
    print("\n=== Pythonic Patterns Example ===")
    
    doublets = Doublets(MockBackend())
    
    # Create some test data
    for i in range(5):
        doublets.create(source=i, target=i+1)
    
    # Pythonic iteration
    print("Using for loop:")
    for link in doublets:
        print(f"  {link}")
    
    # Using list comprehension
    print("\nLinks with source < 3:")
    small_source_links = [link for link in doublets if link.source < 3]
    for link in small_source_links:
        print(f"  {link}")
    
    # Using the 'in' operator
    print(f"\nChecking if link exists...")
    first_link = next(iter(doublets))
    print(f"Link {first_link.id} exists: {first_link.id in doublets}")
    print(f"Link 999 exists: {999 in doublets}")
    
    # Using len()
    print(f"\nTotal links: {len(doublets)}")
    
    # Using each() method with lambda
    print("\nUsing each() method:")
    doublets.each(lambda link: print(f"  Processing {link}"))
    
    # Filtering and counting
    from_zero_count = doublets.count(source=0)
    print(f"\nLinks from source 0: {from_zero_count}")


if __name__ == '__main__':
    # Run all examples
    mock_backend_example()
    graphql_backend_example()
    pythonic_patterns_example()
    
    print("\n=== Examples completed ===")
    print("See the source code for more usage patterns!")