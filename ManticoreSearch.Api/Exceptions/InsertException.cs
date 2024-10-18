using ManticoreSearch.Api.Resources;

namespace ManticoreSearch.Api.Exceptions
{
    /// <summary>
    /// Represents errors that occur during the insertion of data into ManticoreSearch.
    /// This exception is thrown when there is an error executing an /insert request.
    /// </summary>
    public class InsertException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertException"/> class.
        /// </summary>
        public InsertException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InsertException(string message) : base(string.Format(ProviderError.InsertError, message)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertException"/> class
        /// with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, 
        /// or a null reference if no inner exception is specified.</param>
        public InsertException(string message, Exception innerException) : base(message, innerException) { }
    }
}
