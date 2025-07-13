using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateProductVariant
    {
        [StringLength(32)]
        public string? ProductOptionId { get; set; }
        [StringLength(32)]
        public string SizeId { get; set; } = default!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }
    }

    public class UpdateProductVariant:CreateProductVariant
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
