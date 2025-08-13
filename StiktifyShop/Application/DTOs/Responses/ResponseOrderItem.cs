namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseOrderItem : BaseResponse
    {
        public string? OrderId { get; set; }
        public string? ProductVariantId { get; set; }
        public string? ProductId { get; set; }
        public ResponseProduct? Product { get; set; }
        public ResponseProductVariant? ProductVariant { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string? ImageURI { get; set; }
    }
}
