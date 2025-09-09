using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a percolate request to match documents against stored queries in Manticore Search.
    /// </summary>
    /// <typeparam name="TDocument">The type of document to be percolated against stored queries. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
    public class PercolateRequest<TDocument> where TDocument : ManticoreDocument
    {
        /// <summary>
        /// Gets or sets the query configuration for the percolate operation.
        /// </summary>
        [JsonProperty("query")]
        public PercolateRequestQuery<TDocument> Query { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercolateRequest{TDocument}"/> class.
        /// </summary>
        public PercolateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercolateRequest{TDocument}"/> class.
        /// </summary>
        /// <param name="query">The query configuration for the percolate operation.</param>
        public PercolateRequest(PercolateRequestQuery<TDocument> query)
        {
            Query = query;
        }
    }

    /// <summary>
    /// Represents the query configuration for a percolate operation.
    /// Wraps the percolate document(s) in a query structure for Manticore Search.
    /// </summary>
    /// <typeparam name="TDocument">The type of document to be percolated.</typeparam>
    public class PercolateRequestQuery<TDocument>
    {
        /// <summary>
        /// Gets or sets the document(s) to be matched against stored percolation queries.
        /// </summary>
        [JsonProperty("percolate")]
        public PercolateDocument<TDocument> Percolate { get; set; }
    }

    /// <summary>
    /// Represents the document(s) to be used in a percolate operation.
    /// </summary>
    /// <typeparam name="TDocument">The type of document to be percolated.</typeparam>
    public class PercolateDocument<TDocument>
    {
        /// <summary>
        /// Gets or sets a single document to be percolated against stored queries.
        /// </summary>
        [JsonProperty("document", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TDocument? Document { get; set; }

        /// <summary>
        /// Gets or sets a list of documents to be percolated against stored queries.
        /// </summary>
        [JsonProperty("documents", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<TDocument>? Documents { get; set; }
    }
}
