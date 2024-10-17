using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    public class BulkDeleteRequest
    {
        [JsonProperty("delete")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "InsertRequestRequired")]
        public DeleteRequest Delete { get; set; }

        public BulkDeleteRequest() { }

        public BulkDeleteRequest(DeleteRequest delete)
        {
            Delete = delete;
        }
    }
}
