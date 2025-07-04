using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseShop
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("shopName")]
        public string? ShopName { get; set; }
        [JsonPropertyName("shopType")]
        public string? ShopType { get; set; }
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
        [JsonPropertyName("isBanned")]
        public bool IsBanned { get; set; }
        [JsonPropertyName("rating")]
        public double Rating { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
