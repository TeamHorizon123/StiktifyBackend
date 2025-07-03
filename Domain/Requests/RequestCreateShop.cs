using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Requests
{
    public class RequestCreateShop
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
    }

    public class RequestUpdateShop : RequestCreateShop
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
