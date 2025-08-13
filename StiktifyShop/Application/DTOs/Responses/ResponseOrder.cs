namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseOrder : BaseResponse
    {
        public string? UserId { get; set; }
        public string? ShopId { get; set; }
        public ResponseShop? Shop { get; set; }
        public string? AddressId { get; set; }
        public ResponseUserAddress? Address { get; set; }
        public string? Status { get; set; }
        public double Total { get; set; }
        public double ShippingFee { get; set; }
        public string? Note { get; set; }
        public IEnumerable<ResponseOrderTracking>? OrderTrackings { get; set; }
        public IEnumerable<ResponseOrderItem>? OrderItems{ get; set; }
    }
}
