using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class UpdateRequest
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("doc")]
        public Dictionary<string, object> Document { get; set; }

        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        public UpdateRequest() { }

        public UpdateRequest(string index, Dictionary<string, object> document, long id = 0, Query? query = null)
        {
            Table = index;
            Id = id;
            Document = document;
            Query = query;
        }
    }
}
