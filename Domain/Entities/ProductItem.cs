using Domain.Responses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductItem : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = default!;

        [StringLength(50)]
        public string? Size { get; set; }
        [StringLength(50)]
        public string? Color { get; set; }
        [StringLength(50)]
        public string? Type { get; set; }

        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public double Price { get; set; }
        [Column(TypeName = "text")]
        public string Image { get; set; } = default!;
    }
}
