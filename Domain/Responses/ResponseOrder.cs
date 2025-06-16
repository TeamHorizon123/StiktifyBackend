namespace Domain.Responses
{
    public class ResponseOrder
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? AddressId { get; set; }
        public string? ProductId { get; set; }
        public string? OptionId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double ShippingFee { get; set; }
        public string? Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
