using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; } = default!;

        [StringLength(32)]
        public string? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual Category Parent { get; set; } = default!;
        public virtual ICollection<Category> Children { get; set; } = default!;
        public virtual ICollection<Product> Products { get; set; } = default!;
        public virtual ICollection<CategorySize> CategorySizes { get; set; } = default!;
    }
}
