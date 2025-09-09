using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to update multiple records in a bulk operation.
    /// </summary>
    /// <typeparam name="TDocument">The type of document to be updated in the table. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
    public class BulkUpdateRequest<TDocument> where TDocument : ManticoreDocument
    {
        /// <summary>
        /// Gets or sets the details of the update operation.
        /// </summary>
        [JsonProperty("update")]
        public UpdateRequest<TDocument> Update { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkUpdateRequest{TDocument}"/> class.
        /// </summary>
        public BulkUpdateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkUpdateRequest{TDocument}"/> class.
        /// </summary>
        /// <param name="update">The details of the update operation.</param>
        public BulkUpdateRequest(UpdateRequest<TDocument> update)
        {
            Update = update;
        }
    }
}
