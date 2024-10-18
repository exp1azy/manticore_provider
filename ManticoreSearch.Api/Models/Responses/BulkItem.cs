using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents a single item in the bulk response from the ManticoreSearch engine.
    /// This class encapsulates the result of an individual operation within a bulk request.
    /// </summary>
    public class BulkItem
    {
        /// <summary>
        /// Gets or sets the result of the bulk operation for this item.
        /// This property contains detailed information about the outcome of the operation,
        /// including the index name, document ID, status, and the result of the operation (e.g., created, updated, deleted).
        /// </summary>
        [JsonProperty("bulk")]
        public BulkResult Bulk { get; set; }
    }
}
