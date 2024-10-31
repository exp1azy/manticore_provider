using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ManticoreSearch.Api.Models.Requests
{
    public class SearchRequest
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public object? QueryByObject { get; set; }

        [JsonProperty("_source", NullValueHandling = NullValueHandling.Ignore)]
        public object? Source { get; set; }

        [JsonProperty("profile", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Profile { get; set; }

        [JsonProperty("aggs", NullValueHandling = NullValueHandling.Ignore)]
        public object? Aggs { get; set; }

        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public int? Size { get; set; }

        [JsonProperty("max_matches", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxMatches { get; set; }

        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Sort { get; set; }

        [JsonProperty("script_fields", NullValueHandling = NullValueHandling.Ignore)]
        public object? ScriptFields { get; set; }

        [JsonProperty("expressions", NullValueHandling = NullValueHandling.Ignore)]
        public object? Expressions { get; set; }

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public object? Options { get; set; }

        [JsonProperty("highlight", NullValueHandling = NullValueHandling.Ignore)]
        public HighlightOptions? Highlight { get; set; }

        [JsonProperty("track_scores", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TrackScores { get; set; }

        [JsonProperty("join", NullValueHandling = NullValueHandling.Ignore)]
        public List<SearchJoin>? Join { get; set; }

        [JsonProperty("knn", NullValueHandling = NullValueHandling.Ignore)]
        public object? Knn { get; set; }

        public SearchRequest() { }

        public SearchRequest(object search)
        {
            QueryByObject = search;
        }
    }

    public class HighlightOptions
    {
        [JsonProperty("before_match", NullValueHandling = NullValueHandling.Ignore)]
        public string? BeforeMatch { get; set; }

        [JsonProperty("after_match", NullValueHandling = NullValueHandling.Ignore)]
        public string? AfterMatch { get; set; }

        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        [JsonProperty("limit_words", NullValueHandling = NullValueHandling.Ignore)]
        public int? LimitWords { get; set; }

        [JsonProperty("limit_snippets", NullValueHandling = NullValueHandling.Ignore)]
        public int? LimitSnippets { get; set; }

        [JsonProperty("limits_per_field", NullValueHandling = NullValueHandling.Ignore)]
        public int? LimitsPerField { get; set; }

        [JsonProperty("around", NullValueHandling = NullValueHandling.Ignore)]
        public int? Around { get; set; }

        [JsonProperty("use_boundaries", NullValueHandling = NullValueHandling.Ignore)]
        public int? UseBoundaries { get; set; }

        [JsonProperty("weight_order", NullValueHandling = NullValueHandling.Ignore)]
        public int? WeightOrder { get; set; }

        [JsonProperty("force_all_words", NullValueHandling = NullValueHandling.Ignore)]
        public int? ForceAllWords { get; set; }

        [JsonProperty("start_snippet_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? StartSnippetId { get; set; }

        [JsonProperty("html_strip_mode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public HtmlStripMode? HtmlStripMode { get; set; }

        [JsonProperty("allow_empty", NullValueHandling = NullValueHandling.Ignore)]
        public int? AllowEmpty { get; set; }

        [JsonProperty("snippet_boundary", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SnippetBoundary? SnippetBoundary { get; set; }

        [JsonProperty("emit_zones", NullValueHandling = NullValueHandling.Ignore)]
        public int? EmitZones { get; set; }

        [JsonProperty("force_snippets", NullValueHandling = NullValueHandling.Ignore)]
        public int? ForceSnippets { get; set; }

        [JsonProperty("snippet_separator", NullValueHandling = NullValueHandling.Ignore)]
        public string? SnippetSeparator { get; set; }

        [JsonProperty("field_separator", NullValueHandling = NullValueHandling.Ignore)]
        public string? FieldSeparator { get; set; }

        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Fields { get; set; }

        [JsonProperty("encoder", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Encoder? Encoder { get; set; }

        [JsonProperty("highlight_query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? HighlightQuery { get; set; }

        [JsonProperty("pre_tags", NullValueHandling = NullValueHandling.Ignore)]
        public string? PreTags { get; set; }

        [JsonProperty("post_tags", NullValueHandling = NullValueHandling.Ignore)]
        public string? PostTags { get; set; }

        [JsonProperty("no_match_size", NullValueHandling = NullValueHandling.Ignore)]
        public int? NoMatchSize { get; set; }

        [JsonProperty("order", NullValueHandling = NullValueHandling.Ignore)]
        public string? Order { get; set; }

        [JsonProperty("fragment_size", NullValueHandling = NullValueHandling.Ignore)]
        public int? FragmentSize { get; set; }

        [JsonProperty("number_of_fragments", NullValueHandling = NullValueHandling.Ignore)]
        public int? NumberOfFragments { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Encoder
    {
        @default,
        html
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SnippetBoundary
    {
        sentence,
        paragraph,
        zone
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum HtmlStripMode
    {
        none,
        strip,
        index,
        retain
    }

    public class SearchJoin
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("on")]
        public List<JoinOn> On { get; set; }

        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; } = null;
    }

    public class JoinOn
    {
        [JsonProperty("left")]
        public Side Left { get; set; }

        [JsonProperty("right")]
        public Side Right { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }
    }

    public class Side
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }
    }
}
