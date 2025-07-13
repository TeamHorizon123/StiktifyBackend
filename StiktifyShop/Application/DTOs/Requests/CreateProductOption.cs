using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateProductOption
    {
        [StringLength(32)]
        public string? ProductId { get; set; }
        [Required]
        public string Image { get; set; } = default!;
        [StringLength(50)]
        public string? Color { get; set; }
        [StringLength(50)]
        public string? Type { get; set; } = "None";

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
