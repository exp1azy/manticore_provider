using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace ManticoreSearch.Api.Models.Requests
{
    public class BulkReplaceRequest
    {
        [JsonProperty("replace")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "InsertRequestRequired")]
        public InsertRequest Replace { get; set; }

        public BulkReplaceRequest() { }

        public BulkReplaceRequest(InsertRequest replace)
        {
            Replace = replace;
        }
    }
}
