using StiktifyShop.Domain.Entity;
namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseProductRating : BaseResponse
    {
        public string? ProductId { get; set; }
        public virtual ResponseProduct? Product { get; set; }
        public string? OptionId { get; set; }
        public virtual ResponseProductOption? Option { get; set; }
        public string? VariantId { get; set; }
        public virtual ResponseProductVariant? Variant { get; set; }
        public string? OrderId { get; set; }
        public string? UserId { get; set; }
        public int Point { get; set; }
        public string? Content { get; set; }
        public List<string>? Files { get; set; }
    }
}
