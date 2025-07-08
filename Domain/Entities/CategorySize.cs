using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CategorySize : BaseEntity
    {
        [StringLength(32)]
        public string? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
        [StringLength(100)]
        public string Size { get; set; } = default!;

        public virtual ICollection<ProductVarriant> ProductVarriants { get; set; } = default!;
    }
}
