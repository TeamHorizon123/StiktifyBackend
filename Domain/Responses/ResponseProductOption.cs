namespace Domain.Responses
{
    public class ResponseProductOption
    {
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }
        public string? Attribute { get; set; }
        public string? Value { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
