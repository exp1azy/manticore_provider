using Newtonsoft.Json;
namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to replace multiple records in a bulk operation.
    /// This class encapsulates the details of the replace operation.
    /// </summary>
    public class BulkReplaceRequest
    {
        /// <summary>
        /// Gets or sets the details of the replace operation, including the 
        /// records to be replaced and their associated properties.
        /// </summary>
        [JsonProperty("replace")]
        public ModificationRequest Replace { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkReplaceRequest"/> class.
        /// </summary>
        public BulkReplaceRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkReplaceRequest"/> class 
        /// with the specified replace request details.
        /// </summary>
        /// <param name="replace">The details of the replace operation.</param>
        public BulkReplaceRequest(ModificationRequest replace)
        {
            Replace = replace;
        }
    }
}
