using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the collection of search hits returned from the ManticoreSearch engine.
    /// This class encapsulates the individual hits (documents) that match the search criteria, 
    /// along with metadata about the total number of hits and their relation.
    /// </summary>
    public class HitsObject
    {
        /// <summary>
        /// Gets or sets the collection of hits (documents) that matched the search query.
        /// Each hit in this collection contains the details of a document, such as its ID, 
        /// score, and other relevant information, allowing for easy access to the 
        /// individual search results.
        /// </summary>
        [JsonProperty("hits")]
        public IEnumerable<Hits> Hits { get; set; }

        /// <summary>
        /// Gets or sets the total number of hits that match the search query.
        /// This property provides a count of all documents that satisfy the 
        /// search conditions, regardless of the pagination settings, allowing 
        /// users to understand the scope of the search results.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the relation type of the total hits count.
        /// This property indicates how the total number of hits relates to the 
        /// actual hits returned, such as whether the total count is exact 
        /// or an estimate, providing context for interpreting the results.
        /// </summary>
        [JsonProperty("total_relation")]
        public string TotalRelation { get; set; }
    }
}
