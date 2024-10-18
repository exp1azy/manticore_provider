using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the result of an individual operation in a bulk request to the ManticoreSearch engine.
    /// This class encapsulates the details about the status of the operation, including whether it was created, updated, or deleted.
    /// </summary>
    public class BulkResult
    {
        /// <summary>
        /// Gets or sets the index name where the operation was performed.
        /// This property indicates the target index within ManticoreSearch that the operation affects.
        /// </summary>
        [JsonProperty("_index")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the document affected by the operation.
        /// This property is essential for referencing the specific document that was created, updated, or deleted.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the count of documents that were created as a result of this operation.
        /// This property provides insight into how many new documents were added to the index during the bulk operation.
        /// </summary>
        [JsonProperty("created")]
        public int Created { get; set; }

        /// <summary>
        /// Gets or sets the count of documents that were deleted as a result of this operation.
        /// This property indicates how many documents were removed from the index during the bulk operation.
        /// </summary>
        [JsonProperty("deleted")]
        public int Deleted { get; set; }

        /// <summary>
        /// Gets or sets the count of documents that were updated as a result of this operation.
        /// This property reflects how many existing documents were modified during the bulk operation.
        /// </summary>
        [JsonProperty("updated")]
        public int Updated { get; set; }

        /// <summary>
        /// Gets or sets the result of the operation, indicating success or failure.
        /// This property is a descriptive string that conveys the outcome of the individual operation,
        /// such as "created", "updated", "deleted", or "not_found".
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code returned for the operation.
        /// This property allows consumers of the response to understand the status of the operation,
        /// aiding in error handling and decision-making based on the outcome.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
