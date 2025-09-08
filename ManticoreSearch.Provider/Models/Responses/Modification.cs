using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the successful response of a modification operation in Manticore Search server.
    /// </summary>
    public class ModificationSuccess
    {
        /// <summary>
        /// Gets or sets the name of the table where the modification occurred.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the modified record.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the record was created during the modification process.
        /// </summary>
        [JsonProperty("created")]
        public bool Created { get; set; }

        /// <summary>
        /// Gets or sets the result message of the modification operation.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the status code of the modification operation.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
