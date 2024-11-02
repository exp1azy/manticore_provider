using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to insert multiple records in a bulk operation.
    /// This class encapsulates the details of the insert operation.
    /// </summary>
    public class BulkInsertRequest
    {
        /// <summary>
        /// Gets or sets the details of the insert operation, including the 
        /// records to be inserted and their associated properties.
        /// </summary>
        [JsonProperty("insert")]
        public ModificationRequest Insert { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkInsertRequest"/> class.
        /// </summary>
        public BulkInsertRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkInsertRequest"/> class 
        /// with the specified insert request details.
        /// </summary>
        /// <param name="insert">The details of the insert operation.</param>
        public BulkInsertRequest(ModificationRequest insert)
        {
            Insert = insert;
        }
    }
}
