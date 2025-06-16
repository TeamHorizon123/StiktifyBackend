using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ShopRating : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string ShopId { get; set; } = default!;
        [ForeignKey(nameof(ShopId))]
        public virtual Shop Shop { get; set; } = default!;

        [Required]
        [DefaultValue(5)]
        public int Point { get; set; }

        [Required]
        public string Content { get; set; } = default!;
    }
}
