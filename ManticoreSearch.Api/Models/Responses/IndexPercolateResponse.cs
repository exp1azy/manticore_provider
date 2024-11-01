using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class IndexPercolateResponse : BaseResponse
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
