using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the error response returned by the ManticoreSearch API.
    /// This class contains details about the error encountered during an operation.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Contains detailed information about the error, including its type and reason.
        /// </summary>
        [JsonProperty("error")]
        public ErrorDetails Error { get; set; }

        /// <summary>
        /// The HTTP status code returned with the error response.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
    }

    /// <summary>
    /// Contains detailed information about an error returned by the ManticoreSearch API.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// The type of the error, indicating the general category of the issue.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// A descriptive reason for the error, providing context for the issue encountered.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// The name of the table associated with the error, if applicable.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }
    }
}
