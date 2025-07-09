namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseProductOption : BaseResponse
    {
        public string? ProductId { get; set; }
        public virtual ResponseProduct? Product { get; set; }
        public string? Image { get; set; }
        public string? Color { get; set; }
        public string? Type { get; set; }
        public double? Price { get; set; }

        public int? Quantity { get; set; }
        public List<ResponseProductVariant>? ProductVariants { get; set; }
    }
}
