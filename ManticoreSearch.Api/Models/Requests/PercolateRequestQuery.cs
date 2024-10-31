using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class PercolateRequestQuery
    {
        [JsonProperty("percolate")]
        public object Percolate { get; set; }
    }
}
