using Newtonsoft.Json;

namespace ManticoreSearch.Api.Models.Responses
{
    public class UpdateResponse : BaseResponse
    {
        public UpdateSuccess? Response { get; set; }

        public ErrorResponse? Error { get; set; }
    }

    public class UpdateSuccess
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
