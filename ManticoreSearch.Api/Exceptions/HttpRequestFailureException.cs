using ManticoreSearch.Api.Resources;

namespace ManticoreSearch.Api.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an HTTP request to the Manticore Search API fails.
    /// This exception is used to signal issues that occur during the execution of HTTP requests,
    /// such as network errors, invalid responses, or server-side errors.
    /// </summary>
    public class HttpRequestFailureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestFailureException"/> class 
        /// with a default error message.
        /// </summary>
        public HttpRequestFailureException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestFailureException"/> class 
        /// with a specified error message. 
        /// The message is formatted using the <see cref="ProviderError.HttpRequestFailure"/> 
        /// constant to provide a standardized error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public HttpRequestFailureException(string message) : base(string.Format(ProviderError.HttpRequestFailure, message)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestFailureException"/> class 
        /// with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// This constructor is used to preserve the stack trace of the inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public HttpRequestFailureException(string message, Exception innerException) : base(message, innerException) { }
    }
}
