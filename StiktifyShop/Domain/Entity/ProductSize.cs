using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class ProductSize : BaseEntity
    {
        [StringLength(32)]
        public string? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }

        [Required]
        [StringLength(50)]
        public required string Size { get; set; }
    }
}
