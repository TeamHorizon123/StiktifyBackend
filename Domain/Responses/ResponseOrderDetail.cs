namespace Domain.Responses
{
    public class ResponseOrderDetail
    {
        public string? Id { get; set; }
        public string? PurchaseMethod { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public DateTime? DateOfShipping { get; set; }
    }
}
