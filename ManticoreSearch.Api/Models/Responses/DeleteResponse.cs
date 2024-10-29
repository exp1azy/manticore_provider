using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the response returned from the ManticoreSearch engine after a delete operation.
    /// This class contains information about the status of the delete operation,
    /// including whether the document was found and removed successfully.
    /// </summary>
    public class DeleteResponse
    {
        /// <summary>
        /// Gets or sets the name of the index from which the document was deleted.
        /// This property indicates the specific index involved in the delete operation,
        /// which is useful for tracking and auditing actions performed on various indexes.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the document was found and deleted.
        /// This property returns true if the document with the specified ID was located
        /// in the index and successfully removed; otherwise, it returns false.
        /// </summary>
        [JsonProperty("found")]
        public bool Found { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the document that was deleted.
        /// This ID is essential for identifying which document was affected by the delete operation,
        /// allowing developers to confirm actions performed on specific records.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the result of the delete operation.
        /// This property provides a message indicating the outcome of the operation,
        /// which can be useful for logging and debugging purposes.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
