using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request for percolation query management operations in Manticore Search.
    /// </summary>
    public class PercolationActionRequest
    {
        /// <summary>
        /// Gets or sets the search query to be stored for percolation.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        /// <summary>
        /// Gets or sets additional filter conditions for the percolation query.
        /// </summary>
        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public string? Filters { get; set; }

        /// <summary>
        /// Gets or sets the list of tags associated with the percolation query.
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercolationActionRequest"/> class.
        /// </summary>
        public PercolationActionRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercolationActionRequest"/> class.
        /// </summary>
        /// <param name="query">The search query to be stored for percolation.</param>
        /// <param name="filters">The additional filter conditions for the percolation query.</param>
        /// <param name="tags">The list of tags associated with the percolation query.</param>
        public PercolationActionRequest(Query? query = null, string filters = "", List<string>? tags = null)
        {
            Query = query;
            Filters = filters;
            Tags = tags;
        }
    }
}
