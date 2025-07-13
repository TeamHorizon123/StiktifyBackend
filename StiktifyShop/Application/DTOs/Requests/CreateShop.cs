using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateShop
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string ShopName { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;
        public string AvatarUri { get; set; } = default!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = default!;

        [Phone]
        [StringLength(15)]
        public string Phone { get; set; } = default!;

        [StringLength(20)]
        public string Status { get; set; } = default!;

        [Required]
        [StringLength(150)]
        public string Address { get; set; } = default!;

        public string? ShopType { get; set; }

    }

    public class UpdateShop:CreateShop
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
