using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponsePaymentMethod
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
        [JsonPropertyName("enable")]
        public bool Enable { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
