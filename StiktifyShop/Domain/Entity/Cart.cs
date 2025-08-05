using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class Cart : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public required string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public required string OptionId { get; set; }
        [ForeignKey(nameof(OptionId))]
        public virtual ProductOption Option { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public required string VariantId { get; set; }
        [ForeignKey(nameof(VariantId))]
        public virtual ProductVariant Variant { get; set; } = default!;
        [Required]
        public required string ImageUri { get; set; }

        [Required]
        [StringLength(32)]
        public required string UserId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
