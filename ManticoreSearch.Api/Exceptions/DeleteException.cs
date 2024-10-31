namespace ManticoreSearch.Api.Exceptions
{
    public class DeleteException : Exception
    {
        public DeleteException() : base() { }
        public DeleteException(string message) : base(message) { }
        public DeleteException(string message, Exception innerException) : base(string.Format(message, innerException.Message)) { }
    }
}
