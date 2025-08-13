using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateOrderTracking
    {
        [StringLength(32)]
        public string? OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = default!;

        [Required]
        [StringLength(150)]
        public string Message { get; set; } = default!;

        [Required]
        [StringLength(150)]
        public string Location { get; set; } = default!;

        [StringLength(100)]
        public string? CourierInfo { get; set; }
        public DateTime TimeTracking { get; set; }
    }

    public class UpdateOrderTracking : CreateOrderTracking
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }   
}
