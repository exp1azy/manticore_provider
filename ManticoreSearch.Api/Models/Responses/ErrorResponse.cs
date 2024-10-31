using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public ErrorMessage Error { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }

    public class ErrorMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("table")]
        public string Table { get; set; }
    }
}
