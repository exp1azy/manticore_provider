using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the response from a percolate operation in Manticoresearch.
    /// Inherits from <see cref="ManticoreResponse{DeleteSuccess, ErrorResponse}"/> to provide details about the operation's success or error.
    /// </summary>
    public class PercolateResponse : ManticoreResponse<PercolateSuccess, ErrorMessage>
    {
        /// <summary>
        /// Gets or sets the response containing search success details, if applicable.
        /// This property will be populated when a search operation is performed alongside percolation.
        /// </summary>
        public SearchSuccess? ResponseIfSearch { get; set; }
    }

    /// <summary>
    /// Represents the successful response of a percolate operation.
    /// This class contains details about the percolation result, including the associated table and record information.
    /// </summary>
    public class PercolateSuccess
    {
        /// <summary>
        /// Gets or sets the name of the table associated with the percolation operation.
        /// This property indicates the specific table where the percolate rule is defined.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the type of the percolation result.
        /// This property defines the type or category of the result obtained from the percolation process.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the percolate rule that matched.
        /// This property identifies the specific rule that triggered a match during the percolation operation.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the result message of the percolation operation.
        /// This property provides details about the outcome of the percolation process, such as success or match information.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
