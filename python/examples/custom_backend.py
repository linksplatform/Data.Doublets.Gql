#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Custom backend example for the Python Doublets Adapter.

This script demonstrates how to create a custom backend implementation
that could be used for native C++ integration or other storage systems.
"""
from typing import Optional, List, Dict, Any
from deepclient import Doublets, DoubletsBackend, Link, DeepClientError


class FileBackend(DoubletsBackend):
    """
    Example custom backend that stores links in a simple file format.
    
    This demonstrates how to implement a custom backend that could
    later be replaced with a native C++ library implementation.
    """
    
    def __init__(self, filename: str):
        """Initialize file backend with a storage file."""
        self.filename = filename
        self._next_id = 1
        self._load_from_file()
    
    def _load_from_file(self):
        """Load links from file."""
        self._links: Dict[int, Link] = {}
        try:
            with open(self.filename, 'r') as f:
                for line in f:
                    line = line.strip()
                    if line:
                        parts = line.split(',')
                        if len(parts) == 3:
                            link_id, source, target = map(int, parts)
                            self._links[link_id] = Link(link_id, source, target)
                            self._next_id = max(self._next_id, link_id + 1)
        except FileNotFoundError:
            # File doesn't exist yet, start fresh
            pass
    
    def _save_to_file(self):
        """Save links to file."""
        with open(self.filename, 'w') as f:
            for link in self._links.values():
                f.write(f"{link.id},{link.source},{link.target}\n")
    
    def create(self, source: Optional[int] = None, target: Optional[int] = None) -> Link:
        """Create a new link in file."""
        link_id = self._next_id
        self._next_id += 1
        
        if source is None:
            source = link_id
        if target is None:
            target = link_id
        
        link = Link(id=link_id, source=source, target=target)
        self._links[link_id] = link
        self._save_to_file()
        return link
    
    def get(self, link_id: int) -> Optional[Link]:
        """Get a link by its ID from file."""
        return self._links.get(link_id)
    
    def update(self, link_id: int, source: int, target: int) -> Link:
        """Update an existing link in file."""
        if link_id not in self._links:
            raise DeepClientError(f'Link {link_id} does not exist')
        
        link = Link(id=link_id, source=source, target=target)
        self._links[link_id] = link
        self._save_to_file()
        return link
    
    def delete(self, link_id: int) -> bool:
        """Delete a link by its ID from file."""
        if link_id in self._links:
            del self._links[link_id]
            self._save_to_file()
            return True
        return False
    
    def search(self, source: Optional[int] = None, target: Optional[int] = None,
               limit: Optional[int] = None, offset: Optional[int] = None) -> List[Link]:
        """Search for links by source and/or target in file."""
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
        """Count links matching the criteria in file."""
        count = 0
        
        for link in self._links.values():
            if source is not None and link.source != source:
                continue
            if target is not None and link.target != target:
                continue
            count += 1
        
        return count


class NativeBackendPlaceholder(DoubletsBackend):
    """
    Placeholder for future native C++ backend.
    
    This demonstrates the interface that would be used when
    the native C++ library is ready.
    """
    
    def __init__(self, library_path: str):
        """Initialize with path to native library."""
        self.library_path = library_path
        # In the future, this would load the C++ library using ctypes or pyo3
        raise NotImplementedError(
            "Native C++ backend not yet implemented. "
            "This is a placeholder for future development."
        )
    
    def create(self, source: Optional[int] = None, target: Optional[int] = None) -> Link:
        """Would call native C++ create function."""
        pass
    
    def get(self, link_id: int) -> Optional[Link]:
        """Would call native C++ get function."""
        pass
    
    def update(self, link_id: int, source: int, target: int) -> Link:
        """Would call native C++ update function."""
        pass
    
    def delete(self, link_id: int) -> bool:
        """Would call native C++ delete function."""
        pass
    
    def search(self, source: Optional[int] = None, target: Optional[int] = None,
               limit: Optional[int] = None, offset: Optional[int] = None) -> List[Link]:
        """Would call native C++ search function."""
        pass
    
    def count(self, source: Optional[int] = None, target: Optional[int] = None) -> int:
        """Would call native C++ count function."""
        pass


def file_backend_example():
    """Example using the custom FileBackend."""
    print("=== File Backend Example ===")
    
    # Create a Doublets instance with file backend
    doublets = Doublets(FileBackend('/tmp/doublets_test.db'))
    
    print("Creating links...")
    link1 = doublets.create(source=1, target=2)
    link2 = doublets.create(source=2, target=3)
    link3 = doublets.create(source=link1.id, target=link2.id)
    
    print(f"Created: {link1}")
    print(f"Created: {link2}")
    print(f"Created: {link3}")
    
    print(f"\nTotal links: {len(doublets)}")
    
    # Create another instance to test persistence
    print("\nCreating new instance to test persistence...")
    doublets2 = Doublets(FileBackend('/tmp/doublets_test.db'))
    print(f"Loaded {len(doublets2)} links from file")
    
    for link in doublets2:
        print(f"  {link}")
    
    # Clean up
    import os
    try:
        os.remove('/tmp/doublets_test.db')
        print("\nCleaned up test file")
    except FileNotFoundError:
        pass


def backend_switching_example():
    """Example demonstrating how to switch between backends."""
    print("\n=== Backend Switching Example ===")
    
    # Start with mock backend
    from deepclient import MockBackend
    mock_backend = MockBackend()
    doublets = Doublets(mock_backend)
    
    # Create some data
    for i in range(3):
        doublets.create(source=i, target=i+1)
    
    print(f"Mock backend has {len(doublets)} links")
    
    # Switch to file backend
    file_backend = FileBackend('/tmp/doublets_switch_test.db')
    doublets._backend = file_backend
    
    # Migrate data (simple example)
    print("Migrating data to file backend...")
    for link in mock_backend.search():
        file_backend.create(source=link.source, target=link.target)
    
    print(f"File backend now has {len(doublets)} links")
    
    # This demonstrates how the same Doublets interface can work
    # with different storage backends
    for link in doublets:
        print(f"  {link}")
    
    # Clean up
    import os
    try:
        os.remove('/tmp/doublets_switch_test.db')
        print("\nCleaned up test file")
    except FileNotFoundError:
        pass


if __name__ == '__main__':
    file_backend_example()
    backend_switching_example()
    
    print("\n=== Custom Backend Examples Completed ===")
    print("This demonstrates the pluggable architecture that allows")
    print("swapping GraphQL with native C++ library in the future.")