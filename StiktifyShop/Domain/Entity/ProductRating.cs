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
        public string ProductItemId { get; set; } = default!;

        [ForeignKey(nameof(ProductItemId))]
        public virtual ProductItem ProductItem { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        public int Point { get; set; }

        [Required]
        public string Content { get; set; } = default!;

        [Column(TypeName = "text[]")]
        public List<string>? Files { get; set; }
    }
}
