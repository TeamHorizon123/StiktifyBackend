using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseCart
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        [JsonPropertyName("sizeColorId")]
        public string? SizeColoId { get; set; }
        [JsonPropertyName("sizeColor")]
        public ResponseSizeColor? SizeColor { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
