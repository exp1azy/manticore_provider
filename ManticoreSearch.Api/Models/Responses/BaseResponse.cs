namespace ManticoreSearch.Api.Models.Responses
{
    public class BaseResponse
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public bool IsSuccess { get; set; }

        public string RawResponse { get; set; }
    }
}
