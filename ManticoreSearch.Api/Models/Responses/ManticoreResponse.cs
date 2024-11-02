namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents a generic response structure from Manticore Search API, encapsulating both successful responses and errors.
    /// </summary>
    /// <typeparam name="TResponse">The type of the successful response data.</typeparam>
    /// <typeparam name="TError">The type of the error details in case of a failed response.</typeparam>
    public class ManticoreResponse<TResponse, TError>
    {
        /// <summary>
        /// The successful response data returned from the Manticore Search API.
        /// </summary>
        public TResponse? Response { get; set; }

        /// <summary>
        /// The error details returned from the Manticore Search API if the request was unsuccessful.
        /// </summary>
        public TError? Error { get; set; }

        /// <summary>
        /// The timestamp indicating when the response was generated, in UTC format.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indicates whether the response was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// The raw response string received from the Manticore Search API, useful for logging and debugging purposes.
        /// </summary>
        public string RawResponse { get; set; }
    }
}
