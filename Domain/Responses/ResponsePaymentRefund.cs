namespace Domain.Responses
{
    public class ResponsePaymentRefund
    {
        public string? Id { get; set; }
        public double Amount { get; set; }
        public string? Reason { get; set; }
        public DateTime? RefundAt { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
