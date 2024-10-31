using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class UpdateRequest
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; } = 0;

        [JsonProperty("doc")]
        public Dictionary<string, object> Document { get; set; }

        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        public UpdateRequest() { }

        public UpdateRequest(string index, Dictionary<string, object> document, long id = 0, Query? query = null)
        {
            Index = index;
            Id = id;
            Document = document;
            Query = query;
        }
    }
}
