using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the response received from the ManticoreSearch engine 
    /// after attempting to update a document.
    /// This class encapsulates the information about the index, the status 
    /// of the update operation, and the document's unique identifier.
    /// </summary>
    public class UpdateResponse
    {
        /// <summary>
        /// Gets or sets the name of the index where the document was updated.
        /// This property indicates the index associated with the updated document.
        /// </summary>
        [JsonProperty("_index")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the number of documents that were successfully updated.
        /// This property provides insight into the outcome of the update operation.
        /// </summary>
        [JsonProperty("updated")]
        public int Updated { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the updated document.
        /// This property allows for tracking the specific document that was modified.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the result of the update operation.
        /// This property typically indicates the success or failure of the update,
        /// and may contain values such as "created", "updated", or "not_found".
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
