using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseOrderTracking
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("courierInfo")]
        public string? CourierInfo { get; set; }
        [JsonPropertyName("timeTracking")]
        public DateTime? TimeTracking { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
