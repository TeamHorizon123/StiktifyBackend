using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateCart
    {
        [Required]
        [StringLength(32)]
        public required string ProductId { get; set; }
        [Required]
        [StringLength(32)]

        public required string OptionId { get; set; }
        [Required]
        [StringLength(32)]
        public required string VariantId { get; set; }
        [Required]
        public required string ImageUri { get; set; }

        [Required]
        [StringLength(32)]
        public required string UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal 1.")]
        public int Quantity { get; set; }
    }

    public class UpdateCart : CreateCart
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }

    public class DeleteCart 
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
