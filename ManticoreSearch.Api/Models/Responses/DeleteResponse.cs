using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    /// <summary>
    /// Represents the response structure for delete operations in ManticoreSearch API.
    /// Inherits from <see cref="ManticoreResponse{DeleteSuccess, ErrorResponse}"/> to handle both success and error responses.
    /// </summary>
    public class DeleteResponse : ManticoreResponse<DeleteSuccess, ErrorResponse>
    {
        /// <summary>
        /// Contains the details of the response if the delete operation was performed via a query.
        /// This is optional and may be null if the operation was not a query-based deletion.
        /// </summary>
        public DeleteByQuerySuccess? ResponseIfQuery { get; set; }
    }

    /// <summary>
    /// Represents the details of a successful delete operation in ManticoreSearch.
    /// </summary>
    public class DeleteSuccess
    {
        /// <summary>
        /// The name of the table from which the record was deleted.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// Indicates whether the specified record was found and deleted.
        /// </summary>
        [JsonProperty("found")]
        public bool Found { get; set; }

        /// <summary>
        /// The unique identifier of the deleted record.
        /// </summary>
        [JsonProperty("_id")]
        public long Id { get; set; }

        /// <summary>
        /// The result message of the delete operation, indicating the status of the deletion.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Represents the details of a successful delete operation performed via a query in ManticoreSearch.
    /// </summary>
    public class DeleteByQuerySuccess
    {
        /// <summary>
        /// The name of the table from which records were deleted using a query.
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; }

        /// <summary>
        /// The total number of records that were deleted as a result of the query.
        /// </summary>
        [JsonProperty("deleted")]
        public int Deleted { get; set; }
    }
}
