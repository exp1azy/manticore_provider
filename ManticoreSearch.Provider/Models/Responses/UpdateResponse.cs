using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the successful response from an update operation.
    /// Contains information about the table that was updated, the identifier of the updated record, 
    /// and the result of the operation.
    /// </summary>
    public class UpdateSuccess
    {
        /// <summary>
        /// Gets or sets the name of the table where the update occurred.
        /// This property helps identify which database table was affected by the operation.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the record that was updated.
        /// This property is essential for referencing the specific record in the database.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the result of the update operation.
        /// This property typically contains a message indicating the outcome of the operation, 
        /// such as "updated" or "no changes made".
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
