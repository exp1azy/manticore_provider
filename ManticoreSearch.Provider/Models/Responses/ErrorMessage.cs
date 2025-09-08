using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents an error message returned by the Manticore Search.
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// The error message detailing the issue encountered during the operation.
        /// </summary>
        [JsonProperty("error")]
        public string Message { get; set; }
    }
}
