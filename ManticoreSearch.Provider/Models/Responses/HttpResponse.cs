namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents an HTTP response from a web request.
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// Gets or sets the response content as a string.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the HTTP response status code indicates success.
        /// True if the status code is in the range of 200-299; otherwise, false.
        /// </summary>
        public bool IsSuccessStatusCode { get; set; }
    }
}
