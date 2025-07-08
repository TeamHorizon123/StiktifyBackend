using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseProductVarriant
    {
        [JsonPropertyName("productOptionId")]
        public string? ProductOptionId { get; set; }
        [JsonPropertyName("productOption")]
        public ResponseProductOption? ProductOption { get; set; }
        [JsonPropertyName("sizeId")]
        public string? SizeId{ get; set; }
        [JsonPropertyName("size")]
        public ResponseCategorySize? Size { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
    }
}
