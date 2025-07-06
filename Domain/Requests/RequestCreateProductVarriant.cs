using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateProductVarriant
    {
        [Required]
        [StringLength(32)]
        public string ProductOptionId { get; set; } = default!;
        [StringLength(50)]
        public string SizeId { get; set; } = default!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
    public class RequestUpdateProductVarriant : RequestCreateProductVarriant
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
