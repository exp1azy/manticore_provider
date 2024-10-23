using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents an error response returned by ManticoreSearch or another service.
    /// This class encapsulates the error details received from the API.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the error message.
        /// This property contains the error description returned by the API.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
