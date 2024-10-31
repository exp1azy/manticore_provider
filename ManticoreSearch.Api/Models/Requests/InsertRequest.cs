using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class InsertRequest
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; } = 0;

        [JsonProperty("cluster", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cluster { get; set; }

        [JsonProperty("doc")]
        public Dictionary<string, object> Document { get; set; }

        public InsertRequest() { }

        public InsertRequest(string index, Dictionary<string, object> document, int id = 0, string? cluster = null)
        {
            Index = index;
            Id = id;
            Cluster = cluster;
            Document = document;
        }
    }
}
