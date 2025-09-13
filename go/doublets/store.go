package doublets

import (
	"context"
	"errors"
	"fmt"
	"os"
	"sync"
)

// Link represents a doublet - a connection between two entities
type Link struct {
	ID     uint64 `json:"id"`
	FromID uint64 `json:"from_id"`
	ToID   uint64 `json:"to_id"`
}

// Store provides the interface for doublets storage operations
type Store interface {
	// Create a new link
	Create(ctx context.Context, fromID, toID uint64) (*Link, error)
	
	// Get a link by ID
	Get(ctx context.Context, id uint64) (*Link, error)
	
	// Update a link
	Update(ctx context.Context, id, fromID, toID uint64) (*Link, error)
	
	// Delete a link
	Delete(ctx context.Context, id uint64) error
	
	// Query links with filtering
	Query(ctx context.Context, filter *QueryFilter) ([]*Link, error)
	
	// Count links matching filter
	Count(ctx context.Context, filter *QueryFilter) (int64, error)
	
	// Get links that point from the given link ID
	GetOutgoing(ctx context.Context, fromID uint64, filter *QueryFilter) ([]*Link, error)
	
	// Get links that point to the given link ID
	GetIncoming(ctx context.Context, toID uint64, filter *QueryFilter) ([]*Link, error)
}

// QueryFilter defines filtering, ordering, and pagination options
type QueryFilter struct {
	// Filtering
	ID     *IDFilter `json:"id,omitempty"`
	FromID *IDFilter `json:"from_id,omitempty"`
	ToID   *IDFilter `json:"to_id,omitempty"`
	
	// Logical operators
	And []*QueryFilter `json:"_and,omitempty"`
	Or  []*QueryFilter `json:"_or,omitempty"`
	Not *QueryFilter   `json:"_not,omitempty"`
	
	// Ordering
	OrderBy []*OrderBy `json:"order_by,omitempty"`
	
	// Pagination
	Offset *int `json:"offset,omitempty"`
	Limit  *int `json:"limit,omitempty"`
	
	// Distinct selection
	DistinctOn []string `json:"distinct_on,omitempty"`
}

// IDFilter provides comparison operations for ID fields
type IDFilter struct {
	Eq  *uint64   `json:"_eq,omitempty"`
	Neq *uint64   `json:"_neq,omitempty"`
	Gt  *uint64   `json:"_gt,omitempty"`
	Gte *uint64   `json:"_gte,omitempty"`
	Lt  *uint64   `json:"_lt,omitempty"`
	Lte *uint64   `json:"_lte,omitempty"`
	In  []uint64  `json:"_in,omitempty"`
	Nin []uint64  `json:"_nin,omitempty"`
}

// OrderBy defines ordering for query results
type OrderBy struct {
	Field     string    `json:"field"`
	Direction Direction `json:"direction"`
}

// Direction represents sort direction
type Direction string

const (
	DirectionAsc  Direction = "ASC"
	DirectionDesc Direction = "DESC"
)

// MemoryStore is a simple in-memory implementation for demonstration
// In production, this would connect to the actual doublets storage system
type MemoryStore struct {
	links   map[uint64]*Link
	nextID  uint64
	mutex   sync.RWMutex
}

// NewMemoryStore creates a new in-memory store
func NewMemoryStore() *MemoryStore {
	return &MemoryStore{
		links:  make(map[uint64]*Link),
		nextID: 1,
	}
}

// Create implements Store.Create
func (s *MemoryStore) Create(ctx context.Context, fromID, toID uint64) (*Link, error) {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	
	link := &Link{
		ID:     s.nextID,
		FromID: fromID,
		ToID:   toID,
	}
	
	s.links[s.nextID] = link
	s.nextID++
	
	return link, nil
}

// Get implements Store.Get
func (s *MemoryStore) Get(ctx context.Context, id uint64) (*Link, error) {
	s.mutex.RLock()
	defer s.mutex.RUnlock()
	
	link, exists := s.links[id]
	if !exists {
		return nil, errors.New("link not found")
	}
	
	return link, nil
}

// Update implements Store.Update
func (s *MemoryStore) Update(ctx context.Context, id, fromID, toID uint64) (*Link, error) {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	
	link, exists := s.links[id]
	if !exists {
		return nil, errors.New("link not found")
	}
	
	link.FromID = fromID
	link.ToID = toID
	
	return link, nil
}

// Delete implements Store.Delete
func (s *MemoryStore) Delete(ctx context.Context, id uint64) error {
	s.mutex.Lock()
	defer s.mutex.Unlock()
	
	if _, exists := s.links[id]; !exists {
		return errors.New("link not found")
	}
	
	delete(s.links, id)
	return nil
}

// Query implements Store.Query
func (s *MemoryStore) Query(ctx context.Context, filter *QueryFilter) ([]*Link, error) {
	s.mutex.RLock()
	defer s.mutex.RUnlock()
	
	var results []*Link
	
	for _, link := range s.links {
		if s.matchesFilter(link, filter) {
			results = append(results, link)
		}
	}
	
	// Apply ordering, pagination, etc.
	results = s.applyOrdering(results, filter)
	results = s.applyPagination(results, filter)
	
	return results, nil
}

// Count implements Store.Count
func (s *MemoryStore) Count(ctx context.Context, filter *QueryFilter) (int64, error) {
	s.mutex.RLock()
	defer s.mutex.RUnlock()
	
	count := int64(0)
	for _, link := range s.links {
		if s.matchesFilter(link, filter) {
			count++
		}
	}
	
	return count, nil
}

// GetOutgoing implements Store.GetOutgoing
func (s *MemoryStore) GetOutgoing(ctx context.Context, fromID uint64, filter *QueryFilter) ([]*Link, error) {
	if filter == nil {
		filter = &QueryFilter{}
	}
	
	// Add fromID constraint to filter
	if filter.FromID == nil {
		filter.FromID = &IDFilter{}
	}
	filter.FromID.Eq = &fromID
	
	return s.Query(ctx, filter)
}

// GetIncoming implements Store.GetIncoming
func (s *MemoryStore) GetIncoming(ctx context.Context, toID uint64, filter *QueryFilter) ([]*Link, error) {
	if filter == nil {
		filter = &QueryFilter{}
	}
	
	// Add toID constraint to filter
	if filter.ToID == nil {
		filter.ToID = &IDFilter{}
	}
	filter.ToID.Eq = &toID
	
	return s.Query(ctx, filter)
}

// Helper methods for filtering and sorting

func (s *MemoryStore) matchesFilter(link *Link, filter *QueryFilter) bool {
	if filter == nil {
		return true
	}
	
	// Check ID filter
	if filter.ID != nil && !s.matchesIDFilter(link.ID, filter.ID) {
		return false
	}
	
	// Check FromID filter
	if filter.FromID != nil && !s.matchesIDFilter(link.FromID, filter.FromID) {
		return false
	}
	
	// Check ToID filter
	if filter.ToID != nil && !s.matchesIDFilter(link.ToID, filter.ToID) {
		return false
	}
	
	// Check logical operators
	if filter.And != nil {
		for _, andFilter := range filter.And {
			if !s.matchesFilter(link, andFilter) {
				return false
			}
		}
	}
	
	if filter.Or != nil {
		found := false
		for _, orFilter := range filter.Or {
			if s.matchesFilter(link, orFilter) {
				found = true
				break
			}
		}
		if !found {
			return false
		}
	}
	
	if filter.Not != nil && s.matchesFilter(link, filter.Not) {
		return false
	}
	
	return true
}

func (s *MemoryStore) matchesIDFilter(value uint64, filter *IDFilter) bool {
	if filter.Eq != nil && value != *filter.Eq {
		return false
	}
	if filter.Neq != nil && value == *filter.Neq {
		return false
	}
	if filter.Gt != nil && value <= *filter.Gt {
		return false
	}
	if filter.Gte != nil && value < *filter.Gte {
		return false
	}
	if filter.Lt != nil && value >= *filter.Lt {
		return false
	}
	if filter.Lte != nil && value > *filter.Lte {
		return false
	}
	if filter.In != nil {
		found := false
		for _, v := range filter.In {
			if value == v {
				found = true
				break
			}
		}
		if !found {
			return false
		}
	}
	if filter.Nin != nil {
		for _, v := range filter.Nin {
			if value == v {
				return false
			}
		}
	}
	
	return true
}

func (s *MemoryStore) applyOrdering(links []*Link, filter *QueryFilter) []*Link {
	// Simple implementation - in production would use more efficient sorting
	// For now, just return as-is since this is a demonstration
	return links
}

func (s *MemoryStore) applyPagination(links []*Link, filter *QueryFilter) []*Link {
	if filter == nil {
		return links
	}
	
	start := 0
	if filter.Offset != nil {
		start = *filter.Offset
	}
	
	if start >= len(links) {
		return []*Link{}
	}
	
	end := len(links)
	if filter.Limit != nil {
		end = start + *filter.Limit
		if end > len(links) {
			end = len(links)
		}
	}
	
	return links[start:end]
}

// FileStore would be the production implementation that interfaces with
// the actual doublets file storage system. This is a placeholder.
type FileStore struct {
	dbPath    string
	indexPath string
}

// NewFileStore creates a new file-based store
func NewFileStore(dbPath, indexPath string) (*FileStore, error) {
	// Ensure files exist
	for _, path := range []string{dbPath, indexPath} {
		if _, err := os.Stat(path); os.IsNotExist(err) {
			file, err := os.Create(path)
			if err != nil {
				return nil, fmt.Errorf("failed to create file %s: %w", path, err)
			}
			file.Close()
		}
	}
	
	return &FileStore{
		dbPath:    dbPath,
		indexPath: indexPath,
	}, nil
}

// Implementation methods for FileStore would go here
// For now, these would delegate to the actual doublets storage library
func (f *FileStore) Create(ctx context.Context, fromID, toID uint64) (*Link, error) {
	// TODO: Implement using actual doublets storage
	return nil, errors.New("not implemented - would use actual doublets storage")
}

func (f *FileStore) Get(ctx context.Context, id uint64) (*Link, error) {
	// TODO: Implement using actual doublets storage
	return nil, errors.New("not implemented - would use actual doublets storage")
}

func (f *FileStore) Update(ctx context.Context, id, fromID, toID uint64) (*Link, error) {
	// TODO: Implement using actual doublets storage
	return nil, errors.New("not implemented - would use actual doublets storage")
}

func (f *FileStore) Delete(ctx context.Context, id uint64) error {
	// TODO: Implement using actual doublets storage
	return errors.New("not implemented - would use actual doublets storage")
}

func (f *FileStore) Query(ctx context.Context, filter *QueryFilter) ([]*Link, error) {
	// TODO: Implement using actual doublets storage
	return nil, errors.New("not implemented - would use actual doublets storage")
}

func (f *FileStore) Count(ctx context.Context, filter *QueryFilter) (int64, error) {
	// TODO: Implement using actual doublets storage
	return 0, errors.New("not implemented - would use actual doublets storage")
}

func (f *FileStore) GetOutgoing(ctx context.Context, fromID uint64, filter *QueryFilter) ([]*Link, error) {
	// TODO: Implement using actual doublets storage
	return nil, errors.New("not implemented - would use actual doublets storage")
}

func (f *FileStore) GetIncoming(ctx context.Context, toID uint64, filter *QueryFilter) ([]*Link, error) {
	// TODO: Implement using actual doublets storage
	return nil, errors.New("not implemented - would use actual doublets storage")
}