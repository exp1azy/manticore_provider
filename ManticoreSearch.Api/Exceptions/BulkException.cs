namespace ManticoreSearch.Api.Exceptions
{
    public class BulkException : Exception
    {
        public BulkException() : base() { }
        public BulkException(string message) : base(message) { }
        public BulkException(string message, Exception innerException) : base(string.Format(message, innerException.Message)) { }
    }
}
