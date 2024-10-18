using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class BulkSuccessResponse
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
}
