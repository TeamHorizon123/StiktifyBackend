using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PaymentMethod : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        public bool Enable { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = default!;
    }
}
