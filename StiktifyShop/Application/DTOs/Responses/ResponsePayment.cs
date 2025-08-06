namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponsePayment : BaseResponse
    {
        public string? OrderId { get; set; } 
        public string? UserId { get; set; }
        public double Amount { get; set; }
        public string? Status { get; set; } 
        public string? TxnRef { get; set; } 
        public DateTime PaidAt { get; set; }
        public string? MethodId { get; set; } 
        public virtual ResponsePaymentMethod? PaymentMethod { get; set; } 
    }
}
