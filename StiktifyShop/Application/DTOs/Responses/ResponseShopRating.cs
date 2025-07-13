using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseShopRating :BaseResponse
    {
        public string? UserId { get; set; } 
        public string? ShopId { get; set; }
        public virtual ResponseShop? Shop { get; set; } 
        public int Point { get; set; }
        public string? Content { get; set; } 
    }
}
