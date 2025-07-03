using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Cart : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string ProductItemId { get; set; } = default!;

        [DefaultValue(1)]
        public int Quantity { get; set; }
    }
}
