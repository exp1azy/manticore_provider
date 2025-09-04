using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the successful response from a search operation.
    /// Contains information about the time taken for the search, whether it timed out, and the hits returned.
    /// </summary>
    public class SearchSuccess
    {
        /// <summary>
        /// Gets or sets the time taken to execute the search, in milliseconds.
        /// This property provides insight into the performance of the search operation.
        /// </summary>
        [JsonProperty("took")]
        public int Took { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the search operation timed out.
        /// This property helps identify if the search took longer than expected.
        /// </summary>
        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        /// <summary>
        /// Gets or sets the object containing the hits returned by the search.
        /// This property includes details about each hit, such as its ID and score.
        /// </summary>
        [JsonProperty("hits")]
        public HitsObject Hits { get; set; }
    }

    /// <summary>
    /// Represents the collection of hits returned by a search operation.
    /// Contains the individual hits and information about the total number of hits.
    /// </summary>
    public class HitsObject
    {
        /// <summary>
        /// Gets or sets the individual hits returned by the search.
        /// This property is a collection of <see cref="Hits"/> that match the search criteria.
        /// </summary>
        [JsonProperty("hits")]
        public IEnumerable<Hit> Hits { get; set; }

        /// <summary>
        /// Gets or sets the total number of hits that matched the search criteria.
        /// This property indicates how many documents are available that meet the query.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the relationship of the total hits to the hits returned.
        /// This property can indicate whether the total is an exact count or an approximate count.
        /// </summary>
        [JsonProperty("total_relation")]
        public string TotalRelation { get; set; }
    }

    /// <summary>
    /// Represents an individual hit returned from a search operation.
    /// Contains details about the hit, including its ID, score, and source document.
    /// </summary>
    public class Hit
    {
        /// <summary>
        /// Gets or sets the unique identifier of the document that was matched.
        /// This property is used to reference the specific document in the database or index.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the score of the hit, indicating its relevance to the search query.
        /// This property is typically used for ranking the hits based on their matching accuracy.
        /// </summary>
        [JsonProperty("_score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the source document associated with the hit.
        /// This property contains the actual data of the document in a key-value format.
        /// </summary>
        [JsonProperty("_source")]
        public Dictionary<string, object> Source { get; set; }

        /// <summary>
        /// Gets or sets the highlight details returned from a search operation.
        /// This property contains information about the highlighted fields of the document.
        /// </summary>
        [JsonProperty("highlight")]
        public Dictionary<string, string[]>? Highlight { get; set; }
    }
}
