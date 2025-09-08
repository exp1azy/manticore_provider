using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to delete documents from a table.
    /// </summary>
    public class DeleteRequest
    {
        /// <summary>
        /// Gets or sets the name of the table to delete documents from.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the name of the cluster where the table resides.
        /// </summary>
        [JsonProperty("cluster", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cluster { get; set; }

        /// <summary>
        /// Gets or sets the specific document ID to be deleted.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the query criteria for deleting multiple documents.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest"/> class.
        /// </summary>
        public DeleteRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest"/> class
        /// with the specified deletion parameters.
        /// </summary>
        /// <param name="table">The name of the target table for deletion.</param>
        /// <param name="cluster">The name of the cluster containing the table.</param>
        /// <param name="id">The specific document ID to delete. Use 0 or null for query-based deletion.</param>
        /// <param name="query">The query criteria for deleting multiple documents.</param>
        public DeleteRequest(string table, string? cluster = null, long id = 0, Query? query = null)
        {
            Table = table;
            Cluster = cluster;
            Id = id;
            Query = query;
        }
    }
}
