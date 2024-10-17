using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class BulkResult
    {
        [JsonProperty("_index")]
        public string Index { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("deleted")]
        public int Deleted { get; set; }

        [JsonProperty("updated")]
        public int Updated { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
