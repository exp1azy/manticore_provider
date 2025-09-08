using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the successful response of a mapping operation in Manticore Search server.
    /// </summary>
    public class MappingSuccess
    {
        /// <summary>
        /// Gets or sets the total number of successful mappings.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets any error message that occurred during the mapping process.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets any warning message that may be relevant to the mapping operation.
        /// </summary>
        [JsonProperty("warning")]
        public string Warning { get; set; }
    }
}
