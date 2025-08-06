using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateProducRating
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string OptionId { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public string VariantId { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public string OrderId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Range(1, 5)]
        public int Point { get; set; }

        [Required]
        public string Content { get; set; } = default!;

        public List<string>? Files { get; set; }
    }
}
