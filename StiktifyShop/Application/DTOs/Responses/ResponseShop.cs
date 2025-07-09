namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseShop : BaseResponse
    {
        public string? UserId { get; set; }
        public string? ShopName { get; set; }
        public string? Description { get; set; }
        public string? AvartarUri { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public string? ShopType { get; set; }
    }
}
