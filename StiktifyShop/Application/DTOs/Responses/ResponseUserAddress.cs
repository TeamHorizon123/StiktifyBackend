namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseUserAddress : BaseResponse
    {
        public string? UserId { get; set; }
        public string? Address { get; set; }
        public string? Receiver { get; set; }
        public string? PhoneReceive { get; set; }
        public string? Note { get; set; }
    }
}
