using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class BulkInsertRequest
    {
        [JsonProperty("insert")]
        public InsertRequest Insert { get; set; }

        public BulkInsertRequest() { }

        public BulkInsertRequest(InsertRequest insert)
        {
            Insert = insert;
        }
    }
}
