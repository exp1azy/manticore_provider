namespace ManticoreSearch.Api.Exceptions
{
    public class SearchException : Exception
    {
        public SearchException() : base() { }
        public SearchException(string message) : base(message) { }
        public SearchException(string message, Exception innerException) : base(string.Format(message, innerException.Message)) { }
    }
}
