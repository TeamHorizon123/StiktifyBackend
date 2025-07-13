using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public required string ShopId { get; set; }
        [ForeignKey(nameof(ShopId))]
        public virtual Shop Shop { get; set; } = default!;
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public required string Description { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public required string ImageUri { get; set; }
        [DefaultValue(false)]
        public bool IsHidden { get; set; }

        public required string CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = default!;

        public virtual ICollection<ProductOption> ProductOptions { get; set; } = default!;
        public virtual ICollection<ProductRating> ProductRatings { get; set; } = default!;
        public virtual ICollection<ProductItem> ProductItems { get; set; } = default!;
    }
}
