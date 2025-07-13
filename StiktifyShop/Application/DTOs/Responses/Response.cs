namespace StiktifyShop.Application.DTOs.Responses
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = default!;
        public object? Data { get; set; }
    }
}
