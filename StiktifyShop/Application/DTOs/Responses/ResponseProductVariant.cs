namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseProductVariant : BaseResponse
    {
        public string? SizeId { get; set; }
        public virtual ResponseProductSize? Size { get; set; }
        public string? ProductOptionId { get; set; }
        public virtual ResponseProductOption? ProductOption { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
