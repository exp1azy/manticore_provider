namespace ManticoreSearch.Api.Exceptions
{
    public class InsertException : Exception
    {
        public InsertException() : base() { }

        public InsertException(string message) : base(message) { }

        public InsertException(string message, Exception innerException) : base(string.Format(message, innerException.Message)) { }
    }
}
