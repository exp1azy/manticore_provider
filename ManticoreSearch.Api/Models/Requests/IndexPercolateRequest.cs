using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class IndexPercolateRequest
    {
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public string Filters { get; set; } = "";

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Tags { get; set; }

        public IndexPercolateRequest() { }

        public IndexPercolateRequest(Query? query = null, string filters = "", List<string>? tags = null)
        {
            Query = query;
            Filters = filters;
            Tags = tags;
        }
    }
}
