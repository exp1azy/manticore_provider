using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a search request for querying the ManticoreSearch engine.
    /// This class encapsulates various parameters to customize the search operation,
    /// including query criteria, indexing options, and pagination controls.
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        /// Gets or sets the name of the index to search.
        /// This property is required to specify the index from which the search will retrieve documents.
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the query object to filter search results.
        /// This property is optional and can be used to define the criteria for matching documents.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public object? Query { get; set; } = null;

        /// <summary>
        /// Gets or sets the fields to be returned in the search results.
        /// This property is optional; if not provided, all fields will be returned.
        /// </summary>
        [JsonProperty("_source", NullValueHandling = NullValueHandling.Ignore)]
        public object? Source { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether to profile the search execution.
        /// If true, the search performance will be logged, useful for diagnostics.
        /// This property is optional.
        /// </summary>
        [JsonProperty("profile", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Profile { get; set; } = null;

        /// <summary>
        /// Gets or sets the aggregation options for the search results.
        /// This property is optional and can be used to summarize data based on specific criteria.
        /// </summary>
        [JsonProperty("aggs", NullValueHandling = NullValueHandling.Ignore)]
        public object? Aggregations { get; set; } = null;

        /// <summary>
        /// Gets or sets the maximum number of search results to be returned.
        /// This property is optional; if not specified, default behavior will apply.
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; } = null;

        /// <summary>
        /// Gets or sets the offset from which to start returning results.
        /// This property is useful for implementing pagination and is optional.
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; } = null;

        /// <summary>
        /// Gets or sets the number of hits to return from the search.
        /// This property is optional and may be used for limiting the size of results.
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public int? Size { get; set; } = null;

        /// <summary>
        /// Gets or sets the starting point for returning search results.
        /// This property is optional and can be used in conjunction with the <see cref="Size"/> property for pagination.
        /// </summary>
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public int? From { get; set; } = null;

        /// <summary>
        /// Gets or sets the maximum number of matches to return for the search query.
        /// This property is optional and may be set to control the amount of returned results.
        /// </summary>
        [JsonProperty("max_matches", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxMatches { get; set; } = null;

        /// <summary>
        /// Gets or sets the sorting criteria for the search results.
        /// This property is optional and can be used to specify the order in which results are returned.
        /// </summary>
        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public List<object>? Sort { get; set; } = null;

        /// <summary>
        /// Gets or sets the fields calculated using scripts to be included in the results.
        /// This property is optional and allows for dynamic field generation based on query logic.
        /// </summary>
        [JsonProperty("script_fields", NullValueHandling = NullValueHandling.Ignore)]
        public object? ScriptFields { get; set; } = null;

        /// <summary>
        /// Gets or sets the expressions to be evaluated during the search.
        /// This property is optional and allows for custom logic to be applied to the search results.
        /// </summary>
        [JsonProperty("expressions", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string>? Expressions { get; set; } = null;

        /// <summary>
        /// Gets or sets the additional options for customizing the search behavior.
        /// This property is optional and can include various settings specific to the search operation.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Options { get; set; } = null;

        /// <summary>
        /// Gets or sets the highlight settings for search results.
        /// This property is optional and can be used to emphasize matching terms in the results.
        /// </summary>
        [JsonProperty("highlight", NullValueHandling = NullValueHandling.Ignore)]
        public object? Highlight { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether to track scores for the search results.
        /// If true, the scores will be included in the search response, aiding in relevance evaluation.
        /// This property is optional.
        /// </summary>
        [JsonProperty("track_scores", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TrackScores { get; set; } = null;

        /// <summary>
        /// Gets or sets the K-Nearest Neighbors (KNN) search parameters.
        /// This property is optional and can be used to specify criteria for KNN searches within the index.
        /// </summary>
        [JsonProperty("knn", NullValueHandling = NullValueHandling.Ignore)]
        public object? Knn { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchRequest"/> class.
        /// This default constructor is used to create an empty search request.
        /// </summary>
        public SearchRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchRequest"/> class with specified parameters.
        /// This constructor allows for the customization of the search request upon creation.
        /// </summary>
        /// <param name="index">The index name to search.</param>
        /// <param name="query">The search query criteria.</param>
        /// <param name="source">The fields to be returned in the search results.</param>
        /// <param name="profile">Indicates whether to profile the search execution.</param>
        /// <param name="aggs">The aggregation options for the search results.</param>
        /// <param name="limit">The maximum number of search results to be returned.</param>
        /// <param name="offset">The offset from which to start returning results.</param>
        /// <param name="maxMatches">The maximum number of matches to return.</param>
        /// <param name="sort">The sorting criteria for the search results.</param>
        /// <param name="scriptFields">The fields calculated using scripts.</param>
        /// <param name="expressions">The expressions to be evaluated during the search.</param>
        /// <param name="options">The additional options for customizing the search behavior.</param>
        /// <param name="highlight">The highlight settings for search results.</param>
        /// <param name="trackScores">Indicates whether to track scores for the search results.</param>
        /// <param name="knn">The KNN search parameters.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchRequest"/> class with specified parameters for pagination.
        /// This constructor allows for specifying the size and from parameters to control the result set.
        /// </summary>
        /// <param name="index">The index name to search.</param>
        /// <param name="query">The search query criteria.</param>
        /// <param name="source">The fields to be returned in the search results.</param>
        /// <param name="profile">Indicates whether to profile the search execution.</param>
        /// <param name="size">The number of hits to return from the search.</param>
        /// <param name="from">The starting point for returning search results.</param>
        /// <param name="aggs">The aggregation options for the search results.</param>
        /// <param name="maxMatches">The maximum number of matches to return.</param>
        /// <param name="sort">The sorting criteria for the search results.</param>
        /// <param name="scriptFields">The fields calculated using scripts.</param>
        /// <param name="expressions">The expressions to be evaluated during the search.</param>
        /// <param name="options">The additional options for customizing the search behavior.</param>
        /// <param name="highlight">The highlight settings for search results.</param>
        /// <param name="trackScores">Indicates whether to track scores for the search results.</param>
        /// <param name="knn">The KNN search parameters.</param>
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
