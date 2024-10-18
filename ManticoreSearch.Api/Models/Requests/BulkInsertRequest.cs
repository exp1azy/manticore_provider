using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a bulk insert request to ManticoreSearch.
    /// This class encapsulates the details required to perform a bulk insert operation.
    /// </summary>
    public class BulkInsertRequest
    {
        /// <summary>
        /// Specifies the insert operation for the bulk request.
        /// This field is required and contains the details of the insert request.
        /// </summary>
        [JsonProperty("insert")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "InsertRequestRequired")]
        public InsertRequest Insert { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkInsertRequest"/> class.
        /// </summary>
        public BulkInsertRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkInsertRequest"/> class
        /// with a specified <see cref="InsertRequest"/>.
        /// </summary>
        /// <param name="insert">The insert request details for the bulk operation.</param>
        public BulkInsertRequest(InsertRequest insert)
        {
            Insert = insert;
        }
    }
}
