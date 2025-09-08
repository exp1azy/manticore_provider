using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to delete multiple records in a bulk operation.
    /// </summary>
    public class BulkDeleteRequest
    {
        /// <summary>
        /// Gets or sets the details of the delete operation.
        /// </summary>
        [JsonProperty("delete")]
        public DeleteRequest Delete { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkDeleteRequest"/> class.
        /// </summary>
        public BulkDeleteRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkDeleteRequest"/> class.
        /// </summary>
        /// <param name="delete">The details of the delete operation.</param>
        public BulkDeleteRequest(DeleteRequest delete)
        {
            Delete = delete;
        }
    }
}
