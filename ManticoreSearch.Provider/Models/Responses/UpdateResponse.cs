using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the successful response from an update operation.
    /// </summary>
    public class UpdateSuccess
    {
        /// <summary>
        /// Gets or sets the name of the table where the update occurred.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the record that was updated.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the result of the update operation.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
