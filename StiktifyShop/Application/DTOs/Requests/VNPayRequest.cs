namespace StiktifyShop.Application.DTOs.Requests
{
    public class VNPayRequest
    {
        public decimal Amount { get; set; }
        public string OrderId { get; set; } = default!;
        public string OrderInfo { get; set; } = default!;
        public string? BankCode { get; set; }
        public string? IpAddress { get; set; }
    }
}
