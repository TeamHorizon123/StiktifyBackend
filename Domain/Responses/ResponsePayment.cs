namespace Domain.Responses
{
    public class ResponsePayment
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? UserId { get; set; }
        public double Amount { get; set; }
        public string? Status { get; set; }
        public string? TxnRef { get; set; }
        public DateTime? PaidAt { get; set; }
        public string? MethodId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
