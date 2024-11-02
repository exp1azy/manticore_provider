using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the successful response of a modification operation in Manticoresearch.
    /// This class contains details about the modified record, including its status and result.
    /// </summary>
    public class ModificationSuccess
    {
        /// <summary>
        /// Gets or sets the name of the table where the modification occurred.
        /// This property indicates the specific table associated with the modified record.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the modified record.
        /// This property is used to identify the specific entry that was modified.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the record was created during the modification process.
        /// This property can be used to determine if a new record was added as part of the operation.
        /// </summary>
        [JsonProperty("created")]
        public bool Created { get; set; }

        /// <summary>
        /// Gets or sets the result message of the modification operation.
        /// This property provides details about the outcome of the operation, such as success or failure messages.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the status code of the modification operation.
        /// This property indicates the HTTP status or application-specific status related to the operation's outcome.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
