using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductRating : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string OptionId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        public int Point { get; set; }

        [Required]
        public string Content { get; set; } = default!;

        [Column(TypeName = "text[]")]
        public ICollection<string>? Image { get; set; }
    }
}
