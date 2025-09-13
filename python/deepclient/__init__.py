# -*- coding: utf-8 -*-
"""
Python Doublets Adapter - Native Python interface for Doublets operations.

This package provides a high-level, Pythonic interface for working with
Doublets while supporting pluggable backends (GraphQL, native C++, etc.).
"""
from .exceptions import GraphQlQueryError, DeepClientError
from .doublets import Doublets, Link, DoubletsBackend

# Try to import GraphQL-related components, but don't fail if dependencies aren't available
try:
    from .client import DeepClient
    from .backends import GraphQLBackend, MockBackend
    _GRAPHQL_AVAILABLE = True
except ImportError:
    # GraphQL dependencies not available
    from .backends import MockBackend
    DeepClient = None
    GraphQLBackend = None
    _GRAPHQL_AVAILABLE = False

# For backward compatibility
__all__ = [
    'GraphQlQueryError', 
    'DeepClientError',
    'Doublets',            # Main high-level interface
    'Link',                # Link data structure
    'DoubletsBackend',     # Backend interface
    'MockBackend',         # Mock backend for testing
]

# Add GraphQL components only if available
if _GRAPHQL_AVAILABLE:
    __all__.extend([
        'DeepClient',          # Original GraphQL client
        'GraphQLBackend',      # GraphQL backend implementation
    ])
