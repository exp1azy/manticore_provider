using ManticoreSearch.Api.Resources;

namespace ManticoreSearch.Api.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a null reference is encountered 
    /// where an object is required.
    /// </summary>
    public class NullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullException"/> class 
        /// with a default error message.
        /// </summary>
        public NullException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullException"/> class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NullException(string message) : base(string.Format(ProviderError.Null, message)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullException"/> class 
        /// with a specified error message and a reference to the inner exception 
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public NullException(string message, Exception innerException) : base(message, innerException) { }
    }
}
