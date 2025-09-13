package platform.data.doublets.native;

import platform.data.doublets.client.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

/**
 * Native implementation of the LinksClient interface.
 * Uses JNI to communicate with the native Platform Data Doublets library.
 */
public class NativeLinksClient implements LinksClient {
    
    private long nativeHandle;
    private final String databasePath;
    
    static {
        // Load the native library
        System.loadLibrary("platform-data-doublets-native");
    }
    
    /**
     * Creates a new native client with the specified database path.
     *
     * @param databasePath the path to the database file
     * @throws DoubletsException if initialization fails
     */
    public NativeLinksClient(String databasePath) throws DoubletsException {
        this.databasePath = databasePath;
        this.nativeHandle = nativeInit(databasePath);
        if (this.nativeHandle == 0) {
            throw new DoubletsException("Failed to initialize native client");
        }
    }
    
    /**
     * Creates a new native client with the default database path.
     *
     * @throws DoubletsException if initialization fails
     */
    public NativeLinksClient() throws DoubletsException {
        this("db.links");
    }

    @Override
    public Link getOrCreate(long fromId, long toId) throws DoubletsException {
        long linkId = nativeGetOrCreate(nativeHandle, fromId, toId);
        if (linkId == 0) {
            throw new DoubletsException("Failed to get or create link");
        }
        return new Link(linkId, fromId, toId);
    }

    @Override
    public Link create(long fromId, long toId) throws DoubletsException {
        long linkId = nativeCreate(nativeHandle, fromId, toId);
        if (linkId == 0) {
            throw new DoubletsException("Failed to create link");
        }
        return new Link(linkId, fromId, toId);
    }

    @Override
    public Link update(long linkId, long newFromId, long newToId) throws DoubletsException {
        long updatedLinkId = nativeUpdate(nativeHandle, linkId, newFromId, newToId);
        if (updatedLinkId == 0) {
            throw new DoubletsException("Failed to update link with id: " + linkId);
        }
        return new Link(updatedLinkId, newFromId, newToId);
    }

    @Override
    public void delete(long linkId) throws DoubletsException {
        boolean success = nativeDelete(nativeHandle, linkId);
        if (!success) {
            throw new DoubletsException("Failed to delete link with id: " + linkId);
        }
    }

    @Override
    public Optional<Link> getLink(long linkId) throws DoubletsException {
        long[] linkData = nativeGetLink(nativeHandle, linkId);
        if (linkData == null || linkData.length != 3) {
            return Optional.empty();
        }
        return Optional.of(new Link(linkData[0], linkData[1], linkData[2]));
    }

    @Override
    public List<Link> searchLinks(LinkQuery query) throws DoubletsException {
        long queryHandle = nativeCreateQuery(nativeHandle);
        
        try {
            if (query.getId().isPresent()) {
                nativeQuerySetId(queryHandle, query.getId().get());
            }
            if (query.getFromId().isPresent()) {
                nativeQuerySetFromId(queryHandle, query.getFromId().get());
            }
            if (query.getToId().isPresent()) {
                nativeQuerySetToId(queryHandle, query.getToId().get());
            }
            if (query.getLimit().isPresent()) {
                nativeQuerySetLimit(queryHandle, query.getLimit().get());
            }
            if (query.getOffset().isPresent()) {
                nativeQuerySetOffset(queryHandle, query.getOffset().get());
            }
            if (query.getSortField().isPresent() && query.getSortOrder().isPresent()) {
                int sortField = mapSortField(query.getSortField().get());
                boolean ascending = query.getSortOrder().get() == LinkQuery.SortOrder.ASC;
                nativeQuerySetSort(queryHandle, sortField, ascending);
            }
            
            long[][] results = nativeExecuteQuery(queryHandle);
            List<Link> links = new ArrayList<>();
            
            if (results != null) {
                for (long[] linkData : results) {
                    if (linkData.length == 3) {
                        links.add(new Link(linkData[0], linkData[1], linkData[2]));
                    }
                }
            }
            
            return links;
            
        } finally {
            nativeDestroyQuery(queryHandle);
        }
    }

    @Override
    public List<Link> getAllLinks() throws DoubletsException {
        LinkQuery query = LinkQuery.builder().build();
        return searchLinks(query);
    }

    @Override
    public List<Link> getLinksByFrom(long fromId) throws DoubletsException {
        LinkQuery query = LinkQuery.builder()
                .fromId(fromId)
                .build();
        return searchLinks(query);
    }

    @Override
    public List<Link> getLinksByTo(long toId) throws DoubletsException {
        LinkQuery query = LinkQuery.builder()
                .toId(toId)
                .build();
        return searchLinks(query);
    }

    @Override
    public long getLinksCount() throws DoubletsException {
        return nativeGetCount(nativeHandle);
    }
    
    /**
     * Closes the native client and releases resources.
     * Should be called when the client is no longer needed.
     */
    public void close() {
        if (nativeHandle != 0) {
            nativeClose(nativeHandle);
            nativeHandle = 0;
        }
    }
    
    @Override
    protected void finalize() throws Throwable {
        close();
        super.finalize();
    }
    
    private int mapSortField(LinkQuery.SortField field) {
        return switch (field) {
            case ID -> 0;
            case FROM_ID -> 1;
            case TO_ID -> 2;
        };
    }
    
    // Native method declarations
    private native long nativeInit(String databasePath);
    private native void nativeClose(long handle);
    private native long nativeGetOrCreate(long handle, long fromId, long toId);
    private native long nativeCreate(long handle, long fromId, long toId);
    private native long nativeUpdate(long handle, long linkId, long newFromId, long newToId);
    private native boolean nativeDelete(long handle, long linkId);
    private native long[] nativeGetLink(long handle, long linkId);
    private native long nativeGetCount(long handle);
    
    // Query-related native methods
    private native long nativeCreateQuery(long handle);
    private native void nativeDestroyQuery(long queryHandle);
    private native void nativeQuerySetId(long queryHandle, long id);
    private native void nativeQuerySetFromId(long queryHandle, long fromId);
    private native void nativeQuerySetToId(long queryHandle, long toId);
    private native void nativeQuerySetLimit(long queryHandle, int limit);
    private native void nativeQuerySetOffset(long queryHandle, int offset);
    private native void nativeQuerySetSort(long queryHandle, int field, boolean ascending);
    private native long[][] nativeExecuteQuery(long queryHandle);
}