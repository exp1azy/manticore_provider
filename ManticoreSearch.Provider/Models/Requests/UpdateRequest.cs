using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to update a document in a table.
    /// </summary>
    /// <typeparam name="TDocument">The type of document to be updated in the table. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
    public class UpdateRequest<TDocument> where TDocument : ManticoreDocument
    {
        /// <summary>
        /// The name of the table to update documents in.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// The ID of the document to update.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        /// <summary>
        /// The document data to update.
        /// </summary>
        [JsonProperty("doc")]
        public TDocument Document { get; set; }

        /// <summary>
        /// A query to identify documents to be updated if the ID is not provided.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        /// <summary>
        /// The name of the cluster to use for the request.
        /// </summary>
        [JsonProperty("cluster", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cluster { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRequest{TDocument}"/> class.
        /// </summary>
        public UpdateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRequest{TDocument}"/> class.
        /// </summary>
        /// <param name="table">The name of the table to update documents in.</param>
        /// <param name="document">The document data to update.</param>
        /// <param name="id">The ID of the document to update.</param>
        /// <param name="query">The query to identify documents to be updated if the ID is not provided.</param>
        /// <param name="cluster">The name of the cluster to use for the request.</param>
        public UpdateRequest(string table, TDocument document, long id = 0, Query? query = null, string? cluster = null)
        {
            Table = table;
            Id = id;
            Document = document;
            Query = query;
            Cluster = cluster;
        }
    }
}
