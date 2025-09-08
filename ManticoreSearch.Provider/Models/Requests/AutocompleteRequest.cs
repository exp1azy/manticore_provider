using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Requests
{
    /// <summary>
    /// Represents a request for autocomplete functionality to Manticore Search server.
    /// Used to retrieve search suggestions based on partial input.
    /// </summary>
    public class AutocompleteRequest
    {
        /// <summary>
        /// Gets or sets the name of the table to perform autocomplete against.
        /// The table must be configured with appropriate autocomplete settings.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the partial query string for which to generate autocomplete suggestions.
        /// The autocomplete request will return suggestions based on the provided query string.
        /// </summary>
        [JsonProperty("query")]
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets additional options to customize the autocomplete behavior.
        /// Optional parameter that can be null if default behavior is acceptable.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public AutocompleteOptions? Options { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocompleteRequest"/> class.
        /// </summary>
        public AutocompleteRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocompleteRequest"/> class
        /// with the specified table, query, and optional options.
        /// </summary>
        /// <param name="table">The name of the table to search against.</param>
        /// <param name="query">The partial query string for autocomplete suggestions.</param>
        /// <param name="options">Optional parameters to customize autocomplete behavior.</param>
        public AutocompleteRequest(string table, string query, AutocompleteOptions? options = null)
        {
            Table = table;
            Query = query;
            Options = options;
        }
    }

    /// <summary>
    /// Contains optional parameters to customize the autocomplete behavior
    /// and fine-tune the suggestion generation process.
    /// </summary>
    public class AutocompleteOptions
    {
        /// <summary>
        /// Gets or sets the keyboard layouts to consider for autocomplete suggestions.
        /// </summary>
        [JsonProperty("layouts", NullValueHandling = NullValueHandling.Ignore)]
        public string Layouts { get; set; }

        /// <summary>
        /// Gets or sets the fuzziness level for matching suggestions.
        /// Higher values allow more typos and variations in the matching.
        /// </summary>
        [JsonProperty("fuzziness", NullValueHandling = NullValueHandling.Ignore)]
        public int? Fuzziness { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to prepend wildcards to the search pattern.
        /// When true, allows matches that end with the query string (prefix matching).
        /// When false or null, uses the default matching behavior.
        /// </summary>
        [JsonProperty("prepend", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Prepend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to append wildcards to the search pattern.
        /// When true, allows matches that start with the query string (suffix matching).
        /// When false or null, uses the default matching behavior.
        /// </summary>
        [JsonProperty("append", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Append { get; set; }

        /// <summary>
        /// Gets or sets the expansion length for generating suggestions.
        /// Defines how many additional characters to consider around the match.
        /// </summary>
        [JsonProperty("expansion_len", NullValueHandling = NullValueHandling.Ignore)]
        public int? ExpansionLen { get; set; }
    }
}
