using ManticoreSearch.Api.Resources;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents the query details for a percolation request in ManticoreSearch.
    /// This class encapsulates the specific criteria that will be evaluated during the percolation process.
    /// </summary>
    public class PercolateRequestQuery
    {
        /// <summary>
        /// Gets or sets the percolate criteria.
        /// This property is required and must contain a valid percolation query object.
        /// The structure of the percolate object should conform to ManticoreSearch requirements for percolation queries.
        /// </summary>
        [JsonProperty("percolate")]
        public object Percolate { get; set; }
    }
}
