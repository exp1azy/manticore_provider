using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to update multiple records in a bulk operation.
    /// This class encapsulates the details of the update operation.
    /// </summary>
    public class BulkUpdateRequest
    {
        /// <summary>
        /// Gets or sets the details of the update operation, including the 
        /// records to be updated and their associated properties.
        /// </summary>
        [JsonProperty("update")]
        public UpdateRequest Update { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkUpdateRequest"/> class.
        /// </summary>
        public BulkUpdateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkUpdateRequest"/> class 
        /// with the specified update request details.
        /// </summary>
        /// <param name="update">The details of the update operation.</param>
        public BulkUpdateRequest(UpdateRequest update)
        {
            Update = update;
        }
    }
}
