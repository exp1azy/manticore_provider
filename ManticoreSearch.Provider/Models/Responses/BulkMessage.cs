using Newtonsoft.Json;

namespace ManticoreSearch.Provider.Models.Responses
{
    /// <summary>
    /// Represents the response message for a bulk operation in Manticore Search.
    /// </summary>
    public class BulkMessage
    {
        /// <summary>
        /// A list of individual items processed during the bulk operation.
        /// </summary>
        [JsonProperty("items")]
        public List<BulkItem> Items { get; set; }

        /// <summary>
        /// The current line number being processed in the bulk request.
        /// </summary>
        [JsonProperty("current_line")]
        public int CurrentLine { get; set; }

        /// <summary>
        /// The number of lines skipped during the bulk operation due to errors or other issues.
        /// </summary>
        [JsonProperty("skipped_lines")]
        public int SkippedLines { get; set; }

        /// <summary>
        /// Indicates whether any errors occurred during the bulk operation.
        /// </summary>
        [JsonProperty("errors")]
        public bool Errors { get; set; }

        /// <summary>
        /// An error message detailing the nature of the error, if any occurred.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }

    /// <summary>
    /// Represents an individual item in the bulk operation response, containing details about the operation.
    /// </summary>
    public class BulkItem
    {
        /// <summary>
        /// The details of the bulk operation for this item.
        /// </summary>
        [JsonProperty("bulk")]
        public BulkDetails? Bulk { get; set; }

        /// <summary>
        /// Details of an insert operation result for this item.
        /// </summary>
        [JsonProperty("insert")]
        public BulkErrorItem? Insert { get; set; }
        
        /// <summary>
        /// Details of a replace operation result for this item.
        /// </summary>
        [JsonProperty("replace")]
        public BulkErrorItem? Replace { get; set; }

        /// <summary>
        /// Details of an update operation result for this item.
        /// </summary>
        [JsonProperty("update")]
        public BulkErrorItem? Update { get; set; }

        /// <summary>
        /// Details of a delete operation result for this item.
        /// </summary>
        [JsonProperty("delete")]
        public BulkErrorItem? Delete { get; set; }
    }

    /// <summary>
    /// Represents the result of an individual bulk operation item with error details.
    /// </summary>
    public class BulkErrorItem
    {
        /// <summary>
        /// Error details for the bulk operation item.
        /// </summary>
        [JsonProperty("error")]
        public BulkErrorDetails Error { get; set; }

        /// <summary>
        /// HTTP status code for the operation.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
    }

    /// <summary>
    /// Represents detailed error information for a bulk operation item.
    /// </summary>
    public class BulkErrorDetails
    {
        /// <summary>
        /// Type of error that occurred during the bulk operation.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Name of the table where the error occurred.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }
    }

    /// <summary>
    /// Represents the details of an individual item processed during a bulk operation in Manticore Search.
    /// </summary>
    public class BulkDetails
    {
        /// <summary>
        /// The name of the table associated with the bulk operation.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// The unique identifier for the document that was processed.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// Indicates the creation status of the document: 1 if created, 0 if not created.
        /// </summary>
        [JsonProperty("created")]
        public int Created { get; set; }

        /// <summary>
        /// Indicates the deletion status of the document: 1 if deleted, 0 if not deleted.
        /// </summary>
        [JsonProperty("deleted")]
        public int Deleted { get; set; }

        /// <summary>
        /// Indicates the update status of the document: 1 if updated, 0 if not updated.
        /// </summary>
        [JsonProperty("updated")]
        public int Updated { get; set; }

        /// <summary>
        /// The result of the bulk operation, indicating success or failure.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// The HTTP status code returned from the bulk operation.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
