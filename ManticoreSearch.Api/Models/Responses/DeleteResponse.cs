using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class DeleteResponse
    {
        [JsonProperty("_index")]
        public string Index { get; set; }

        [JsonProperty("found")]
        public bool Found { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
