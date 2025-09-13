package platform.data.doublets.native;

import org.junit.jupiter.api.Test;
import platform.data.doublets.client.DoubletsException;

import static org.junit.jupiter.api.Assertions.*;

class NativeLinksClientTest {

    @Test
    void testClientCreationFailsWithoutNativeLibrary() {
        // Since the native library is not available in test environment,
        // we expect the client creation to fail with UnsatisfiedLinkError
        assertThrows(UnsatisfiedLinkError.class, () -> {
            new NativeLinksClient("test.db");
        });
    }

    @Test
    void testClientCreationWithDefaultPath() {
        // Test that default constructor also fails without native library
        assertThrows(UnsatisfiedLinkError.class, () -> {
            new NativeLinksClient();
        });
    }
}