using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class MappingRequest
    {
        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; set; }

        public MappingRequest() { }

        public MappingRequest(Dictionary<string, object> properties)
        {
            Properties = properties;
        }
    }
}
