using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the response received from the ManticoreSearch engine 
    /// after attempting to insert a document into an index.
    /// This class encapsulates the details of the insertion operation, 
    /// including the index, document ID, creation status, result, and HTTP status code.
    /// </summary>
    public class InsertResponse
    {
        /// <summary>
        /// Gets or sets the name of the index where the document was inserted.
        /// This property identifies the specific index within ManticoreSearch 
        /// that contains the newly added document.
        /// </summary>
        [JsonProperty("_index")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier assigned to the inserted document.
        /// This property allows for the retrieval or manipulation of the document 
        /// in subsequent operations, such as updates or deletions.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the document was successfully created.
        /// This property provides a clear indication of the success or failure 
        /// of the insert operation, facilitating error handling and logic flow.
        /// </summary>
        [JsonProperty("created")]
        public bool Created { get; set; }

        /// <summary>
        /// Gets or sets the result of the insertion operation.
        /// This property contains a message or status indicating the outcome of the insert request, 
        /// such as "created" or "updated", providing context for the result.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code returned by the insert operation.
        /// This property allows consumers of the API to assess the response 
        /// and determine if the operation was successful or if an error occurred.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
