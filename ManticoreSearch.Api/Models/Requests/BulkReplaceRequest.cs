using Newtonsoft.Json;
namespace ManticoreSearch.Api.Models.Requests
{
    public class BulkReplaceRequest
    {
        [JsonProperty("replace")]
        public InsertRequest Replace { get; set; }

        public BulkReplaceRequest() { }

        public BulkReplaceRequest(InsertRequest replace)
        {
            Replace = replace;
        }
    }
}
