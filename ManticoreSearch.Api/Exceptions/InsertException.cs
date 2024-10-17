using ManticoreSearch.Api.Resources;

namespace ManticoreSearch.Api.Exceptions
{
    public class InsertException : Exception
    {
        public InsertException() : base() { }
        public InsertException(string message) : base(string.Format(ProviderError.InsertError, message)) { }
        public InsertException(string message, Exception innerException) : base(message, innerException) { }
    }
}
