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
        public string Image { get; set; } = default!;

        public int Quantity { get; set; }
        [StringLength(50)]
        public string Attribute { get; set; } = default!;
        [StringLength(50)]
        public string Value { get; set; } = default!;
    }
}
