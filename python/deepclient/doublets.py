# -*- coding: utf-8 -*-
"""
Python Doublets Adapter - Native Python interface for Doublets operations.

This module provides a Pythonic interface for Doublets CRUD operations
while supporting pluggable backends (GraphQL, native C++ library, etc.).
"""
from abc import ABC, abstractmethod
from typing import Optional, List, Dict, Any, Union, Iterator
from dataclasses import dataclass


@dataclass
class Link:
    """Represents a Doublets link with id, source (from_id), and target (to_id)."""
    id: int
    source: int
    target: int
    
    def __repr__(self) -> str:
        return f"Link(id={self.id}, source={self.source}, target={self.target})"


class DoubletsBackend(ABC):
    """Abstract base class for Doublets storage backends."""
    
    @abstractmethod
    def create(self, source: Optional[int] = None, target: Optional[int] = None) -> Link:
        """Create a new link."""
        pass
    
    @abstractmethod
    def get(self, link_id: int) -> Optional[Link]:
        """Get a link by its ID."""
        pass
    
    @abstractmethod
    def update(self, link_id: int, source: int, target: int) -> Link:
        """Update an existing link."""
        pass
    
    @abstractmethod
    def delete(self, link_id: int) -> bool:
        """Delete a link by its ID."""
        pass
    
    @abstractmethod
    def search(self, source: Optional[int] = None, target: Optional[int] = None, 
               limit: Optional[int] = None, offset: Optional[int] = None) -> List[Link]:
        """Search for links by source and/or target."""
        pass
    
    @abstractmethod
    def count(self, source: Optional[int] = None, target: Optional[int] = None) -> int:
        """Count links matching the criteria."""
        pass


class Doublets:
    """
    Main Doublets interface providing native Python-style CRUD operations.
    
    This class provides a high-level, Pythonic interface for working with
    Doublets while abstracting away the underlying storage backend.
    """
    
    def __init__(self, backend: DoubletsBackend):
        """
        Initialize Doublets with a specific backend.
        
        Args:
            backend: Storage backend implementation (GraphQL, native, etc.)
        """
        self._backend = backend
    
    def create(self, source: Optional[int] = None, target: Optional[int] = None) -> Link:
        """
        Create a new link.
        
        Args:
            source: Source link ID (optional, defaults to self-reference)
            target: Target link ID (optional, defaults to self-reference)
            
        Returns:
            The created link
            
        Examples:
            >>> doublets = Doublets(backend)
            >>> link = doublets.create()  # Self-referencing link
            >>> link = doublets.create(source=1, target=2)  # Link from 1 to 2
        """
        return self._backend.create(source, target)
    
    def get(self, link_id: int) -> Optional[Link]:
        """
        Get a link by its ID.
        
        Args:
            link_id: The ID of the link to retrieve
            
        Returns:
            The link if found, None otherwise
            
        Examples:
            >>> link = doublets.get(42)
            >>> if link:
            ...     print(f"Link: {link.source} -> {link.target}")
        """
        return self._backend.get(link_id)
    
    def update(self, link_id: int, source: int, target: int) -> Link:
        """
        Update an existing link.
        
        Args:
            link_id: ID of the link to update
            source: New source link ID
            target: New target link ID
            
        Returns:
            The updated link
            
        Examples:
            >>> updated = doublets.update(42, source=1, target=3)
        """
        return self._backend.update(link_id, source, target)
    
    def delete(self, link_id: int) -> bool:
        """
        Delete a link by its ID.
        
        Args:
            link_id: ID of the link to delete
            
        Returns:
            True if the link was deleted, False otherwise
            
        Examples:
            >>> success = doublets.delete(42)
        """
        return self._backend.delete(link_id)
    
    def search(self, source: Optional[int] = None, target: Optional[int] = None,
               limit: Optional[int] = None, offset: Optional[int] = None) -> List[Link]:
        """
        Search for links by source and/or target.
        
        Args:
            source: Filter by source link ID (optional)
            target: Filter by target link ID (optional)
            limit: Maximum number of results (optional)
            offset: Number of results to skip (optional)
            
        Returns:
            List of matching links
            
        Examples:
            >>> links = doublets.search(source=1)  # All links from 1
            >>> links = doublets.search(target=2)  # All links to 2
            >>> links = doublets.search(source=1, target=2)  # Links from 1 to 2
            >>> links = doublets.search(limit=10)  # First 10 links
        """
        return self._backend.search(source, target, limit, offset)
    
    def count(self, source: Optional[int] = None, target: Optional[int] = None) -> int:
        """
        Count links matching the criteria.
        
        Args:
            source: Filter by source link ID (optional)
            target: Filter by target link ID (optional)
            
        Returns:
            Number of matching links
            
        Examples:
            >>> total = doublets.count()  # Total number of links
            >>> from_one = doublets.count(source=1)  # Links from 1
        """
        return self._backend.count(source, target)
    
    def each(self, callback: callable, source: Optional[int] = None, 
             target: Optional[int] = None) -> None:
        """
        Iterate over links and call a function for each one.
        
        Args:
            callback: Function to call for each link
            source: Filter by source link ID (optional)
            target: Filter by target link ID (optional)
            
        Examples:
            >>> doublets.each(lambda link: print(f"Link: {link}"))
            >>> doublets.each(lambda link: print(link.id), source=1)
        """
        for link in self.search(source=source, target=target):
            callback(link)
    
    def __iter__(self) -> Iterator[Link]:
        """
        Allow iteration over all links.
        
        Examples:
            >>> for link in doublets:
            ...     print(link)
        """
        return iter(self.search())
    
    def __len__(self) -> int:
        """
        Get the total number of links.
        
        Examples:
            >>> total_links = len(doublets)
        """
        return self.count()
    
    def __contains__(self, link_id: int) -> bool:
        """
        Check if a link exists.
        
        Examples:
            >>> if 42 in doublets:
            ...     print("Link 42 exists")
        """
        return self.get(link_id) is not None