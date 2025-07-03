using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateSizeColor
    {
        [Required]
        [StringLength(32)]
        public string OptionId { get; set; } = default!;
        [Required]
        [StringLength(32)]
        public string? OptionSize { get; set; } = default!;
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
    public class RequestUpdateSizeColor : RequestCreateSizeColor
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
