namespace ManticoreSearch.Api.Exceptions
{
    public class ReplaceException : Exception
    {
        public ReplaceException() : base() { }
        public ReplaceException(string message) : base(message) { }
        public ReplaceException(string message, Exception innerException) : base(string.Format(message, innerException.Message)) { }
    }
}
