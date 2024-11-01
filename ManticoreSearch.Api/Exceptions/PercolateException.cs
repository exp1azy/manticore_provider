namespace ManticoreSearch.Api.Exceptions
{
    public class PercolateException : Exception
    {
        public PercolateException() : base() { }
        public PercolateException(string message) : base(message) { }
        public PercolateException(string message, Exception innerException) : base(string.Format(message, innerException.Message)) { }
    }
}
