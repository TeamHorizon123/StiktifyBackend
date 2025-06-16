namespace Domain.Responses
{
    public class ResponseProduct
    {
        public string? Id { get; set; }
        public string? ShopId { get; set; }
        public string? Name { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double Rating { get; set; }
        public int Order { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
