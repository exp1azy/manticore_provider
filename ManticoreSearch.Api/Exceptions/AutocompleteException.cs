namespace ManticoreSearch.Api.Exceptions
{
    public class AutocompleteException : Exception
    {
        public AutocompleteException() : base() { }
        public AutocompleteException(string message) : base(message) { }
        public AutocompleteException(string message, Exception innerException) : base(string.Format(message, innerException.Message)) { }
    }
}
