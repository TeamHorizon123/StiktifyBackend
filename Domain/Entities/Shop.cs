using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Shop : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string ShopName { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Location { get; set; } = default!;

        [Required]
        public string Avatar { get; set; } = default!;

        [StringLength(25)]
        public string? ShopType { get; set; }

        [DefaultValue(false)]
        public bool IsBanned { get; set; }

        public virtual ICollection<ShopRating> ShopRatings { get; set; } = default!;
    }
}
