using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a bulk delete request to ManticoreSearch.
    /// This class encapsulates the details required to perform a bulk delete operation.
    /// </summary>
    public class BulkDeleteRequest
    {
        /// <summary>
        /// Specifies the delete operation for the bulk request.
        /// This field is required and contains the details of the delete request.
        /// </summary>
        [JsonProperty("delete")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "InsertRequestRequired")]
        public DeleteRequest Delete { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkDeleteRequest"/> class.
        /// </summary>
        public BulkDeleteRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkDeleteRequest"/> class
        /// with a specified <see cref="DeleteRequest"/>.
        /// </summary>
        /// <param name="delete">The delete request details for the bulk operation.</param>
        public BulkDeleteRequest(DeleteRequest delete)
        {
            Delete = delete;
        }
    }
}
