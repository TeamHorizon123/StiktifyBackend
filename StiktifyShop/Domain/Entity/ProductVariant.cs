using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class ProductVariant : BaseEntity
    {
        [StringLength(32)]
        public string SizeId { get; set; } = default!;
        [ForeignKey(nameof(SizeId))]
        public virtual ProductSize ProductSize { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string ProductOptionId { get; set; } = default!;
        [ForeignKey(nameof(ProductOptionId))]
        public virtual ProductOption ProductOption { get; set; } = default!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double Price { get; set; }

        public virtual ICollection<Cart> Carts { get; set; } = default!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = default!;
    }
}
