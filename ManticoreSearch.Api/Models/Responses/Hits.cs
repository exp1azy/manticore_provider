using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class Hits
    {
        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("_score")]
        public int Score { get; set; }

        [JsonProperty("_source")]
        public Dictionary<string, object> Source { get; set; }
    }
}