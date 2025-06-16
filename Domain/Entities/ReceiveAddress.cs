using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table(name: "Address")]
    public class ReceiveAddress : BaseEntity
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
        public string PhoneReceive { get; set; } = default!;

        public string? Note { get; set; }
    }
}
