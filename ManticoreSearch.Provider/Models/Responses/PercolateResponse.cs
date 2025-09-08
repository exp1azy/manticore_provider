using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the response from a percolate operation in Manticore Search server.
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
    /// </summary>
    public class PercolateSuccess
    {
        /// <summary>
        /// Gets or sets the name of the table associated with the percolation operation.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the type of the percolation result.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the percolate rule that matched.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the result message of the percolation operation.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
