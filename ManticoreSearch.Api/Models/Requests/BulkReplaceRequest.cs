using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a bulk replace request to ManticoreSearch.
    /// This class encapsulates the details required to perform a bulk replace operation.
    /// </summary>
    public class BulkReplaceRequest
    {
        /// <summary>
        /// Specifies the replace operation for the bulk request.
        /// This field is required and contains the details of the replace request.
        /// </summary>
        [JsonProperty("replace")]
        public InsertRequest Replace { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkReplaceRequest"/> class.
        /// </summary>
        public BulkReplaceRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulkReplaceRequest"/> class
        /// with a specified <see cref="InsertRequest"/> for the replace operation.
        /// </summary>
        /// <param name="replace">The replace request details for the bulk operation.</param>
        public BulkReplaceRequest(InsertRequest replace)
        {
            Replace = replace;
        }
    }
}
