using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to index a percolation query. 
    /// This class allows the definition of the query, filters, and tags to be associated with the percolation.
    /// </summary>
    public class IndexPercolateRequest
    {
        /// <summary>
        /// Gets or sets the query that will be used for percolation. 
        /// This parameter is optional and can be null if not specified.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        /// <summary>
        /// Gets or sets the filters to be applied to the percolation query.
        /// This parameter is optional and can be null or an empty string if not specified.
        /// </summary>
        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public string? Filters { get; set; }

        /// <summary>
        /// Gets or sets the list of tags associated with the percolation query. 
        /// This parameter is optional and can be null if not specified.
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexPercolateRequest"/> class.
        /// </summary>
        public IndexPercolateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexPercolateRequest"/> class with 
        /// specified parameters to configure the percolate indexing operation.
        /// </summary>
        /// <param name="query">The query to be indexed for percolation (optional).</param>
        /// <param name="filters">The filters to apply to the query (optional).</param>
        /// <param name="tags">The list of tags associated with the query (optional).</param>
        public IndexPercolateRequest(Query? query = null, string filters = "", List<string>? tags = null)
        {
            Query = query;
            Filters = filters;
            Tags = tags;
        }
    }
}
