namespace Domain.Responses
{
    public class ResponsePaymentMethod
    {
        public string? Id { get; set; }
        public string Name { get; set; } = default!;

        public bool Enable { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
