using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateAddress
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(150)]
        public string Address { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Receiver { get; set; } = default!;

        [Required]
        [StringLength(15)]
        [Phone]
        public string PhoneReceive { get; set; } = default!;

        public string? Note { get; set; }
    }

    public class RequestUpdateAddress : RequestCreateAddress
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;

        public DateTime CreateAt { get; set; }
    }
}
