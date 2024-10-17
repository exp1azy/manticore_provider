using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class BulkItem
    {
        [JsonProperty("bulk")]
        public BulkResult Bulk { get; set; }
    }
}
