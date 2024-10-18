using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to insert a document into an index in ManticoreSearch.
    /// This class encapsulates the details required to perform an insert operation.
    /// </summary>
    public class InsertRequest
    {
        /// <summary>
        /// Gets or sets the name of the index where the document will be inserted.
        /// This field is required and must contain a valid index name.
        /// </summary>
        [JsonProperty("index")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "IndexRequired")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the document.
        /// If not provided, a default value of 0 will be used.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the cluster name where the index resides.
        /// This property is optional and will be ignored if not provided.
        /// </summary>
        [JsonProperty("cluster", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cluster { get; set; } = null;

        /// <summary>
        /// Gets or sets the document to be inserted.
        /// This field is required and must contain the document data as key-value pairs.
        /// </summary>
        [JsonProperty("doc")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "DocumentRequired")]
        public Dictionary<string, object> Document { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertRequest"/> class.
        /// </summary>
        public InsertRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertRequest"/> class
        /// with the specified index, document, optional id, and optional cluster.
        /// </summary>
        /// <param name="index">The name of the index where the document will be inserted.</param>
        /// <param name="document">The document data to be inserted.</param>
        /// <param name="id">The unique identifier for the document (default is 0).</param>
        /// <param name="cluster">The optional cluster name where the index resides.</param>
        public InsertRequest(string index, Dictionary<string, object> document, int id = 0, string? cluster = null)
        {
            Index = index;
            Id = id;
            Cluster = cluster;
            Document = document;
        }
    }
}
