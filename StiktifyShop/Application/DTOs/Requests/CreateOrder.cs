using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateOrder
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public string ShopId { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public string ProductItemId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string AddressId { get; set; } = default!;

        [Required]
        [StringLength(50)]
        [DefaultValue("Pending")]
        public string Status { get; set; } = default!;

        [DefaultValue(1)]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal zero.")]
        public double Price { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Shipping fee must be greater than or equal zero.")]
        public double ShippingFee { get; set; }
    }

    public class UpdateOrder : CreateOrder
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }

}
