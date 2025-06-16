using System.ComponentModel.DataAnnotations;

namespace Domain.Responses
{
    public class ResponseReceiveAddress
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Address { get; set; }
        public string? Receiver { get; set; }
        public string? PhoneReceive { get; set; }

        public string? Note { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
