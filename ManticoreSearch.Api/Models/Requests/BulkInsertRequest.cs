using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    public class BulkInsertRequest
    {
        [JsonProperty("insert")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "InsertRequestRequired")]
        public InsertRequest Insert { get; set; }

        public BulkInsertRequest() { }

        public BulkInsertRequest(InsertRequest insert)
        {
            Insert = insert;
        }
    }
}
