using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class BulkReplaceItem
    {
        [JsonProperty("replace")]
        public InsertResponse Replace { get; set; }
    }
}
