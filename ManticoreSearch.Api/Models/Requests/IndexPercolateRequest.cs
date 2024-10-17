using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    public class IndexPercolateRequest
    {
        [JsonProperty("query")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "QueryRequired")]
        public object Query { get; set; }

        public IndexPercolateRequest() { }

        public IndexPercolateRequest(object query)
        {
            Query = query;
        }
    }
}
