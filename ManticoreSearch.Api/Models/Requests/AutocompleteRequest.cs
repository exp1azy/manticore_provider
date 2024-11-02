using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Requests
{
    /// <summary>
    /// Represents a request for the autocomplete feature, containing the necessary parameters 
    /// to retrieve autocomplete suggestions from the specified index.
    /// </summary>
    public class AutocompleteRequest
    {
        /// <summary>
        /// Gets or sets the name of the index (table) to search for autocomplete suggestions.
        /// </summary>
        [JsonProperty("table")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the query string to be used for generating autocomplete suggestions.
        /// </summary>
        [JsonProperty("query")]
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the options for customizing the autocomplete behavior, 
        /// such as layouts and fuzziness. This property can be null.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public AutocompleteOptions? Options { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocompleteRequest"/> class.
        /// </summary>
        public AutocompleteRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocompleteRequest"/> class 
        /// with specified index, query, and optional settings.
        /// </summary>
        /// <param name="index">The name of the index to search.</param>
        /// <param name="query">The query string for autocomplete suggestions.</param>
        /// <param name="options">Optional settings for the autocomplete request.</param>
        public AutocompleteRequest(string index, string query, AutocompleteOptions? options = null)
        {
            Index = index;
            Query = query;
            Options = options;
        }
    }

    /// <summary>
    /// Represents options for customizing the autocomplete behavior, such as layout preferences, 
    /// fuzziness, and string expansion options.
    /// </summary>
    public class AutocompleteOptions
    {
        /// <summary>
        /// Gets or sets the layout options for displaying autocomplete suggestions. This property can be null.
        /// </summary>
        [JsonProperty("layouts", NullValueHandling = NullValueHandling.Ignore)]
        public string Layouts { get; set; }

        /// <summary>
        /// Gets or sets the fuzziness level for matching the autocomplete suggestions. 
        /// This property can be null.
        /// </summary>
        [JsonProperty("fuzziness", NullValueHandling = NullValueHandling.Ignore)]
        public int? Fuzziness { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the autocomplete suggestions should 
        /// prioritize results that start with the query. This property can be null.
        /// </summary>
        [JsonProperty("prepend", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Prepend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the autocomplete suggestions should 
        /// include results that end with the query. This property can be null.
        /// </summary>
        [JsonProperty("append", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Append { get; set; }

        /// <summary>
        /// Gets or sets the length of the expansion for the autocomplete suggestions. 
        /// This property can be null.
        /// </summary>
        [JsonProperty("expansion_len", NullValueHandling = NullValueHandling.Ignore)]
        public int? ExpansionLen { get; set; }
    }
}
