using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class Order : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public required string UserId { get; set; }
        [Required]
        [StringLength(32)]
        public required string ShopId { get; set; }
        [ForeignKey(nameof(ShopId))]
        public virtual Shop Shop { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public required string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public required string ProductItemId { get; set; }
        [ForeignKey(nameof(ProductItemId))]
        public virtual ProductItem ProductItem { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public required string AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual UserAddress Address { get; set; } = default!;

        [Required]
        [StringLength(50)]
        [DefaultValue("Pending")]
        public required string Status { get; set; }

        [DefaultValue(1)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double Price { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double ShippingFee { get; set; }

        public virtual ICollection<OrderTracking> OrderTrackings { get; set; } = default!;
    }
}
