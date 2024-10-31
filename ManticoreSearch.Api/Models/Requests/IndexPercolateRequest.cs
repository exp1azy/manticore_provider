using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class IndexPercolateRequest
    {
        [JsonProperty("query")]
        public object Query { get; set; }

        [JsonProperty("filters")]
        public string Filters { get; set; } = string.Empty;

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tags { get; set; } = null;

        public IndexPercolateRequest() { }

        public IndexPercolateRequest(object query, string filters = "", List<string> tags = null)
        {
            Query = query;
            Filters = filters;
        }
    }
}
