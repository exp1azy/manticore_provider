using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    public class SearchRequest
    {
        [JsonProperty("index")]
        public string Index { get; set; } = "";

        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public object? Query { get; set; } = null;

        [JsonProperty("_source", NullValueHandling = NullValueHandling.Ignore)]
        public object? Source { get; set; } = null;

        [JsonProperty("profile", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Profile { get; set; } = null;

        [JsonProperty("aggs", NullValueHandling = NullValueHandling.Ignore)]
        public object? Aggregations { get; set; } = null;

        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; } = null;

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; } = null;

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public int? Size { get; set; } = null;

        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public int? From { get; set; } = null;

        [JsonProperty("max_matches", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxMatches { get; set; } = null;

        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Sort { get; set; } = null;

        [JsonProperty("script_fields", NullValueHandling = NullValueHandling.Ignore)]
        public object? ScriptFields { get; set; } = null;

        [JsonProperty("expressions", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string>? Expressions { get; set; } = null;

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Options { get; set; } = null;

        [JsonProperty("highlight", NullValueHandling = NullValueHandling.Ignore)]
        public object? Highlight { get; set; } = null;

        [JsonProperty("track_scores", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TrackScores { get; set; } = null;

        [JsonProperty("knn", NullValueHandling = NullValueHandling.Ignore)]
        public object? Knn { get; set; } = null;

        public SearchRequest() { }

        public SearchRequest(
            string index = "", 
            object? query = null, 
            object? source = null,
            bool? profile = null,
            object? aggs = null,
            int? limit = null,
            int? offset = null,
            int? maxMatches = null,
            List<object>? sort = null,
            object? scriptFields = null,
            Dictionary<string, string>? expressions = null,
            Dictionary<string, object>? options = null,
            object? highlight = null,
            bool? trackScores = null,
            object? knn = null)
        {
            Index = index; 
            Query = query;
            Source = source;
            Profile = profile;
            Aggregations = aggs;
            Limit = limit;
            Offset = offset;
            MaxMatches = maxMatches;
            Sort = sort;
            ScriptFields = scriptFields;
            Expressions = expressions;
            Options = options;
            Highlight = highlight;
            TrackScores = trackScores;
            Knn = knn;
        }

        public SearchRequest(
            string index = "",
            object? query = null,
            object? source = null,
            bool? profile = null,       
            int? size = null,
            int? from = null,
            object? aggs = null,
            int? maxMatches = null,
            List<object>? sort = null,
            object? scriptFields = null,
            Dictionary<string, string>? expressions = null,
            Dictionary<string, object>? options = null,
            object? highlight = null,
            bool? trackScores = null,
            object? knn = null)
        {
            Index = index;
            Query = query;
            Source = source;
            Profile = profile;
            Aggregations = aggs;
            Size = size;
            From = from;
            MaxMatches = maxMatches;
            Sort = sort;
            ScriptFields = scriptFields;
            Expressions = expressions;
            Options = options;
            Highlight = highlight;
            TrackScores = trackScores;
            Knn = knn;
        }
    }
}
