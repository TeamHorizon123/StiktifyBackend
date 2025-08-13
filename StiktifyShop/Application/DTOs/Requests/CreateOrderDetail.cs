using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateOrderDetail
    {
        [StringLength(32)]
        public string? Id { get; set; }
        [Required]
        [StringLength(32)]
        public string PurchaseMethod { get; set; } = default!;

        public DateTime? DateOfPurchase { get; set; }

        public DateTime? DateOfDelivery { get; set; }

        public DateTime? DateOfShipping { get; set; }
    }
}
