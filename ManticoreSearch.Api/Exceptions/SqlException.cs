using ManticoreSearch.Api.Resources;

namespace ManticoreSearch.Api.Exceptions
{
    /// <summary>
    /// Represents SQL-related errors that occur during database operations.
    /// </summary>
    public class SqlException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlException"/> class 
        /// with a default SQL error message.
        /// </summary>
        public SqlException() : base(ExceptionError.SqlError) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlException"/> class 
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public SqlException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlException"/> class 
        /// with a specified error message and a reference to the inner exception 
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public SqlException(string message, Exception innerException)
            : base(string.Format(message, innerException.Message)) { }
    }
}
