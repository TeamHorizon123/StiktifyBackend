using System.ComponentModel.DataAnnotations;

namespace Domain.Responses
{
    public class ResponseCart
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? OptionId { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
