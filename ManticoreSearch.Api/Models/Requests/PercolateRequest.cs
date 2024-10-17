using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    public class PercolateRequest
    {
        [JsonProperty("query")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "QueryRequired")]
        public PercolateRequestQuery Query { get; set; }

        public PercolateRequest() { }

        public PercolateRequest(PercolateRequestQuery query)
        {
            Query = query;
        }
    }
}
