using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseProductRating
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("productId")]
        public string? ProductId { get; set; }
        [JsonPropertyName("productItemId")]
        public string? ProductItemId { get; set; }
        [JsonPropertyName("productItem")]
        public ResponseProductItem? ProductItem { get; set; }
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        [JsonPropertyName("point")]
        public int Point { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }
        [JsonPropertyName("image")]
        public ICollection<string>? Image { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
