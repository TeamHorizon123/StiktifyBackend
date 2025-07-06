using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string AddressId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;   
        [Required]
        [StringLength(32)]
        public string ProductItemId { get; set; } = default!;

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public double Price { get; set; }

        [Column(TypeName = "money")]
        public double ShippingFee { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = default!;

        public virtual ICollection<OrderTracking> OrderTrackings { get; set; } = default!;
    }
}
