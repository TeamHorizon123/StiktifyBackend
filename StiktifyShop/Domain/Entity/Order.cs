using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

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
        public required string AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual UserAddress Address { get; set; } = default!;

        [Required]
        [StringLength(50)]
        [DefaultValue("Pending")]
        public required string Status { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double TotalAmount { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double ShippingFee { get; set; }

        [StringLength(150)]
        public string? Note { get; set; }

        //public virtual ICollection<OrderTracking> OrderTrackings { get; set; } = default!;
        //public virtual OrderDetail OrderDetail { get; set; } = default!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = default!;
        public virtual ProductRating? Rating { get; set; }
        public virtual Payment Payment { get; set; } = default!;
    }
}
