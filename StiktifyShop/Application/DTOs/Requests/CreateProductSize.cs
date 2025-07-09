using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateProductSize
    {
        [StringLength(32)]
        public string? CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Size { get; set; }
    }

    public class UpdateProductSize : CreateProductSize
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
