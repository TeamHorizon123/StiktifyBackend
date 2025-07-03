using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseReceiveAddress
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("receiver")]
        public string? Receiver { get; set; }
        [JsonPropertyName("phoneReceive")]
        public string? PhoneReceive { get; set; }
        [JsonPropertyName("note")]
        public string? Note { get; set; }
        [JsonPropertyName("createAt")]
        public DateTime? CreateAt { get; set; }
        [JsonPropertyName("updateAt")]
        public DateTime? UpdateAt { get; set; }
    }
}
