using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PaymentRefund : BaseEntity
    {
        [ForeignKey(nameof(Id))]
        public virtual Payment Payment { get; set; } = default!;

        [Column(TypeName = "money")]
        public double Amount { get; set; }

        [Required]
        public string Reason { get; set; } = default!;

        [Column(TypeName = "timestamp")]
        public DateTime RefundAt { get; set; }
    }
}
