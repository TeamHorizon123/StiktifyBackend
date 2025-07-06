using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductVarriant
    {
        [StringLength(32)]
        public string SizeId { get; set; } = default!;
        [ForeignKey(nameof(SizeId))]
        public virtual CategorySize Size { get; set; } = default!;

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
    }
}
