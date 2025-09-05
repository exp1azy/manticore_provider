﻿using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to percolate a document through predefined queries.
    /// This class holds the percolation query that will be used to match documents.
    /// </summary>
    public class PercolateRequest<TDocument>
    {
        /// <summary>
        /// Gets or sets the percolate request query containing the document to match.
        /// </summary>
        [JsonProperty("query")]
        public PercolateRequestQuery<TDocument> Query { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercolateRequest{TDocument}"/> class.
        /// </summary>
        public PercolateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercolateRequest{TDocument}"/> class 
        /// with the specified percolate request query.
        /// </summary>
        /// <param name="query">The percolate request query.</param>
        public PercolateRequest(PercolateRequestQuery<TDocument> query)
        {
            Query = query;
        }
    }

    /// <summary>
    /// Represents the percolate request query, which contains the document to match against.
    /// </summary>
    public class PercolateRequestQuery<TDocument>
    {
        /// <summary>
        /// Gets or sets the percolate document that contains the document to be checked against queries.
        /// </summary>
        [JsonProperty("percolate")]
        public PercolateDocument<TDocument> Percolate { get; set; }
    }

    /// <summary>
    /// Represents the document or documents to be matched against predefined queries.
    /// This class allows for matching either a single document or multiple documents.
    /// </summary>
    public class PercolateDocument<TDocument>
    {
        /// <summary>
        /// Gets or sets the single document to be percolated.
        /// </summary>
        [JsonProperty("document", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TDocument? Document { get; set; }

        /// <summary>
        /// Gets or sets a list of documents to be percolated.
        /// This property can be used when matching multiple documents at once.
        /// </summary>
        [JsonProperty("documents", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<TDocument>? Documents { get; set; }
    }
}
