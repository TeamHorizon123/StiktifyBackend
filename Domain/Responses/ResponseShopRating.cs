using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseShopRating
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        [JsonPropertyName("shopId")]
        public string? ShopId { get; set; }
        [JsonPropertyName("point")]
        public int Point { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
