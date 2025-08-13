using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Domain.Entity
{
    public class PaymentMethod :BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        public bool Enable { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = default!;
    }
}
