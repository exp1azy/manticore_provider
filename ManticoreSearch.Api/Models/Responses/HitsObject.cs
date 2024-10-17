using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class HitsObject
    {
        [JsonProperty("hits")]
        public IEnumerable<Hits> Hits { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("total_relation")]
        public string TotalRelation { get; set; }
    }
}
