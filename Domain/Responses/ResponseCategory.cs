using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseCategory
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("parentId")]
        public string? ParentId { get; set; }
        [JsonPropertyName("children")]
        public IEnumerable<ResponseCategory> ?Children { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
