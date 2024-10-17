using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    public class DeleteRequest
    {
        [JsonProperty("index")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "IndexRequired")]
        public string Index { get; set; }

        [JsonProperty("cluster", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cluster { get; set; } = null;

        [JsonProperty("id")]
        public long Id { get; set; } = 0;

        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public object? Query { get; set; } = null;

        public DeleteRequest() { }

        public DeleteRequest(string index, string? cluster = null, long id = 0, object? query = null)
        {
            Index = index;
            Cluster = cluster;
            Id = id;
            Query = query;
        }
    }
}
