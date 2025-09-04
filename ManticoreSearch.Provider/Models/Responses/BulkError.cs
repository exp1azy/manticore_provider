using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the error details returned from a bulk operation in ManticoreSearch.
    /// </summary>
    public class BulkError
    {
        /// <summary>
        /// The total number of errors encountered during the bulk operation.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// An error message detailing the nature of the error encountered.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// (Optional) A warning message providing additional context about the error, if applicable.
        /// </summary>
        [JsonProperty("warning")]
        public string Warning { get; set; }
    }
}
