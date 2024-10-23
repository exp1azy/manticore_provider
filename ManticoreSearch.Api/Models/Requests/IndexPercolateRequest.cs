using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to index a percolation query in ManticoreSearch.
    /// This class encapsulates the details required to perform an indexing operation for queries.
    /// </summary>
    public class IndexPercolateRequest
    {
        /// <summary>
        /// Gets or sets the percolation query to be indexed.
        /// This field is required and must contain a valid query object.
        /// The query will be evaluated against incoming documents to determine matches.
        /// </summary>
        [JsonProperty("query")]
        [Required(ErrorMessageResourceType = typeof(ModelError), ErrorMessageResourceName = "QueryRequired")]
        public object Query { get; set; }

        /// <summary>
        /// Gets or sets additional filters that can be applied when indexing the percolation query.
        /// This property is required and can be used to narrow down the scope of the query 
        /// by applying specific conditions or constraints.
        /// </summary>
        [JsonProperty("filters")]
        public string Filters { get; set; } = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexPercolateRequest"/> class.
        /// </summary>
        public IndexPercolateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexPercolateRequest"/> class
        /// with the specified percolation query.
        /// </summary>
        /// <param name="query">The percolation query to be indexed.</param>\
        /// <param name="filters">Filters that can be applied when indexing the percolation query.</param>
        public IndexPercolateRequest(object query, string filters)
        {
            Query = query;
            Filters = filters;
        }
    }
}
