namespace Domain.Responses
{
    public class ResponseProductRating
    {
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public string? OptionId { get; set; }
        public string? UserId { get; set; }
        public int Point { get; set; }
        public string? Content { get; set; }
        public ICollection<string>? Image { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
