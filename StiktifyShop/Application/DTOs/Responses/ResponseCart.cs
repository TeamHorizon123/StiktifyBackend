using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseCart : BaseResponse
    {
        public string? ProductItemId { get; set; }
        public ResponseProductItem? ProductItem { get; set; }
        public string? UserId { get; set; }
        public int Quantity { get; set; }
    }
}
