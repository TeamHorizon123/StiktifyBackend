using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateProductItem
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
        [Range(0, int.MaxValue)]
        public double Price { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public string Image { get; set; } = default!;
    }

    public class RequestUpdateProductItem : RequestCreateProductItem
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
