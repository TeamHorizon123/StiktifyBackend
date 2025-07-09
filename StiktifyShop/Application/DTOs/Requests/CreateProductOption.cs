using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateProductOption
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;
        [Required]
        public string Image { get; set; } = default!;
        [StringLength(50)]
        public string Color { get; set; } = default!;
        [StringLength(50)]
        public string Type { get; set; } = default!;

        [Range(0, double.MaxValue)]
        public double? Price { get; set; }
        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }
        public List<CreateProductVariant> ProductVariants { get; set; } = new();
    }

    public class UpdateProductOption : CreateProductOption
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
