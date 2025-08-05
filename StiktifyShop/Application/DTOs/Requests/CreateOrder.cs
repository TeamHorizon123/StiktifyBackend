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
        public string AddressId { get; set; } = default!;

        [Required]
        [StringLength(50)]
        [DefaultValue("pending")]
        public string Status { get; set; } = default!;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "TotalAmount must be greater than or equal zero.")]
        public double TotalAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Shipping fee must be greater than or equal zero.")]
        public double ShippingFee { get; set; }
        [StringLength(150)]
        public string? Note { get; set; }
        public ICollection<CreateOrderItem> OrderItems { get; set; } = default!;
    }

    public class UpdateOrder : CreateOrder
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }

}
