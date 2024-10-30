using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class ModificationResponse : BaseResponse
    {
        public ModificationSuccess? Response { get; set; }

        public ModificationError? Error { get; set; }
    }

    public class ModificationSuccess
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("created")]
        public bool Created { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }

    public class ModificationError
    {
        [JsonProperty("error")]
        public ModificationErrorMessage Message { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }

    public class ModificationErrorMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("table")]
        public string? Table { get; set; }
    }
}
