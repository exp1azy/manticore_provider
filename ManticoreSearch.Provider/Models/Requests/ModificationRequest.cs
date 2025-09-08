using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request for document modification operations.
    /// This class is used for both insert and replace operations.
    /// </summary>
    /// <typeparam name="TDocument">The type of document to be inserted or replaced in the table.</typeparam>
    public class ModificationRequest<TDocument>
    {
        /// <summary>
        /// Gets or sets the name of the table where the document should be inserted or replaced.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the ID of the document to be inserted or replaced.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the cluster where the table resides.
        /// </summary>
        [JsonProperty("cluster", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cluster { get; set; }

        /// <summary>
        /// Gets or sets the document to be inserted or replaced.
        /// </summary>
        [JsonProperty("doc")]
        public TDocument Document { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModificationRequest{TDocument}"/> class.
        /// </summary>
        public ModificationRequest()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModificationRequest{TDocument}"/> class
        /// with the specified modification parameters.
        /// </summary>
        /// <param name="table">The name of the target table (index) for the operation.</param>
        /// <param name="document">The document data to be inserted or used as replacement.</param>
        /// <param name="id">The document identifier. Use 0 for auto-generated ID in insert operations.</param>
        /// <param name="cluster">The name of the cluster containing the table.</param>
        public ModificationRequest(string table, TDocument document, long id = 0, string? cluster = null)
        {
            Table = table;
            Id = id;
            Cluster = cluster;
            Document = document;
        }
    }
}