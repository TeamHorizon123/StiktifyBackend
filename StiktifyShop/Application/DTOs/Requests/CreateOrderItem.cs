using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateOrderItem 
    {
        [StringLength(32)]
        public string? OrderId { get; set; }
        [Required]
        [StringLength(32)]
        public string ProductVariantId { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal zero.")]
        public int Quantity { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be greater than or equal zero.")]
        public double UnitPrice { get; set; }
        [Required]
        public required string ImageUri { get; set; }
    }
}
