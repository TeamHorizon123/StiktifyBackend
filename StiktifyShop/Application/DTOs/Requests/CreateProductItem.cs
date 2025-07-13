using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateProductItem
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;

        [StringLength(50)]
        public string? Size { get; set; }
        [StringLength(50)]
        public string? Color { get; set; }
        [StringLength(50)]
        public string? Type { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public string Image { get; set; } = default!;
    }

    public class UpdateProducItem : CreateProductItem
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
