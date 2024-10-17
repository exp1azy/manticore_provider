using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class SearchResponse
    {
        [JsonProperty("took")]
        public int Took { get; set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        [JsonProperty("hits")]
        public HitsObject Hits { get; set; }
    }
}
