using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseSizeColor
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("optionId")]
        public string? OptionId { get; set; }
        [JsonPropertyName("productOption")]
        public ResponseProductOption? ProductOption { get; set; }
        [JsonPropertyName("optionSize")]
        public string? OptionSize { get; set; }
        [JsonPropertyName("size")]
        public ResponseOptionSize? Size { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }

    public class ResponseOptionSize
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("sizeValue")]
        public string? SizeValue { get; set; }
        [JsonPropertyName("categoryId")]
        public string? CategoryId { get; set; }
        [JsonPropertyName("categoryMajor")]
        public ResponseCategory? CategoryMajor { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
