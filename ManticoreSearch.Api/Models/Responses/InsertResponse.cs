using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class InsertResponse
    {
        [JsonProperty("_index")]
        public string Index { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("created")]
        public bool Created { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
