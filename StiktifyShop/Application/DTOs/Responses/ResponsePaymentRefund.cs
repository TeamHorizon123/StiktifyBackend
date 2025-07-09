namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponsePaymentRefund : BaseResponse
    {
        public virtual ResponsePayment? Payment { get; set; }
        public double Amount { get; set; }
        public string? Reason { get; set; }
        public DateTime RefundAt { get; set; }
    }
}
