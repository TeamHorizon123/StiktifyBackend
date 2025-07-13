using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateCart
    {
        [Required]
        [StringLength(32)]
        public required string ProductItemId { get; set; }

        [Required]
        [StringLength(32)]
        public required string UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal 1.")]
        public int Quantity { get; set; }
    }

    public class UpdateCart: CreateCart
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
