using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponsePaymentRefund
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
        [JsonPropertyName("refundAt")]
        public DateTime? RefundAt { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
