using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class UpdateResponse
    {
        [JsonProperty("_index")]
        public string Index { get; set; }

        [JsonProperty("updated")]
        public int Updated { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
