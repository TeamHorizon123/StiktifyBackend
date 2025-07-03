using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ReponseProductItem
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("productId")]
        public string? ProductId { get; set; }
        [JsonPropertyName("product")]
        public ResponseProduct? Product { get; set; }
        [JsonPropertyName("size")]
        public string? Size { get; set; }
        [JsonPropertyName("color")]
        public string? Color { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
