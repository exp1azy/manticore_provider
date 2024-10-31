namespace ManticoreSearch.Api.Exceptions
{
    public class UpdateException : Exception
    {
        public UpdateException() : base() { }
        public UpdateException(string message) : base(message) { }
        public UpdateException(string message, Exception innerException) : base(string.Format(message, innerException.Message)) { }
    }
}
