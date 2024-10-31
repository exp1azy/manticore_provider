using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class BulkDeleteRequest
    {
        [JsonProperty("delete")]
        public DeleteRequest Delete { get; set; }

        public BulkDeleteRequest() { }

        public BulkDeleteRequest(DeleteRequest delete)
        {
            Delete = delete;
        }
    }
}
