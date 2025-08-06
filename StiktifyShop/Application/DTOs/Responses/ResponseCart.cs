using StiktifyShop.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Responses
{
    public class ResponseCart : BaseResponse
    {
        [Required]
        [StringLength(32)]
        public string? ProductId { get; set; }
        public virtual ResponseProduct? Product { get; set; }
        [Required]
        [StringLength(32)]
        public string? OptionId { get; set; }
        public virtual ResponseProductOption? Option { get; set; }
        [Required]
        [StringLength(32)]
        public string? VariantId { get; set; }
        public virtual ResponseProductVariant? Variant { get; set; }
        [Required]
        public string? ImageUri { get; set; }
        public string? UserId { get; set; }
        public int Quantity { get; set; }
    }
}
