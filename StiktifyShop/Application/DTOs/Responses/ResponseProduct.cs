namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseProduct : BaseResponse
    {
        public string? ShopId { get; set; }
        public virtual ResponseShop? Shop { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUri { get; set; }
        public bool IsHidden { get; set; }
        public string? CategoryId { get; set; }
        public string? PriceRange { get; set; }
        public double? Price { get; set; }
        public double AveragePoint { get; set; }
        public int RateTurn { get; set; }
        public int Order { get; set; }
        public virtual ResponseCategory? Category { get; set; }
        public virtual ICollection<ResponseProductOption>? ProductOptions { get; set; }
    }
}
