using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateShopRating
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string ShopId { get; set; } = default!;

        [Required]
        [Range(1, 5, ErrorMessage = "Point must be between 1 and 5.")]
        [DefaultValue(5)]
        public int Point { get; set; }

        [Required]
        public string Content { get; set; } = default!;
    }
}
