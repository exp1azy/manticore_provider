using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to insert multiple records in a bulk operation.
    /// </summary>
    /// <typeparam name="TDocument">The type of document to be inserted into the table. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
    public class BulkInsertRequest<TDocument> where TDocument : ManticoreDocument
    {
        /// <summary>
        /// Gets or sets the details of the insert operation.
        /// </summary>
        [JsonProperty("insert")]
        public ModificationRequest<TDocument> Insert { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkInsertRequest{TDocument}"/> class.
        /// </summary>
        public BulkInsertRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkInsertRequest{TDocument}"/> class.
        /// </summary>
        /// <param name="insert">The details of the insert operation.</param>
        public BulkInsertRequest(ModificationRequest<TDocument> insert)
        {
            Insert = insert;
        }
    }
}
