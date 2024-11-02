namespace ManticoreSearch.Provider.Exceptions
{
    /// <summary>
    /// Represents errors that occur during update operations in the application.
    /// </summary>
    public class UpdateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class.
        /// </summary>
        public UpdateException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UpdateException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class 
        /// with a specified error message and a reference to the inner exception 
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public UpdateException(string message, Exception innerException)
            : base(string.Format(message, innerException.Message)) { }
    }
}
