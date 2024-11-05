namespace ManticoreSearch.Provider.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when the base address of the HTTP client is null.
    /// </summary>
    public class BaseAddressNullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAddressNullException"/> class.
        /// </summary>
        public BaseAddressNullException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAddressNullException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BaseAddressNullException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAddressNullException"/> class with a specified 
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference 
        /// if no inner exception is specified.</param>
        public BaseAddressNullException(string message, Exception innerException) : base(message, innerException) { }
    }
}
