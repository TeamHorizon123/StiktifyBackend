using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseCart
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        [JsonPropertyName("productItemId")]
        public string? ProductItemId { get; set; }
        [JsonPropertyName("productItem")]
        public ResponseProductItem? ProductItem { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
