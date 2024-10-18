using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the response returned from the ManticoreSearch engine after executing a bulk operation.
    /// This class contains details about the success or failure of individual items in the bulk request,
    /// as well as overall statistics regarding the processing of the bulk operation.
    /// </summary>
    public class BulkResponse
    {
        /// <summary>
        /// Gets or sets the list of items processed in the bulk operation.
        /// Each item represents an individual operation (insert, update, delete) and its result,
        /// providing detailed information on the success or failure of each operation within the bulk request.
        /// </summary>
        [JsonProperty("items")]
        public List<BulkItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the current line number in the bulk operation being processed.
        /// This property helps identify which line of the bulk request is currently being executed,
        /// aiding in debugging and logging efforts.
        /// </summary>
        [JsonProperty("current_line")]
        public int CurrentLine { get; set; }

        /// <summary>
        /// Gets or sets the number of lines that were skipped during the bulk operation.
        /// This property provides insight into how many operations were ignored,
        /// which can occur due to various reasons such as validation failures or preconditions not being met.
        /// </summary>
        [JsonProperty("skipped_lines")]
        public int SkippedLines { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether any errors occurred during the bulk operation.
        /// This property allows consumers of the response to quickly check if there were issues
        /// that require attention or further investigation.
        /// </summary>
        [JsonProperty("errors")]
        public bool Errors { get; set; }

        /// <summary>
        /// Gets or sets a string describing the error that occurred, if any.
        /// This property provides a detailed message about the nature of the error,
        /// enabling developers to understand the cause and address it accordingly.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
