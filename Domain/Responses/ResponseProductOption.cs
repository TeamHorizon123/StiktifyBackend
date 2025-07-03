using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseProductOption
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("productId")]
        public string? ProductId { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
        [JsonPropertyName("color")]
        public string? Color { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
