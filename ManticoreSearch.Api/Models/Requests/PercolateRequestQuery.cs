using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class PercolateRequestQuery
    {
        [JsonProperty("percolate")]
        public PercolateDocument Percolate { get; set; }
    }

    public class PercolateDocument
    {
        [JsonProperty("document", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object? Document { get; set; }

        [JsonProperty("documents", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<object>? Documents { get; set; }
    }
}
