using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the successful response of a mapping operation in Manticoresearch.
    /// This class contains information about the total number of mappings and any potential errors or warnings.
    /// </summary>
    public class MappingSuccess
    {
        /// <summary>
        /// Gets or sets the total number of successful mappings.
        /// This property indicates how many mappings were processed successfully.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets any error message that occurred during the mapping process.
        /// This property may contain detailed information about the error, if applicable.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets any warning message that may be relevant to the mapping operation.
        /// This property provides additional information about potential issues without halting the operation.
        /// </summary>
        [JsonProperty("warning")]
        public string Warning { get; set; }
    }
}
