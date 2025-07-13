using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class Cart : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public required string ProductItemId { get; set; }
        [ForeignKey(nameof(ProductItemId))]
        public virtual ProductItem ProductItem { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public required string UserId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
