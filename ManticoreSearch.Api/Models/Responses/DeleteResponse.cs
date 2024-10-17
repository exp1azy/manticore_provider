using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class DeleteResponse
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("deleted")]
        public int Deleted { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
