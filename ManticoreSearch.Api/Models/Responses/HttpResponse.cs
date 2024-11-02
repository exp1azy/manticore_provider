namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents an HTTP response from a web request.
    /// This class encapsulates the response content and the status of the request.
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// Gets or sets the response content as a string.
        /// This property contains the data returned by the server.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the HTTP response status code indicates success.
        /// True if the status code is in the range of 200-299; otherwise, false.
        /// </summary>
        public bool IsSuccessStatusCode { get; set; }
    }
}
