using ManticoreSearch.Api.Resources;

namespace ManticoreSearch.Api.Exceptions
{
    public class SqlException : Exception
    {
        public SqlException() : base(ExceptionError.SqlError) { }
        public SqlException(string message) : base(message) { }
        public SqlException(string message, Exception innerException) : base(string.Format(message, innerException.Message)) { }
    }
}
