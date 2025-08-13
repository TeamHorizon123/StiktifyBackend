namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseOrderDetail : BaseResponse
    {
        public string? PurchaseMethod { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public DateTime DateOfShipping { get; set; }
    }
}
