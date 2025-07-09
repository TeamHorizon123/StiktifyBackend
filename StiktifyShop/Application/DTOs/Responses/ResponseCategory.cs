namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseCategory : BaseResponse
    {
        public string? Name { get; set; }
        public string? ParentId { get; set; }
        public virtual ICollection<ResponseCategory>? Children { get; set; }
    }
}
