using ManticoreSearch.Api.Resources;

namespace ManticoreSearch.Api.Exceptions
{
    /// <summary>
    /// Exception that occurs when an HTTP request does not result in a 200 (OK) status code.
    /// </summary>
    public class HttpRequestFailureException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestFailureException"/> class.
        /// </summary>
        public HttpRequestFailureException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestFailureException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public HttpRequestFailureException(string message) : base(string.Format(ProviderError.HttpRequestFailure, message)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestFailureException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public HttpRequestFailureException(string message, Exception innerException) : base(message, innerException) { }
    }
}
