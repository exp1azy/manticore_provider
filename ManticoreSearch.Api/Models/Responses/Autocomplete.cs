using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the successful response from an autocomplete query in ManticoreSearch.
    /// </summary>
    public class AutocompleteSuccess
    {
        /// <summary>
        /// The total number of results returned from the autocomplete query.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// (Optional) An error message, if any error occurred during the request.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// (Optional) A warning message, if any issue occurred that does not prevent the request from succeeding.
        /// </summary>
        [JsonProperty("warning")]
        public string Warning { get; set; }

        /// <summary>
        /// A list of columns that describe the structure of the returned data.
        /// </summary>
        [JsonProperty("columns")]
        public List<Column> Columns { get; set; }

        /// <summary>
        /// (Optional) A list of data items containing the queries returned from the autocomplete request.
        /// </summary>
        [JsonProperty("data")]
        public List<DataItem>? Data { get; set; }
    }

    /// <summary>
    /// Represents a column in the autocomplete response, defining its structure and query type.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// The type of query associated with this column.
        /// </summary>
        [JsonProperty("query")]
        public QueryType Query { get; set; }
    }

    /// <summary>
    /// Represents the type of a query in the autocomplete response.
    /// </summary>
    public class QueryType
    {
        /// <summary>
        /// The specific type of query as a string.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Represents an individual data item returned from the autocomplete query, containing the query string.
    /// </summary>
    public class DataItem
    {
        /// <summary>
        /// The query string that was returned as part of the autocomplete suggestions.
        /// </summary>
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}
