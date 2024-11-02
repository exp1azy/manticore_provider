namespace ManticoreSearch.Provider.Exceptions
{
    /// <summary>
    /// Represents errors that occur during mapping operations in Manticore.
    /// </summary>
    public class MappingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingException"/> class.
        /// </summary>
        public MappingException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingException"/> class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MappingException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingException"/> class 
        /// with a specified error message and a reference to the inner exception 
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MappingException(string message, Exception innerException)
            : base(string.Format(message, innerException)) { }
    }
}
