namespace ManticoreSearch.Api.Exceptions
{
    public class MappingException : Exception
    {
        public MappingException() { }
        public MappingException(string message) : base(message) { }
        public MappingException(string message, Exception innerException) : base(string.Format(message, innerException)) { }
    }
}
