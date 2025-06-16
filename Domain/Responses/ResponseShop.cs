using System.ComponentModel;

namespace Domain.Responses
{
    public class ResponseShop
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Location { get; set; }
        public string? ShopName { get; set; }
        public string? ShopType { get; set; }
        public string? Avatar { get; set; }
        public bool IsBanned { get; set; }
        public double Rating { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
