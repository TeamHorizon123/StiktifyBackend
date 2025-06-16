using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Requests
{
    public class RequestCreateShopRating
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string ShopId { get; set; } = default!;

        [Range(1, 5)]
        [DefaultValue(5)]
        public int Point { get; set; }

        [Required]
        public string Content { get; set; } = default!;
    }

    public class RequestUpdateShopRating : RequestCreateShopRating
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
