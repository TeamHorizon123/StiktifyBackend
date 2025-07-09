namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponsePaymentMethod : BaseResponse
    {
        public string? Name { get; set; }
        public bool Enable { get; set; }
    }
}
