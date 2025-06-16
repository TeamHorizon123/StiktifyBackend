using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Requests
{
    public class RequestCreateRefund
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;

        [Range(0, double.MaxValue)]
        [DefaultValue(0)]
        public double Amount { get; set; }

        [Required]
        public string Reason { get; set; } = default!;

        public DateTime RefundAt { get; set; }
    }
}
