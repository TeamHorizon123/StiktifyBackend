namespace Domain.Responses
{
    public class ResponseOrderTracking
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public string? Location { get; set; }
        public string? CourierInfo { get; set; }
        public DateTime? TimeTracking { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
