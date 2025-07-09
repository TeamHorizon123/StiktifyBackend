namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseOrderTracking : BaseResponse
    {
        public string? OrderId { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public string? Location { get; set; }
        public string? CourierInfo { get; set; }
        public DateTime TimeTracking { get; set; }
    }
}
