using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to modify a document in an index.
    /// This class encapsulates the necessary information to identify the 
    /// document and provide the new data to update it.
    /// </summary>
    public class ModificationRequest
    {
        /// <summary>
        /// Gets or sets the name of the index where the document is stored.
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the document to be modified.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the cluster name where the index resides.
        /// This property is optional and can be null if not applicable.
        /// </summary>
        [JsonProperty("cluster", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cluster { get; set; }

        /// <summary>
        /// Gets or sets the dictionary representing the document's fields 
        /// and their new values for the modification.
        /// </summary>
        [JsonProperty("doc")]
        public Dictionary<string, object> Document { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModificationRequest"/> class.
        /// </summary>
        public ModificationRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModificationRequest"/> class 
        /// with the specified index, document data, and optional id and cluster.
        /// </summary>
        /// <param name="index">The name of the index.</param>
        /// <param name="document">The new document data.</param>
        /// <param name="id">The unique identifier of the document. Default is 0.</param>
        /// <param name="cluster">The optional cluster name.</param>
        public ModificationRequest(string index, Dictionary<string, object> document, long id = 0, string? cluster = null)
        {
            Index = index;
            Id = id;
            Cluster = cluster;
            Document = document;
        }
    }
}
