using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class PercolateRequest
    {
        [JsonProperty("query")]
        public PercolateRequestQuery Query { get; set; }

        public PercolateRequest() { }

        public PercolateRequest(PercolateRequestQuery query)
        {
            Query = query;
        }
    }
}
