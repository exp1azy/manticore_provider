namespace ManticoreSearch.Api.Exceptions
{
    /// <summary>
    /// Represents errors that occur during search operations in Manticore.
    /// </summary>
    public class SearchException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchException"/> class.
        /// </summary>
        public SearchException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchException"/> class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public SearchException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchException"/> class 
        /// with a specified error message and a reference to the inner exception 
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public SearchException(string message, Exception innerException)
            : base(string.Format(message, innerException.Message)) { }
    }
}
