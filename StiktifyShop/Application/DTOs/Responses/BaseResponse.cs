namespace StiktifyShop.Application.DTOs.Responses
{
    public class BaseResponse
    {
        public string? Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
