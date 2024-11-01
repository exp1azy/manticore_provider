using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class PercolateErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
