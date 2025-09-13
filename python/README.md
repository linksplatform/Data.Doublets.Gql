# Python Doublets Adapter

A native Python interface for Doublets operations via GraphQL client, designed for the LinksPlatform ecosystem.

## Overview

This package provides a high-level, Pythonic interface for working with Doublets while supporting pluggable backends. It includes:

- **Native Python API**: Intuitive CRUD operations following Python conventions
- **Pluggable Architecture**: Support for GraphQL, native C++ library, and custom backends
- **GraphQL Client**: Separate low-level GraphQL client for direct usage
- **Type Safety**: Full type hints for better development experience
- **Comprehensive Testing**: Unit tests for all components

## Installation

### From PyPI (when published)

```bash
pip install doublets-gql
```

### From Source

```bash
git clone https://github.com/linksplatform/Data.Doublets.Gql.git
cd Data.Doublets.Gql/python
pip install -e .
```

### Development Installation

```bash
pip install -e .[dev]
```

## Quick Start

### Basic Usage with Mock Backend

```python
from deepclient import Doublets, MockBackend

# Create a Doublets instance with mock backend (for testing)
doublets = Doublets(MockBackend())

# Create links
link1 = doublets.create()  # Self-referencing link
link2 = doublets.create(source=1, target=2)  # Link from 1 to 2
link3 = doublets.create(source=link1.id, target=link2.id)

print(f"Created: {link1}")  # Link(id=1, source=1, target=1)
print(f"Created: {link2}")  # Link(id=2, source=1, target=2)
print(f"Created: {link3}")  # Link(id=3, source=1, target=2)

# Search and iterate
print(f"Total links: {len(doublets)}")

for link in doublets:
    print(f"  {link}")

# Search by criteria
links_from_1 = doublets.search(source=1)
links_to_2 = doublets.search(target=2)

# Update and delete
updated = doublets.update(link2.id, source=10, target=20)
success = doublets.delete(link1.id)
```

### GraphQL Backend Usage

```python
from deepclient import Doublets, GraphQLBackend

# Connect to GraphQL server
doublets = Doublets(GraphQLBackend(
    'http://localhost:60341/v1/graphql',
    headers={'Authorization': 'Bearer your-token-here'}  # Optional
))

# Same API as mock backend
total = doublets.count()
new_link = doublets.create(source=1, target=2)
recent_links = doublets.search(limit=10)
```

## Architecture

### Core Components

1. **Doublets**: Main high-level interface
2. **DoubletsBackend**: Abstract backend interface
3. **GraphQLBackend**: GraphQL implementation
4. **MockBackend**: In-memory implementation for testing
5. **DeepClient**: Low-level GraphQL client
6. **Link**: Data structure representing a doublet

### Pluggable Backends

The architecture supports swapping backends without changing client code:

```python
# Start with GraphQL
doublets = Doublets(GraphQLBackend('http://server/graphql'))

# Later switch to native C++ library (when available)
# doublets._backend = NativeBackend('/path/to/library.so')

# Same API works with any backend
link = doublets.create(source=1, target=2)
```

## API Reference

### Doublets Class

The main interface for Doublets operations.

#### Methods

- `create(source=None, target=None) -> Link`: Create a new link
- `get(link_id) -> Optional[Link]`: Get link by ID
- `update(link_id, source, target) -> Link`: Update existing link
- `delete(link_id) -> bool`: Delete link by ID
- `search(source=None, target=None, limit=None, offset=None) -> List[Link]`: Search links
- `count(source=None, target=None) -> int`: Count matching links
- `each(callback, source=None, target=None)`: Iterate with callback

#### Pythonic Features

- `len(doublets)`: Get total link count
- `for link in doublets`: Iterate over all links
- `link_id in doublets`: Check if link exists
- List comprehensions: `[link for link in doublets if link.source == 1]`

### Link Class

Data structure representing a doublet.

```python
@dataclass
class Link:
    id: int      # Unique link identifier
    source: int  # Source link ID (from_id)
    target: int  # Target link ID (to_id)
```

### Backend Interface

To create custom backends, implement the `DoubletsBackend` abstract class:

```python
class CustomBackend(DoubletsBackend):
    def create(self, source=None, target=None) -> Link: ...
    def get(self, link_id) -> Optional[Link]: ...
    def update(self, link_id, source, target) -> Link: ...
    def delete(self, link_id) -> bool: ...
    def search(self, source=None, target=None, limit=None, offset=None) -> List[Link]: ...
    def count(self, source=None, target=None) -> int: ...
```

## GraphQL Client

For direct GraphQL usage without the high-level interface:

```python
from deepclient import DeepClient

client = DeepClient('http://localhost:60341/v1/graphql')

# Direct GraphQL operations
response = client.query('''
    query {
        links {
            id
            from_id
            to_id
        }
    }
''')

# Or use convenience methods
links = client.select(42, 'from_id', 'to_id')
new_link = client.insert_one(1, "test data", 'id', 'type_id')
```

## Examples

See the `examples/` directory for comprehensive usage examples:

- `basic_usage.py`: Basic operations with different backends
- `custom_backend.py`: Creating custom backend implementations

## Testing

Run the test suite:

```bash
# Install development dependencies
pip install -e .[dev]

# Run tests
python -m pytest tests/

# Run with coverage
python -m pytest tests/ --cov=deepclient --cov-report=html
```

## Future Development

### Native C++ Backend

The architecture is designed to support a future native C++ backend:

```python
# Future usage (when C++ library is ready)
from deepclient import NativeBackend

doublets = Doublets(NativeBackend('/path/to/doublets.so'))
# Same API, better performance
```

### Integration with PyO3

The package is designed to work with [PyO3](https://lib.rs/crates/pyo3) for Rust/C++ integration, as mentioned in the original issue.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Add tests for new functionality
4. Ensure all tests pass
5. Submit a pull request

## License

LGPLv3 - See LICENSE file for details.

## Links

- [Repository](https://github.com/linksplatform/Data.Doublets.Gql)
- [Issues](https://github.com/linksplatform/Data.Doublets.Gql/issues)
- [LinksPlatform Organization](https://github.com/linksplatform)
- [Discord Community](https://discord.gg/eEXJyjWv5e)

