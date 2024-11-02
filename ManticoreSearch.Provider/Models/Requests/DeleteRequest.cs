using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to delete a record or records from a specific index.
    /// This class provides the necessary parameters to identify what should be deleted.
    /// </summary>
    public class DeleteRequest
    {
        /// <summary>
        /// Gets or sets the name of the index from which the record should be deleted.
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the name of the cluster where the index is located.
        /// This parameter is optional and can be null if not specified.
        /// </summary>
        [JsonProperty("cluster", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cluster { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the record to be deleted.
        /// This parameter is optional; it can be null if a query is provided.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the query used to identify the records to be deleted.
        /// This parameter is optional; if provided, the <see cref="Id"/> should not be used.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest"/> class.
        /// </summary>
        public DeleteRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest"/> class with 
        /// specified parameters to configure the delete operation.
        /// </summary>
        /// <param name="index">The name of the index from which to delete the record.</param>
        /// <param name="cluster">The name of the cluster (optional).</param>
        /// <param name="id">The unique identifier of the record to delete (optional).</param>
        /// <param name="query">The query to identify records to delete (optional).</param>
        public DeleteRequest(string index, string? cluster = null, long id = 0, Query? query = null)
        {
            Index = index;
            Cluster = cluster;
            Id = id;
            Query = query;
        }
    }
}
