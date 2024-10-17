using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    public class PercolateRequestQuery
    {
        [JsonProperty("percolate")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "PercolateRequired")]
        public object Percolate { get; set; }
    }
}
