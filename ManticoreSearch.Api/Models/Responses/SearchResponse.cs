using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the response received from the ManticoreSearch engine 
    /// after executing a search query.
    /// This class encapsulates the total time taken for the search, 
    /// whether the search timed out, and the resulting hits from the query.
    /// </summary>
    public class SearchResponse
    {
        /// <summary>
        /// Gets or sets the total time, in milliseconds, that the search took to complete.
        /// This property provides insight into the performance of the search operation.
        /// </summary>
        [JsonProperty("took")]
        public int Took { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the search timed out.
        /// This property indicates if the search operation exceeded the allowed time limit,
        /// which may impact the reliability of the returned results.
        /// </summary>
        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        /// <summary>
        /// Gets or sets the hits returned from the search query.
        /// This property contains the actual results of the search operation, 
        /// including the matching documents and their details.
        /// </summary>
        [JsonProperty("hits")]
        public HitsObject Hits { get; set; }
    }
}
