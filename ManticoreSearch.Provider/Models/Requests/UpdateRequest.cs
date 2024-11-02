using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request to update a document in a specified table (index) within the ManticoreSearch system.
    /// </summary>
    public class UpdateRequest
    {
        /// <summary>
        /// The name of the table (index) where the document is located.
        /// </summary>
        [JsonProperty("table")]
        public string Index { get; set; }

        /// <summary>
        /// (Optional) The unique identifier of the document to be updated. 
        /// If not specified, the update may rely on the provided query.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        /// <summary>
        /// A dictionary representing the fields and their updated values for the document.
        /// </summary>
        [JsonProperty("doc")]
        public Dictionary<string, object> Document { get; set; }

        /// <summary>
        /// (Optional) A query to identify documents to be updated if the ID is not provided.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Query? Query { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRequest"/> class.
        /// </summary>
        public UpdateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRequest"/> class with specified parameters.
        /// </summary>
        /// <param name="index">The name of the table (index) where the document is located.</param>
        /// <param name="document">A dictionary of fields and values to update in the document.</param>
        /// <param name="id">The unique identifier of the document to update.</param>
        /// <param name="query">An optional query to find documents to update if no ID is provided.</param>
        public UpdateRequest(string index, Dictionary<string, object> document, long id = 0, Query? query = null)
        {
            Index = index;
            Id = id;
            Document = document;
            Query = query;
        }
    }
}
