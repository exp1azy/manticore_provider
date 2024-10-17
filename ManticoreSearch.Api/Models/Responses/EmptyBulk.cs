using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class EmptyBulk
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("warning")]
        public string Warning { get; set; }
    }
}