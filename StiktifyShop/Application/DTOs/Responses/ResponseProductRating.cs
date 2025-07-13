using StiktifyShop.Domain.Entity;
namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseProductRating : BaseResponse
    {
        public string? ProductId { get; set; }
        public virtual ResponseProduct? Product { get; set; }
        public string? ProductItemId { get; set; }
        public virtual ResponseProductItem? ProductItem { get; set; }
        public string? UserId { get; set; }
        public int Point { get; set; }
        public string? Content { get; set; }
        public List<string>? Files { get; set; }
    }
}
