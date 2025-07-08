using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseCategorySize
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("categoryId")]
        public string? CategoryId { get; set; }
        [JsonPropertyName("category")]
        public ResponseCategory? Category { get; set; }
        [JsonPropertyName("size")]
        public string? Size { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }

}
