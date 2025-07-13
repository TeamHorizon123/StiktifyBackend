using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Domain.Entity
{
    public class OrderTracking : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public string OrderId { get; set; } = default!;
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; } = default!;

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

        [Column(TypeName = "timestamp")]
        public DateTime TimeTracking { get; set; }
    }
}
