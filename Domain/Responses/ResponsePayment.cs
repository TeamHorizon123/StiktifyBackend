using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponsePayment
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("txnRef")]
        public string? TxnRef { get; set; }
        [JsonPropertyName("paidAt")]
        public DateTime? PaidAt { get; set; }
        [JsonPropertyName("methodId")]
        public string? MethodId { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
