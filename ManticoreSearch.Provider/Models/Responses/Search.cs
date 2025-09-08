using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the successful response from a search operation.
    /// </summary>
    public class SearchSuccess
    {
        /// <summary>
        /// Gets or sets the time taken to execute the search, in milliseconds.
        /// </summary>
        [JsonProperty("took")]
        public int Took { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the search operation timed out.
        /// </summary>
        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        /// <summary>
        /// Gets or sets the object containing the hits returned by the search.
        /// </summary>
        [JsonProperty("hits")]
        public HitsObject Hits { get; set; }

        /// <summary>
        /// Gets or sets the profile information for the search operation.
        /// </summary>
        [JsonProperty("profile")]
        public Profile? Profile { get; set; }

        /// <summary>
        /// Gets or sets the scroll ID for the search operation.
        /// </summary>
        [JsonProperty("scroll")]
        public string? Scroll { get; set; }

        /// <summary>
        /// Gets or sets the aggregations for the search operation.
        /// </summary>
        [JsonProperty("aggregations")]
        public object? Aggregations { get; set; }
    }

    /// <summary>
    /// Represents the profile information for a search operation.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Gets or sets the query for the search operation.
        /// </summary>
        [JsonProperty("query")]
        public ProfileQuery[]? Query { get; set; }
    }

    /// <summary>
    /// Represents the query profile information for a search operation.
    /// </summary>
    public class ProfileQuery
    {
        /// <summary>
        /// Describes the specific state where the time was spent.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Shows the wall clock time, in seconds.
        /// </summary>
        [JsonProperty("duration")]
        public double Duration { get; set; }

        /// <summary>
        /// Displays the number of times the query engine changed to the given state.
        /// These are merely logical engine state switches and not any OS level context switches or function calls (although some sections might actually map to function calls), and they do not have any direct effect on performance.
        /// In a sense, the number of switches is just the number of times the respective instrumentation point was hit.
        /// </summary>
        [JsonProperty("switches")]
        public int Switches { get; set; }

        /// <summary>
        /// Shows the percentage of time spent in this state.
        /// </summary>
        [JsonProperty("percent")]
        public float Percent { get; set; }
    }

    /// <summary>
    /// Represents the collection of hits returned by a search operation.
    /// </summary>
    public class HitsObject
    {
        /// <summary>
        /// Gets or sets the individual hits returned by the search.
        /// This property is a collection of <see cref="Hits"/> that match the search criteria.
        /// </summary>
        [JsonProperty("hits")]
        public Hit[] Hits { get; set; }

        /// <summary>
        /// Gets or sets the total number of hits that matched the search criteria.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the relationship of the total hits to the hits returned.
        /// </summary>
        [JsonProperty("total_relation")]
        public string TotalRelation { get; set; }
    }

    /// <summary>
    /// Represents an individual hit returned from a search operation.
    /// </summary>
    public class Hit
    {
        /// <summary>
        /// Gets or sets the unique identifier of the document that was matched.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the score of the hit, indicating its relevance to the search query.
        /// </summary>
        [JsonProperty("_score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the source document associated with the hit.
        /// </summary>
        [JsonProperty("_source")]
        public Dictionary<string, object> Source { get; set; }

        /// <summary>
        /// Gets or sets the highlight details returned from a search operation.
        /// </summary>
        [JsonProperty("highlight")]
        public Dictionary<string, string[]>? Highlight { get; set; }

        /// <summary>
        /// Gets or sets the KNN distance of the hit.
        /// </summary>
        [JsonProperty("_knn_dist")]
        public double? KnnDistance { get; set; }
    }
}
