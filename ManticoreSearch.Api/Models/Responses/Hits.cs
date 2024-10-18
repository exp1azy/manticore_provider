using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents a single search hit (document) returned from the ManticoreSearch engine.
    /// This class contains information about the document, including its unique identifier,
    /// relevance score, and the actual source data of the document.
    /// </summary>
    public class Hits
    {
        /// <summary>
        /// Gets or sets the unique identifier of the document retrieved from the search results.
        /// This ID is used to reference the document in the database and can be utilized
        /// for further operations such as updates or deletions.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the relevance score of the document as determined by the search engine.
        /// This score reflects how well the document matches the search query, with higher scores
        /// indicating greater relevance. It can be used to rank the results presented to the user.
        /// </summary>
        [JsonProperty("_score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the source data of the document, represented as a dictionary of key-value pairs.
        /// This property contains the actual content of the document that matched the search query,
        /// allowing users to access and display relevant information directly from the search results.
        /// </summary>
        [JsonProperty("_source")]
        public Dictionary<string, object> Source { get; set; }
    }
}