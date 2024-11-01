using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class AutocompleteResponse : BaseResponse
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("warning")]
        public string Warning { get; set; }

        [JsonProperty("columns")]
        public List<object> Columns { get; set; }

        [JsonProperty("data")]
        public List<object> Data { get; set; }
    }
}
