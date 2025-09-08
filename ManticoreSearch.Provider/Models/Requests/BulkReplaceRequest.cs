using Newtonsoft.Json;
namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to replace multiple records in a bulk operation.
    /// </summary>
    /// <typeparam name="TDocument">The type of document to be replaced in the table.</typeparam>
    public class BulkReplaceRequest<TDocument>
    {
        /// <summary>
        /// Gets or sets the details of the replace operation.
        /// </summary>
        [JsonProperty("replace")]
        public ModificationRequest<TDocument> Replace { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkReplaceRequest{TDocument}"/> class.
        /// </summary>
        public BulkReplaceRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkReplaceRequest{TDocument}"/> class.
        /// </summary>
        /// <param name="replace">The details of the replace operation.</param>
        public BulkReplaceRequest(ModificationRequest<TDocument> replace)
        {
            Replace = replace;
        }
    }
}
