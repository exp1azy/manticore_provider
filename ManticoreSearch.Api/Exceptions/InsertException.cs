using ManticoreSearch.Api.Resources;

namespace ManticoreSearch.Api.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an error occurs during the insertion
    /// of a document into the Manticore Search API. This exception is used to signal 
    /// issues that arise while attempting to insert documents, such as validation errors 
    /// or server-side failures.
    /// </summary>
    public class InsertException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertException"/> class 
        /// with a default error message.
        /// </summary>
        public InsertException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertException"/> class 
        /// with a specified error message. 
        /// The message is formatted using the <see cref="ProviderError.InsertError"/> 
        /// constant to provide a standardized error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InsertException(string message) : base(string.Format(ProviderError.InsertError, message)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertException"/> class 
        /// with a specified error message and a reference to the inner exception 
        /// that is the cause of this exception. This constructor is used to preserve 
        /// the stack trace of the inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InsertException(string message, Exception innerException) : base(message, innerException) { }
    }
}
