using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class ResponseOrderDetail
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("purhcaseMethod")]
        public string? PurchaseMethod { get; set; }
        [JsonPropertyName("dateOfPurchase")]
        public DateTime? DateOfPurchase { get; set; }
        [JsonPropertyName("dateOfDelivery")]
        public DateTime? DateOfDelivery { get; set; }
        [JsonPropertyName("dateOfShipping")]
        public DateTime? DateOfShipping { get; set; }
    }
}
