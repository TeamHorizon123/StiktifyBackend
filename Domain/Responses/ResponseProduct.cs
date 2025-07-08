using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseProduct
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("shopId")]
        public string? ShopId { get; set; }
        [JsonPropertyName("shop")]
        public ResponseShop? Shop { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("ratingPoint")]
        public double RatingPoint { get; set; }
        [JsonPropertyName("ratingTurn")]
        public int RatingTurn { get; set; }
        [JsonPropertyName("order")]
        public int Order { get; set; }
        [JsonPropertyName("rangePrice")]
        public string? RangePrice { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
