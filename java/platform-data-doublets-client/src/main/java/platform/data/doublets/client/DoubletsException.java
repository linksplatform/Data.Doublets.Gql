package platform.data.doublets.client;

/**
 * Exception thrown when operations on the doublets data structure fail.
 */
public class DoubletsException extends Exception {
    
    /**
     * Creates a new DoubletsException with the specified message.
     *
     * @param message the error message
     */
    public DoubletsException(String message) {
        super(message);
    }
    
    /**
     * Creates a new DoubletsException with the specified message and cause.
     *
     * @param message the error message
     * @param cause   the underlying cause
     */
    public DoubletsException(String message, Throwable cause) {
        super(message, cause);
    }
    
    /**
     * Creates a new DoubletsException with the specified cause.
     *
     * @param cause the underlying cause
     */
    public DoubletsException(Throwable cause) {
        super(cause);
    }
}