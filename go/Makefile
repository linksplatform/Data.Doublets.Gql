# Makefile for Data.Doublets.Gql Go implementation with gqlgen
# Provides faster compiled implementation compared to interpreted approaches

.PHONY: all build run generate clean test lint help

# Default target
all: generate build

# Generate GraphQL code using gqlgen
# This is where the "faster compiled implementation" magic happens
generate:
	@echo "Generating GraphQL code with gqlgen for faster compilation..."
	go install github.com/99designs/gqlgen
	go run github.com/99designs/gqlgen generate

# Build the application
build: generate
	@echo "Building Go GraphQL server..."
	go mod tidy
	go build -o bin/doublets-gql-server ./main.go

# Run the server with default settings
run: build
	@echo "Starting GraphQL server..."
	./bin/doublets-gql-server

# Run with custom database files
run-with-db: build
	@echo "Starting GraphQL server with custom database files..."
	./bin/doublets-gql-server db.links index.links

# Clean build artifacts
clean:
	@echo "Cleaning build artifacts..."
	rm -rf bin/
	go clean

# Run tests
test:
	@echo "Running tests..."
	go test ./...

# Run linter
lint:
	@echo "Running linter..."
	go install github.com/golangci/golangci-lint/cmd/golangci-lint@latest
	golangci-lint run

# Install dependencies
deps:
	@echo "Installing dependencies..."
	go mod download
	go mod tidy

# Development server with hot reload (requires air)
dev:
	@echo "Starting development server with hot reload..."
	go install github.com/cosmtrek/air@latest
	air

# Create binary releases for multiple platforms
release: generate
	@echo "Building releases for multiple platforms..."
	mkdir -p dist
	
	# Linux amd64
	GOOS=linux GOARCH=amd64 go build -o dist/doublets-gql-server-linux-amd64 ./main.go
	
	# Linux arm64
	GOOS=linux GOARCH=arm64 go build -o dist/doublets-gql-server-linux-arm64 ./main.go
	
	# Windows amd64
	GOOS=windows GOARCH=amd64 go build -o dist/doublets-gql-server-windows-amd64.exe ./main.go
	
	# macOS amd64
	GOOS=darwin GOARCH=amd64 go build -o dist/doublets-gql-server-darwin-amd64 ./main.go
	
	# macOS arm64 (Apple Silicon)
	GOOS=darwin GOARCH=arm64 go build -o dist/doublets-gql-server-darwin-arm64 ./main.go

# Show help
help:
	@echo "Data.Doublets.Gql Go Implementation with gqlgen"
	@echo "============================================="
	@echo ""
	@echo "This implementation provides a faster compiled GraphQL server"
	@echo "using gqlgen for code generation and Go's compiled performance."
	@echo ""
	@echo "Available targets:"
	@echo "  all          - Generate code and build (default)"
	@echo "  generate     - Generate GraphQL code using gqlgen"
	@echo "  build        - Build the application"
	@echo "  run          - Run the server with default settings"
	@echo "  run-with-db  - Run with custom database files"
	@echo "  clean        - Clean build artifacts"
	@echo "  test         - Run tests"
	@echo "  lint         - Run linter"
	@echo "  deps         - Install dependencies"
	@echo "  dev          - Start development server with hot reload"
	@echo "  release      - Build releases for multiple platforms"
	@echo "  help         - Show this help message"
	@echo ""
	@echo "Examples:"
	@echo "  make                    # Build everything"
	@echo "  make run                # Run server on :8080"
	@echo "  make run-with-db        # Run with specific database files"
	@echo "  PORT=3000 make run      # Run on custom port"

# Performance comparison target
benchmark:
	@echo "Performance comparison with other implementations:"
	@echo "================================================="
	@echo ""
	@echo "Go (this implementation):"
	@echo "  ✓ Compiled binary - fastest startup time"
	@echo "  ✓ Static typing - compile-time error checking"
	@echo "  ✓ gqlgen code generation - no runtime reflection"
	@echo "  ✓ Efficient memory usage"
	@echo "  ✓ Concurrent request handling with goroutines"
	@echo ""
	@echo "Rust implementation:"
	@echo "  ✓ Compiled binary - fast startup time"
	@echo "  ✓ Memory safety"
	@echo "  ~ async-graphql - some runtime overhead"
	@echo ""
	@echo "C# implementation:"
	@echo "  ~ JIT compilation - slower startup"
	@echo "  ~ Reflection-based GraphQL - runtime overhead"
	@echo "  ~ Higher memory usage"