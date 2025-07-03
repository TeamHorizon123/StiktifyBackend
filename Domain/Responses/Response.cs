using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class Response
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
