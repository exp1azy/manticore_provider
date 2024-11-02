using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents an error message returned by the ManticoreSearch API.
    /// This class encapsulates the error information provided in the response.
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// The error message detailing the issue encountered during the API operation.
        /// </summary>
        [JsonProperty("error")]
        public string Message { get; set; }
    }
}
