namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseProductSize : BaseResponse
    {
        public string? CategoryId { get; set; }
        public required string Size { get; set; }
    }
}
