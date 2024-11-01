using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class BulkUpdateRequest
    {
        [JsonProperty("update")]
        public UpdateRequest Update { get; set; }

        public BulkUpdateRequest() { }

        public BulkUpdateRequest(UpdateRequest update) 
        { 
            Update = update;
        }
    }
}
