using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class OrderItem : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string OrderId { get; set; } = default!;
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public required string ProductId { get; set; } = default!;
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public string ProductVariantId { get; set; } = default!;
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant ProductVariant { get; set; } = default!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public double UnitPrice { get; set; }
        [Required]
        public required string ImageUri { get; set; }
    }
}
