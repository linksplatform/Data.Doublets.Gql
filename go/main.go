package main

import (
	"context"
	"log"
	"net/http"
	"os"

	"doublets-gql/doublets"
	"doublets-gql/generated"
	"doublets-gql/resolvers"

	"github.com/99designs/gqlgen/graphql/handler"
	"github.com/99designs/gqlgen/graphql/playground"
	"github.com/gorilla/mux"
)

const defaultPort = "8080"

func main() {
	port := os.Getenv("PORT")
	if port == "" {
		port = defaultPort
	}

	// Initialize the doublets store
	// In production, this would use the actual doublets file store
	var store doublets.Store
	
	dbPath := "db.links"
	indexPath := "index.links"
	
	// Check if we have database files or command line arguments
	if len(os.Args) > 1 {
		dbPath = os.Args[1]
	}
	if len(os.Args) > 2 {
		indexPath = os.Args[2]
	}
	
	// Try to use file store, fall back to memory store for demonstration
	fileStore, err := doublets.NewFileStore(dbPath, indexPath)
	if err != nil {
		log.Printf("Failed to initialize file store: %v, using in-memory store", err)
		store = doublets.NewMemoryStore()
		
		// Add some sample data for demonstration
		ctx := context.Background()
		store.Create(ctx, 1, 1)
		store.Create(ctx, 1, 2)
		store.Create(ctx, 2, 3)
	} else {
		log.Printf("Note: File store created but not fully implemented. Using memory store for demo.")
		store = doublets.NewMemoryStore()
		
		// Add some sample data for demonstration
		ctx := context.Background()
		store.Create(ctx, 1, 1)
		store.Create(ctx, 1, 2)
		store.Create(ctx, 2, 3)
	}

	// Create resolver with store
	resolver := resolvers.NewResolver(store)

	// Create GraphQL server
	// In a real gqlgen setup, this would use the generated schema
	srv := handler.NewDefaultServer(generated.NewExecutableSchema(generated.Config{
		Resolvers: resolver,
	}))

	// Set up routes
	router := mux.NewRouter()
	
	// GraphQL endpoint
	router.Handle("/", srv).Methods("POST")
	router.Handle("/query", srv).Methods("POST")
	router.Handle("/v1/graphql", srv).Methods("POST")
	
	// GraphQL playground endpoints (same as other implementations)
	router.Handle("/ui/playground", playground.Handler("GraphQL playground", "/")).Methods("GET")
	router.Handle("/ui/graphiql", playground.Handler("GraphiQL", "/")).Methods("GET")
	router.Handle("/ui/altair", playground.Handler("Altair", "/")).Methods("GET")
	router.Handle("/ui/voyager", playground.Handler("Voyager", "/")).Methods("GET")
	
	// Root playground for compatibility
	router.Handle("/", playground.Handler("GraphQL playground", "/")).Methods("GET")

	log.Printf("Starting GraphQL server on http://localhost:%s", port)
	log.Printf("GraphQL playground: http://localhost:%s/ui/playground", port)
	log.Printf("GraphiQL: http://localhost:%s/ui/graphiql", port)
	log.Printf("Altair: http://localhost:%s/ui/altair", port)
	log.Printf("Voyager: http://localhost:%s/ui/voyager", port)
	log.Printf("GraphQL endpoint: http://localhost:%s/v1/graphql", port)

	log.Fatal(http.ListenAndServe(":"+port, router))
}