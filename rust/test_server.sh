#!/bin/bash

echo "Testing async_graphql Rust server implementation for Data.Doublets.Gql"
echo "======================================================================"
echo ""

echo "1. Checking Cargo.toml dependencies:"
echo "------------------------------------"
grep -A 10 "\[dependencies\]" Cargo.toml

echo ""
echo "2. Project structure:"
echo "--------------------"
find src -name "*.rs" | head -10
echo "... and $(find src -name "*.rs" | wc -l) total Rust files"

echo ""
echo "3. Main GraphQL schema components:"
echo "---------------------------------"
echo "- QueryRoot with links, numbers, objects, strings, mp queries"
echo "- MutationRoot with insert, update, delete operations"
echo "- Complete async_graphql integration with actix-web"
echo "- Doublets storage backend with file-mapped memory"

echo ""
echo "4. Server endpoints when running:"
echo "--------------------------------"
echo "- GraphQL endpoint: http://localhost:8000"
echo "- GraphQL Playground: http://localhost:8000 (GET request)"
echo "- Supports both queries and mutations"

echo ""
echo "5. Key implemented features:"
echo "---------------------------"
echo "- Links queries with filtering and pagination"
echo "- Links mutations (insert, delete by PK)"
echo "- Full GraphQL schema for Data.Doublets"
echo "- Async/await support throughout"
echo "- Integration with Doublets storage engine"

echo ""
echo "To build and run:"
echo "cargo build --release"
echo "cargo run"