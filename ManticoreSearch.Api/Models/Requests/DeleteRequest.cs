using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to delete documents from ManticoreSearch.
    /// This class encapsulates the details required to perform a delete operation.
    /// </summary>
    public class DeleteRequest
    {
        /// <summary>
        /// Gets or sets the index from which documents will be deleted.
        /// This field is required and must not be null or empty.
        /// </summary>
        [JsonProperty("index")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "IndexRequired")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the optional cluster identifier for the delete operation.
        /// If not provided, the operation will target the default cluster.
        /// </summary>
        [JsonProperty("cluster", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cluster { get; set; } = null;

        /// <summary>
        /// Gets or sets the unique identifier of the document to be deleted.
        /// This field defaults to 0, which may indicate that it is not set.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; } = null;

        /// <summary>
        /// Gets or sets an optional query that defines the criteria for deleting documents.
        /// If provided, it overrides the Id field for more complex deletion scenarios.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public object? Query { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest"/> class.
        /// </summary>
        public DeleteRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest"/> class
        /// with specified parameters for the delete operation.
        /// </summary>
        /// <param name="index">The index from which documents will be deleted.</param>
        /// <param name="cluster">The optional cluster identifier.</param>
        /// <param name="id">The unique identifier of the document to be deleted.</param>
        /// <param name="query">An optional query defining the deletion criteria.</param>
        public DeleteRequest(string index, string? cluster = null, long id = 0, object? query = null)
        {
            Index = index;
            Cluster = cluster;
            Id = id;
            Query = query;
        }
    }
}
