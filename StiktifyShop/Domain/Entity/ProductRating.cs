using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class ProductRating : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string OptionId { get; set; } = default!;
        [ForeignKey(nameof(OptionId))]
        public virtual ProductOption Option { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string VariantId { get; set; } = default!;
        [ForeignKey(nameof(VariantId))]
        public virtual ProductVariant Variant { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        public int Point { get; set; }

        [Required]
        public string Content { get; set; } = default!;

        [Column(TypeName = "text[]")]
        public List<string>? Files { get; set; }

        [Required]
        [StringLength(32)]
        public string OrderId { get; set; } = default!;
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; } = default!;
    }
}
