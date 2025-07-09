using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateProductVarriant
    {
        [StringLength(32)]
        public string? ProductOptionId { get; set; }
        [StringLength(50)]
        public string SizeId { get; set; } = default!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
