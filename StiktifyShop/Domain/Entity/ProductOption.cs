using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class ProductOption : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = default!;
        [Required]
        [Column(TypeName = "text")]
        public string Image { get; set; } = default!;
        [StringLength(50)]
        public string Color { get; set; } = default!;
        [StringLength(50)]
        public string Type { get; set; } = default!;

        [Column(TypeName = "money")]
        public double? Price { get; set; }

        public int? Quantity { get; set; }
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = default!;
    }
}
