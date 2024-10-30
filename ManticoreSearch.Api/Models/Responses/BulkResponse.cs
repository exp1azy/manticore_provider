using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class BulkResponse : BaseResponse
    {
        public BulkSuccess? Response { get; set; }

        public List<BulkError?> Error { get; set; }
    }

    public class BulkSuccess
    {
        [JsonProperty("items")]
        public List<BulkItem> Items { get; set; }

        [JsonProperty("current_line")]
        public int CurrentLine { get; set; }

        [JsonProperty("skipped_lines")]
        public int SkippedLines { get; set; }

        [JsonProperty("errors")]
        public bool Errors { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class BulkItem
    {
        [JsonProperty("bulk")]
        public BulkDetails Bulk { get; set; }
    }

    public class BulkDetails
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("deleted")]
        public int Deleted { get; set; }

        [JsonProperty("updated")]
        public int Updated { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }

    public class BulkError
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("warning")]
        public string Warning { get; set; }
    }
}
