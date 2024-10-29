using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request to percolate a query against a set of registered queries in ManticoreSearch.
    /// This class encapsulates the details required to perform a percolation operation.
    /// </summary>
    public class PercolateRequest
    {
        /// <summary>
        /// Gets or sets the query to be percolated.
        /// This field is required and must contain a valid <see cref="PercolateRequestQuery"/> object.
        /// </summary>
        [JsonProperty("query")]
        public PercolateRequestQuery Query { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercolateRequest"/> class.
        /// </summary>
        public PercolateRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercolateRequest"/> class
        /// with the specified query to be percolated.
        /// </summary>
        /// <param name="query">The query to be percolated.</param>
        public PercolateRequest(PercolateRequestQuery query)
        {
            Query = query;
        }
    }
}
