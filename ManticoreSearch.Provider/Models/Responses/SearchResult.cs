using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the search result container for documents of specified type from Manticore Search.
    /// </summary>
    /// <typeparam name="TDocument">The type of document returned in search results. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
    public class SearchResult<TDocument> where TDocument : ManticoreDocument
    {
        /// <summary>
        /// The time taken by the search operation in milliseconds.
        /// </summary>
        [JsonProperty("took")]
        public int Took { get; set; }

        /// <summary>
        /// Indicates whether the search operation timed out.
        /// </summary>
        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        /// <summary>
        /// The hits object containing search results and metadata.
        /// </summary>
        [JsonProperty("hits")]
        public HitsObject<TDocument> Hits { get; set; }
    }

    /// <summary>
    /// Represents the hits container with search results and total count information.
    /// </summary>
    /// <typeparam name="TDocument">The type of document returned in search results. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
    public class HitsObject<TDocument> where TDocument : ManticoreDocument
    {
        /// <summary>
        /// The total number of documents matching the search query.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// The relation of the total count to the actual number of matching documents.
        /// Can be "eq" for exact count or "gte" for lower bound estimate.
        /// </summary>
        [JsonProperty("total_relation")]
        public string TotalRelation { get; set; }

        /// <summary>
        /// The list of actual search hits containing document data and scoring information.
        /// </summary>
        [JsonProperty("hits")]
        public List<Hits<TDocument>> Hits { get; set; }
    }

    /// <summary>
    /// Represents an individual search hit containing document data and metadata.
    /// </summary>
    /// <typeparam name="TDocument">The type of document returned in search results. Must be inherited from <see cref="ManticoreDocument"/>.</typeparam>
    public class Hits<TDocument> where TDocument : ManticoreDocument
    {
        /// <summary>
        /// The unique identifier of the document in the search index.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// The relevance score of the document for the search query.
        /// Higher scores indicate better relevance.
        /// </summary>
        [JsonProperty("_score")]
        public int Score { get; set; }

        /// <summary>
        /// The actual document data returned from the search index.
        /// </summary>
        [JsonProperty("_source")]
        public TDocument Source { get; set; }
    }
}
