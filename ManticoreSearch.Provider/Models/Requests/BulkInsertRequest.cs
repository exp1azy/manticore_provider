using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to insert multiple records in a bulk operation.
    /// This class encapsulates the details of the insert operation.
    /// </summary>
    public class BulkInsertRequest<TDocument>
    {
        /// <summary>
        /// Gets or sets the details of the insert operation, including the 
        /// records to be inserted and their associated properties.
        /// </summary>
        [JsonProperty("insert")]
        public ModificationRequest<TDocument> Insert { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkInsertRequest{TDocument}"/> class.
        /// </summary>
        public BulkInsertRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkInsertRequest{TDocument}"/> class 
        /// with the specified insert request details.
        /// </summary>
        /// <param name="insert">The details of the insert operation.</param>
        public BulkInsertRequest(ModificationRequest<TDocument> insert)
        {
            Insert = insert;
        }
    }
}
