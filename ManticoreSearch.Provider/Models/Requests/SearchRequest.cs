using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a search request to be sent to a search service.
    /// This class encapsulates various parameters that can be used 
    /// to customize the search operation.
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        /// Gets or sets the name of the index to search in. 
        /// This is a required field.
        /// </summary>
        [JsonProperty("table")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the query object to filter the search results. 
        /// This can be a complex query defined by the Query class.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        /// <summary>
        /// Gets or sets options for specifying which fields to include in the response. 
        /// This provides more granular control over the returned data.
        /// </summary>
        [JsonProperty("_source", NullValueHandling = NullValueHandling.Ignore)]
        public SourceOptions? SourceByOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to profile the query execution. 
        /// If true, profiling information will be included in the response.
        /// </summary>
        [JsonProperty("profile", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Profile { get; set; }

        /// <summary>
        /// Gets or sets aggregation options for grouping and summarizing search results. 
        /// This allows for performing calculations over the data.
        /// </summary>
        [JsonProperty("aggs", NullValueHandling = NullValueHandling.Ignore)]
        public object? Aggs { get; set; }

        /// <summary>
        /// Gets or sets the limit for the number of search results returned. 
        /// This can be used for pagination.
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        /// <summary>
        /// Gets or sets the offset for the search results. 
        /// This can be used for pagination in conjunction with limit.
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to return. 
        /// This is a convenience property that can also be used for pagination.
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public int? Size { get; set; }

        /// <summary>
        /// Gets or sets the starting point from which to return results. 
        /// This can be used to control pagination.
        /// </summary>
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public int? From { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of matches to return. 
        /// This can be useful to limit the response size.
        /// </summary>
        [JsonProperty("max_matches", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxMatches { get; set; }

        /// <summary>
        /// Gets or sets a list of sorting options for the search results. 
        /// This allows for controlling the order of the results returned.
        /// </summary>
        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Sort { get; set; }

        /// <summary>
        /// Gets or sets a set of fields that can be dynamically computed for each hit. 
        /// This allows for custom calculations based on the results.
        /// </summary>
        [JsonProperty("script_fields", NullValueHandling = NullValueHandling.Ignore)]
        public object? ScriptFields { get; set; }

        /// <summary>
        /// Gets or sets custom expressions that can be evaluated as part of the search. 
        /// This allows for advanced search capabilities.
        /// </summary>
        [JsonProperty("expressions", NullValueHandling = NullValueHandling.Ignore)]
        public object? Expressions { get; set; }

        /// <summary>
        /// Gets or sets a dictionary of additional options to customize the search behavior. 
        /// This allows for more fine-tuned control of the search request.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, OptionDetails>? Options { get; set; }

        /// <summary>
        /// Gets or sets highlighting options for emphasizing search terms in the results. 
        /// This improves the visibility of matched terms.
        /// </summary>
        [JsonProperty("highlight", NullValueHandling = NullValueHandling.Ignore)]
        public HighlightOptions? Highlight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to track scores for each result. 
        /// This can be useful for understanding how results are ranked.
        /// </summary>
        [JsonProperty("track_scores", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TrackScores { get; set; }

        /// <summary>
        /// Gets or sets a list of join options for combining multiple queries. 
        /// This enables advanced query patterns by joining results.
        /// </summary>
        [JsonProperty("join", NullValueHandling = NullValueHandling.Ignore)]
        public List<SearchJoin>? Join { get; set; }

        /// <summary>
        /// Gets or sets options for performing k-nearest neighbors (KNN) searches. 
        /// This can be used for similarity-based queries.
        /// </summary>
        [JsonProperty("knn", NullValueHandling = NullValueHandling.Ignore)]
        public KnnOptions? Knn { get; set; }

        /// <summary>
        /// Initializes a new instance of the SearchRequest class.
        /// </summary>
        public SearchRequest() { }

        /// <summary>
        /// Initializes a new instance of the SearchRequest class with specified parameters.
        /// </summary>
        /// <param name="index">The index to search.</param>
        /// <param name="query">The query object.</param>
        /// <param name="queryByObject">An alternative query representation as an object.</param>
        /// <param name="source">Fields to include in the response.</param>
        /// <param name="sourceByOptions">Options for source fields.</param>
        /// <param name="profile">Indicates whether to profile the query execution.</param>
        /// <param name="aggs">Aggregation options.</param>
        /// <param name="limit">Limit for the number of search results.</param>
        /// <param name="offset">Offset for search results.</param>
        /// <param name="size">Maximum number of results to return.</param>
        /// <param name="from">Starting point for returning results.</param>
        /// <param name="maxMatches">Maximum number of matches to return.</param>
        /// <param name="sort">Sorting options.</param>
        /// <param name="scriptFields">Dynamically computed fields for each hit.</param>
        /// <param name="expressions">Custom expressions for the search.</param>
        /// <param name="options">Additional options for the search.</param>
        /// <param name="highlight">Highlighting options for search terms.</param>
        /// <param name="trackScores">Indicates whether to track scores for results.</param>
        /// <param name="join">Join options for combining queries.</param>
        /// <param name="knn">Options for performing KNN searches.</param>
        public SearchRequest(
            string index,
            Query? query = null,
            SourceOptions? sourceByOptions = null,
            bool? profile = null,
            object? aggs = null,
            int? limit = null,
            int? offset = null,
            int? size = null,
            int? from = null,
            int? maxMatches = null,
            List<object>? sort = null,
            object? scriptFields = null,
            object? expressions = null,
            Dictionary<string, OptionDetails>? options = null,
            HighlightOptions? highlight = null,
            bool? trackScores = null,
            List<SearchJoin>? join = null,
            KnnOptions? knn = null)
        {
            Index = index;
            Query = query;
            SourceByOptions = sourceByOptions;
            Profile = profile;
            Aggs = aggs;
            Limit = limit;
            Offset = offset;
            Size = size;
            From = from;
            MaxMatches = maxMatches;
            Sort = sort;
            ScriptFields = scriptFields;
            Expressions = expressions;
            Options = options;
            Highlight = highlight;
            TrackScores = trackScores;
            Join = join;
            Knn = knn;
        }
    }

    /// <summary>
    /// Enumeration of country codes based on ISO 3166-1 alpha-2 standard.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CountryCode
    {
        /// <summary>
        /// Belgium.
        /// </summary>
        [EnumMember(Value = "be")]
        BE,

        /// <summary>
        /// Bulgaria.
        /// </summary>
        [EnumMember(Value = "bg")]
        BG,

        /// <summary>
        /// Brazil.
        /// </summary>
        [EnumMember(Value = "br")]
        BR,

        /// <summary>
        /// Switzerland.
        /// </summary>
        [EnumMember(Value = "ch")]
        CH,

        /// <summary>
        /// Germany.
        /// </summary>
        [EnumMember(Value = "de")]
        DE,

        /// <summary>
        /// Denmark.
        /// </summary>
        [EnumMember(Value = "dk")]
        DK,

        /// <summary>
        /// Spain.
        /// </summary>
        [EnumMember(Value = "es")]
        ES,

        /// <summary>
        /// France.
        /// </summary>
        [EnumMember(Value = "fr")]
        FR,

        /// <summary>
        /// United Kingdom.
        /// </summary>
        [EnumMember(Value = "uk")]
        UK,

        /// <summary>
        /// Greece.
        /// </summary>
        [EnumMember(Value = "gr")]
        GR,

        /// <summary>
        /// Italy.
        /// </summary>
        [EnumMember(Value = "it")]
        IT,

        /// <summary>
        /// Norway.
        /// </summary>
        [EnumMember(Value = "no")]
        NO,

        /// <summary>
        /// Portugal.
        /// </summary>
        [EnumMember(Value = "pt")]
        PT,

        /// <summary>
        /// Russia.
        /// </summary>
        [EnumMember(Value = "ru")]
        RU,

        /// <summary>
        /// Sweden.
        /// </summary>
        [EnumMember(Value = "se")]
        SE,

        /// <summary>
        /// Ukraine.
        /// </summary>
        [EnumMember(Value = "ua")]
        UA,

        /// <summary>
        /// United States.
        /// </summary>
        [EnumMember(Value = "us")]
        US
    }

    /// <summary>
    /// Specifies the sorting mode for search results.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortMode
    {
        /// <summary>
        /// Sort by the minimum value.
        /// </summary>
        [EnumMember(Value = "min")]
        Min,

        /// <summary>
        /// Sort by the maximum value.
        /// </summary>
        [EnumMember(Value = "max")]
        Max
    }

    /// <summary>
    /// Defines the order in which search results are sorted.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortOrder
    {
        /// <summary>
        /// Sort results in ascending order.
        /// </summary>
        [EnumMember(Value = "asc")]
        Asc,

        /// <summary>
        /// Sort results in descending order.
        /// </summary>
        [EnumMember(Value = "desc")]
        Desc
    }

    /// <summary>
    /// Specifies the encoding type for text output.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Encoder
    {
        /// <summary>
        /// Use default encoding.
        /// </summary>
        [EnumMember(Value = "default")]
        Default,

        /// <summary>
        /// Encode output as HTML.
        /// </summary>
        [EnumMember(Value = "html")]
        Html
    }

    /// <summary>
    /// Specifies the boundaries for generating snippets from text.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SnippetBoundary
    {
        /// <summary>
        /// Use sentence boundaries for snippet extraction.
        /// </summary>
        [EnumMember(Value = "sentence")]
        Sentence,

        /// <summary>
        /// Use paragraph boundaries for snippet extraction.
        /// </summary>
        [EnumMember(Value = "paragraph")]
        Paragraph,

        /// <summary>
        /// Use custom-defined zones for snippet extraction.
        /// </summary>
        [EnumMember(Value = "zone")]
        Zone
    }

    /// <summary>
    /// Defines the HTML stripping behavior for text processing.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HtmlStripMode
    {
        /// <summary>
        /// No HTML stripping is performed.
        /// </summary>
        [EnumMember(Value = "none")]
        None,

        /// <summary>
        /// Strips all HTML tags from the text.
        /// </summary>
        [EnumMember(Value = "strip")]
        Strip,

        /// <summary>
        /// Strips HTML tags but retains content for indexing.
        /// </summary>
        [EnumMember(Value = "index")]
        Index,

        /// <summary>
        /// Retains HTML tags and content as is.
        /// </summary>
        [EnumMember(Value = "retain")]
        Retain
    }

    /// <summary>
    /// Represents options for configuring k-nearest neighbors (KNN) search queries.
    /// </summary>
    public class KnnOptions
    {
        /// <summary>
        /// The field in the index to perform the KNN search on.
        /// </summary>
        [JsonProperty("field")]
        public string Field { get; set; }

        /// <summary>
        /// The query vector used to find the nearest neighbors.
        /// </summary>
        [JsonProperty("query_vector")]
        public List<float> QueryVector { get; set; }

        /// <summary>
        /// The number of nearest neighbors to retrieve.
        /// </summary>
        [JsonProperty("k")]
        public int K { get; set; }

        /// <summary>
        /// (Optional) The size of the dynamic candidate list. Higher values may improve recall but increase query time.
        /// </summary>
        [JsonProperty("ef", NullValueHandling = NullValueHandling.Ignore)]
        public int? Ef { get; set; }

        /// <summary>
        /// (Optional) The document ID to focus the search on a specific document.
        /// </summary>
        [JsonProperty("doc_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? DocId { get; set; }

        /// <summary>
        /// (Optional) A filter query to narrow down the KNN search results.
        /// </summary>
        [JsonProperty("filter", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Filter { get; set; }
    }

    /// <summary>
    /// Represents the options for search queries with additional configuration details.
    /// </summary>
    public class OptionDetails
    {
        /// <summary>
        /// Indicates whether fuzzy matching is enabled for the search query.
        /// </summary>
        [JsonProperty("fuzzy")]
        public bool Fuzzy { get; set; }

        /// <summary>
        /// A list of country codes defining the layouts to be used in the search.
        /// </summary>
        [JsonProperty("layouts")]
        public List<CountryCode> Layouts { get; set; }

        /// <summary>
        /// (Optional) The distance parameter for proximity searches, which defines the allowable distance for matches.
        /// </summary>
        [JsonProperty("distance", NullValueHandling = NullValueHandling.Ignore)]
        public int? Distance { get; set; }
    }

    /// <summary>
    /// Represents options for including or excluding fields in the search results.
    /// </summary>
    public class SourceOptions
    {
        /// <summary>
        /// (Optional) A list of fields to include in the search results.
        /// </summary>
        [JsonProperty("includes", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Includes { get; set; }

        /// <summary>
        /// (Optional) A list of fields to exclude from the search results.
        /// </summary>
        [JsonProperty("excludes", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Excludes { get; set; }
    }

    /// <summary>
    /// Represents the options for configuring highlighting in search results.
    /// </summary>
    public class HighlightOptions
    {
        /// <summary>
        /// (Optional) A string to be added before each match in the highlight output.
        /// </summary>
        [JsonProperty("before_match", NullValueHandling = NullValueHandling.Ignore)]
        public string? BeforeMatch { get; set; }

        /// <summary>
        /// (Optional) A string to be added after each match in the highlight output.
        /// </summary>
        [JsonProperty("after_match", NullValueHandling = NullValueHandling.Ignore)]
        public string? AfterMatch { get; set; }

        /// <summary>
        /// (Optional) The maximum number of highlighted fragments to return.
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        /// <summary>
        /// (Optional) The maximum number of words per highlighted snippet.
        /// </summary>
        [JsonProperty("limit_words", NullValueHandling = NullValueHandling.Ignore)]
        public int? LimitWords { get; set; }

        /// <summary>
        /// (Optional) The maximum number of snippets to return for each field.
        /// </summary>
        [JsonProperty("limit_snippets", NullValueHandling = NullValueHandling.Ignore)]
        public int? LimitSnippets { get; set; }

        /// <summary>
        /// (Optional) The limit for the number of snippets per field.
        /// </summary>
        [JsonProperty("limits_per_field", NullValueHandling = NullValueHandling.Ignore)]
        public int? LimitsPerField { get; set; }

        /// <summary>
        /// (Optional) The number of surrounding words to include around each match.
        /// </summary>
        [JsonProperty("around", NullValueHandling = NullValueHandling.Ignore)]
        public int? Around { get; set; }

        /// <summary>
        /// (Optional) A flag indicating whether to use snippet boundaries.
        /// </summary>
        [JsonProperty("use_boundaries", NullValueHandling = NullValueHandling.Ignore)]
        public int? UseBoundaries { get; set; }

        /// <summary>
        /// (Optional) The weight order for highlighted matches.
        /// </summary>
        [JsonProperty("weight_order", NullValueHandling = NullValueHandling.Ignore)]
        public int? WeightOrder { get; set; }

        /// <summary>
        /// (Optional) A flag indicating whether to force highlighting of all words.
        /// </summary>
        [JsonProperty("force_all_words", NullValueHandling = NullValueHandling.Ignore)]
        public int? ForceAllWords { get; set; }

        /// <summary>
        /// (Optional) The ID of the first snippet to start highlighting from.
        /// </summary>
        [JsonProperty("start_snippet_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? StartSnippetId { get; set; }

        /// <summary>
        /// (Optional) The mode for stripping HTML tags from highlighted text.
        /// </summary>
        [JsonProperty("html_strip_mode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public HtmlStripMode? HtmlStripMode { get; set; }

        /// <summary>
        /// (Optional) A flag indicating whether to allow empty highlights.
        /// </summary>
        [JsonProperty("allow_empty", NullValueHandling = NullValueHandling.Ignore)]
        public int? AllowEmpty { get; set; }

        /// <summary>
        /// (Optional) Specifies the boundary for snippets in the highlight output.
        /// </summary>
        [JsonProperty("snippet_boundary", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SnippetBoundary? SnippetBoundary { get; set; }

        /// <summary>
        /// (Optional) A flag indicating whether to emit zones in the highlight output.
        /// </summary>
        [JsonProperty("emit_zones", NullValueHandling = NullValueHandling.Ignore)]
        public int? EmitZones { get; set; }

        /// <summary>
        /// (Optional) A flag indicating whether to force the inclusion of snippets.
        /// </summary>
        [JsonProperty("force_snippets", NullValueHandling = NullValueHandling.Ignore)]
        public int? ForceSnippets { get; set; }

        /// <summary>
        /// (Optional) A string used to separate snippets in the highlight output.
        /// </summary>
        [JsonProperty("snippet_separator", NullValueHandling = NullValueHandling.Ignore)]
        public string? SnippetSeparator { get; set; }

        /// <summary>
        /// (Optional) A string used to separate fields in the highlight output.
        /// </summary>
        [JsonProperty("field_separator", NullValueHandling = NullValueHandling.Ignore)]
        public string? FieldSeparator { get; set; }

        /// <summary>
        /// (Optional) A list of fields to be highlighted.
        /// </summary>
        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Fields { get; set; }

        /// <summary>
        /// (Optional) The encoder to be used for processing highlighted text.
        /// </summary>
        [JsonProperty("encoder", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Encoder? Encoder { get; set; }

        /// <summary>
        /// (Optional) A custom query to determine the highlights in the search results.
        /// </summary>
        [JsonProperty("highlight_query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? HighlightQuery { get; set; }

        /// <summary>
        /// (Optional) The tags to be used before highlighted terms in the output.
        /// </summary>
        [JsonProperty("pre_tags", NullValueHandling = NullValueHandling.Ignore)]
        public string? PreTags { get; set; }

        /// <summary>
        /// (Optional) The tags to be used after highlighted terms in the output.
        /// </summary>
        [JsonProperty("post_tags", NullValueHandling = NullValueHandling.Ignore)]
        public string? PostTags { get; set; }

        /// <summary>
        /// (Optional) The size of the highlighted text to return when there are no matches.
        /// </summary>
        [JsonProperty("no_match_size", NullValueHandling = NullValueHandling.Ignore)]
        public int? NoMatchSize { get; set; }

        /// <summary>
        /// (Optional) The order of highlighted snippets.
        /// </summary>
        [JsonProperty("order", NullValueHandling = NullValueHandling.Ignore)]
        public string? Order { get; set; }

        /// <summary>
        /// (Optional) The size of the highlighted fragments.
        /// </summary>
        [JsonProperty("fragment_size", NullValueHandling = NullValueHandling.Ignore)]
        public int? FragmentSize { get; set; }

        /// <summary>
        /// (Optional) The number of highlighted fragments to return.
        /// </summary>
        [JsonProperty("number_of_fragments", NullValueHandling = NullValueHandling.Ignore)]
        public int? NumberOfFragments { get; set; }
    }

    /// <summary>
    /// Represents a join operation in a search query, allowing for the combination of results from multiple tables.
    /// </summary>
    public class SearchJoin
    {
        /// <summary>
        /// The type of join to be performed (e.g., INNER JOIN, LEFT JOIN).
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The name of the table to be joined with the main query.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// A list of conditions specifying how the join should be performed, including the fields to join on.
        /// </summary>
        [JsonProperty("on")]
        public List<JoinOn> On { get; set; }

        /// <summary>
        /// (Optional) A query to filter the results of the joined table.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }
    }

    /// <summary>
    /// Represents a specific condition for joining tables, including the fields and operator used in the join.
    /// </summary>
    public class JoinOn
    {
        /// <summary>
        /// The left side of the join condition, specifying the table and field to match against.
        /// </summary>
        [JsonProperty("left")]
        public Side Left { get; set; }

        /// <summary>
        /// The right side of the join condition, specifying the table and field to match against.
        /// </summary>
        [JsonProperty("right")]
        public Side Right { get; set; }

        /// <summary>
        /// The operator used in the join condition (e.g., "=", "<", ">").
        /// </summary>
        [JsonProperty("operator")]
        public string Operator { get; set; }
    }

    /// <summary>
    /// Represents a side of the join condition, defining the table and field involved.
    /// </summary>
    public class Side
    {
        /// <summary>
        /// The name of the table that contains the field for this side of the join.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// The name of the field in the specified table to be used in the join condition.
        /// </summary>
        [JsonProperty("field")]
        public string Field { get; set; }

        /// <summary>
        /// (Optional) The type of the field, providing additional context for the join operation.
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Type { get; set; }
    }
}
