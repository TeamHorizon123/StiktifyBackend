using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
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
    }

    public class ProductItem : ProductOption
    {
        [StringLength(50)]
        public string? Size { get; set; }
    }
}
