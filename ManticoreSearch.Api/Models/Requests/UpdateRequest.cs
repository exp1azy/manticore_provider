using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to update a document in the ManticoreSearch engine.
    /// This class encapsulates the necessary parameters for identifying the document 
    /// to be updated, specifying the updated data, and optional query criteria.
    /// </summary>
    public class UpdateRequest
    {
        /// <summary>
        /// Gets or sets the name of the index containing the document to be updated.
        /// This property is required to specify which index the update request targets.
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the document to be updated.
        /// This property is initialized to zero by default, which may indicate a non-set value.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the document containing the fields to be updated.
        /// This property is required to specify the new values for the document's fields.
        /// </summary>
        [JsonProperty("doc")]
        public Dictionary<string, object> Document { get; set; }

        /// <summary>
        /// Gets or sets the optional query criteria for selecting the document(s) to update.
        /// If provided, this property allows for more complex update conditions.
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object>? Query { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRequest"/> class.
        /// This default constructor is used to create an empty update request.
        /// </summary>
        public UpdateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRequest"/> class with specified parameters.
        /// This constructor allows for the customization of the update request upon creation.
        /// </summary>
        /// <param name="index">The index name containing the document to update.</param>
        /// <param name="document">The dictionary containing updated field values for the document.</param>
        /// <param name="id">The unique identifier of the document to be updated.</param>
        /// <param name="query">The optional query criteria for selecting the document(s) to update.</param>
        public UpdateRequest(string index, Dictionary<string, object> document, long id = 0, Dictionary<string, object>? query = null)
        {
            Index = index;
            Id = id;
            Document = document;
            Query = query;
        }
    }
}
