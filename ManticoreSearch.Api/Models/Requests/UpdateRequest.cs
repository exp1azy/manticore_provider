using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    public class UpdateRequest
    {
        [JsonProperty("index")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "IndexRequired")]
        public string Index { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; } = 0;

        [JsonProperty("doc")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "DocumentRequired")]
        public Dictionary<string, object> Document { get; set; }

        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Query { get; set; } = null;

        public UpdateRequest() { }

        public UpdateRequest(string index, Dictionary<string, object> document, long id = 0, Dictionary<string, object>? query = null)
        {
            Index = index;
            Id = id;
            Document = document;
            Query = query;
        }
    }
}
