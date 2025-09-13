package doublets

import (
	"context"
	"testing"
)

func TestMemoryStore_CreateAndGet(t *testing.T) {
	store := NewMemoryStore()
	ctx := context.Background()

	// Test creating a link
	link, err := store.Create(ctx, 1, 2)
	if err != nil {
		t.Fatalf("Failed to create link: %v", err)
	}

	if link.FromID != 1 {
		t.Errorf("Expected FromID to be 1, got %d", link.FromID)
	}
	if link.ToID != 2 {
		t.Errorf("Expected ToID to be 2, got %d", link.ToID)
	}
	if link.ID == 0 {
		t.Error("Expected ID to be generated")
	}

	// Test getting the link
	retrieved, err := store.Get(ctx, link.ID)
	if err != nil {
		t.Fatalf("Failed to get link: %v", err)
	}

	if retrieved.ID != link.ID {
		t.Errorf("Expected ID %d, got %d", link.ID, retrieved.ID)
	}
	if retrieved.FromID != link.FromID {
		t.Errorf("Expected FromID %d, got %d", link.FromID, retrieved.FromID)
	}
	if retrieved.ToID != link.ToID {
		t.Errorf("Expected ToID %d, got %d", link.ToID, retrieved.ToID)
	}
}

func TestMemoryStore_Query(t *testing.T) {
	store := NewMemoryStore()
	ctx := context.Background()

	// Create test data
	link1, _ := store.Create(ctx, 1, 2)
	link2, _ := store.Create(ctx, 1, 3)
	link3, _ := store.Create(ctx, 2, 3)

	// Test query without filter
	all, err := store.Query(ctx, nil)
	if err != nil {
		t.Fatalf("Failed to query links: %v", err)
	}
	if len(all) != 3 {
		t.Errorf("Expected 3 links, got %d", len(all))
	}

	// Test query with FromID filter
	fromIDVal := uint64(1)
	filter := &QueryFilter{
		FromID: &IDFilter{Eq: &fromIDVal},
	}
	
	filtered, err := store.Query(ctx, filter)
	if err != nil {
		t.Fatalf("Failed to query filtered links: %v", err)
	}
	if len(filtered) != 2 {
		t.Errorf("Expected 2 links with FromID=1, got %d", len(filtered))
	}

	// Test count
	count, err := store.Count(ctx, filter)
	if err != nil {
		t.Fatalf("Failed to count links: %v", err)
	}
	if count != 2 {
		t.Errorf("Expected count of 2, got %d", count)
	}

	// Test GetOutgoing
	outgoing, err := store.GetOutgoing(ctx, 1, nil)
	if err != nil {
		t.Fatalf("Failed to get outgoing links: %v", err)
	}
	if len(outgoing) != 2 {
		t.Errorf("Expected 2 outgoing links from link 1, got %d", len(outgoing))
	}

	// Test GetIncoming
	incoming, err := store.GetIncoming(ctx, 3, nil)
	if err != nil {
		t.Fatalf("Failed to get incoming links: %v", err)
	}
	if len(incoming) != 2 {
		t.Errorf("Expected 2 incoming links to link 3, got %d", len(incoming))
	}

	// Verify the created links have correct IDs
	expectedIDs := []uint64{link1.ID, link2.ID, link3.ID}
	for i, link := range all {
		if link.ID != expectedIDs[i] {
			t.Errorf("Expected link %d to have ID %d, got %d", i, expectedIDs[i], link.ID)
		}
	}
}

func TestMemoryStore_Update(t *testing.T) {
	store := NewMemoryStore()
	ctx := context.Background()

	// Create a link
	original, err := store.Create(ctx, 1, 2)
	if err != nil {
		t.Fatalf("Failed to create link: %v", err)
	}

	// Update the link
	updated, err := store.Update(ctx, original.ID, 3, 4)
	if err != nil {
		t.Fatalf("Failed to update link: %v", err)
	}

	if updated.ID != original.ID {
		t.Errorf("Expected ID to remain %d, got %d", original.ID, updated.ID)
	}
	if updated.FromID != 3 {
		t.Errorf("Expected FromID to be 3, got %d", updated.FromID)
	}
	if updated.ToID != 4 {
		t.Errorf("Expected ToID to be 4, got %d", updated.ToID)
	}

	// Verify the update persisted
	retrieved, err := store.Get(ctx, original.ID)
	if err != nil {
		t.Fatalf("Failed to get updated link: %v", err)
	}
	if retrieved.FromID != 3 || retrieved.ToID != 4 {
		t.Errorf("Update did not persist: FromID=%d, ToID=%d", retrieved.FromID, retrieved.ToID)
	}
}

func TestMemoryStore_Delete(t *testing.T) {
	store := NewMemoryStore()
	ctx := context.Background()

	// Create a link
	link, err := store.Create(ctx, 1, 2)
	if err != nil {
		t.Fatalf("Failed to create link: %v", err)
	}

	// Verify it exists
	_, err = store.Get(ctx, link.ID)
	if err != nil {
		t.Fatalf("Link should exist before deletion: %v", err)
	}

	// Delete the link
	err = store.Delete(ctx, link.ID)
	if err != nil {
		t.Fatalf("Failed to delete link: %v", err)
	}

	// Verify it's gone
	_, err = store.Get(ctx, link.ID)
	if err == nil {
		t.Error("Link should not exist after deletion")
	}
}

func TestIDFilter_Matching(t *testing.T) {
	store := NewMemoryStore()

	// Test different comparison operators
	testCases := []struct {
		name     string
		filter   *IDFilter
		value    uint64
		expected bool
	}{
		{"Equal match", &IDFilter{Eq: uint64Ptr(5)}, 5, true},
		{"Equal no match", &IDFilter{Eq: uint64Ptr(5)}, 6, false},
		{"Not equal match", &IDFilter{Neq: uint64Ptr(5)}, 6, true},
		{"Not equal no match", &IDFilter{Neq: uint64Ptr(5)}, 5, false},
		{"Greater than match", &IDFilter{Gt: uint64Ptr(5)}, 6, true},
		{"Greater than no match", &IDFilter{Gt: uint64Ptr(5)}, 5, false},
		{"Greater than equal match", &IDFilter{Gte: uint64Ptr(5)}, 5, true},
		{"Greater than equal no match", &IDFilter{Gte: uint64Ptr(5)}, 4, false},
		{"Less than match", &IDFilter{Lt: uint64Ptr(5)}, 4, true},
		{"Less than no match", &IDFilter{Lt: uint64Ptr(5)}, 5, false},
		{"Less than equal match", &IDFilter{Lte: uint64Ptr(5)}, 5, true},
		{"Less than equal no match", &IDFilter{Lte: uint64Ptr(5)}, 6, false},
		{"In match", &IDFilter{In: []uint64{1, 2, 3}}, 2, true},
		{"In no match", &IDFilter{In: []uint64{1, 2, 3}}, 4, false},
		{"Not in match", &IDFilter{Nin: []uint64{1, 2, 3}}, 4, true},
		{"Not in no match", &IDFilter{Nin: []uint64{1, 2, 3}}, 2, false},
	}

	for _, tc := range testCases {
		t.Run(tc.name, func(t *testing.T) {
			result := store.matchesIDFilter(tc.value, tc.filter)
			if result != tc.expected {
				t.Errorf("Expected %v, got %v for value %d with filter %+v", tc.expected, result, tc.value, tc.filter)
			}
		})
	}
}

// Helper function to create uint64 pointer
func uint64Ptr(v uint64) *uint64 {
	return &v
}

// Benchmark tests to demonstrate performance benefits
func BenchmarkMemoryStore_Create(b *testing.B) {
	store := NewMemoryStore()
	ctx := context.Background()

	b.ResetTimer()
	for i := 0; i < b.N; i++ {
		store.Create(ctx, uint64(i), uint64(i+1))
	}
}

func BenchmarkMemoryStore_Query(b *testing.B) {
	store := NewMemoryStore()
	ctx := context.Background()

	// Create test data
	for i := 0; i < 1000; i++ {
		store.Create(ctx, uint64(i%10), uint64((i+1)%10))
	}

	filter := &QueryFilter{
		FromID: &IDFilter{Eq: uint64Ptr(5)},
	}

	b.ResetTimer()
	for i := 0; i < b.N; i++ {
		store.Query(ctx, filter)
	}
}