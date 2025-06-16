namespace Domain.Responses
{
    public class ResponseShopRating
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? ShopId { get; set; }
        public int Point { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
