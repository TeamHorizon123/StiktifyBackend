using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string ShopId { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        public string Thumbnail { get; set; } = default!;

        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; } = default!;

        [Column(TypeName = "money")]
        public double Price { get; set; }

        [Column(TypeName = "numeric(3,2)")]
        public double Discount { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = default!;
        public virtual ICollection<ProductOption> Options { get; set; } = default!;
    }
}
