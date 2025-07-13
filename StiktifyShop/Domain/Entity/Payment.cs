using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Domain.Entity
{
    public class Payment : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string OrderId { get; set; } = default!;
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Column(TypeName = "money")]
        public double Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = default!;

        public string? TxnRef { get; set; } = default!;

        [Column(TypeName = "timestamp")]
        public DateTime PaidAt { get; set; }

        [Required]
        [StringLength(32)]
        public string MethodId { get; set; } = default!;

        [ForeignKey(nameof(MethodId))]
        public virtual PaymentMethod PaymentMethod { get; set; } = default!;
    }
}
