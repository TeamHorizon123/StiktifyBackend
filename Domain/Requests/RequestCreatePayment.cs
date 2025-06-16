using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Requests
{
    public class RequestCreatePayment
    {
        [Required]
        [StringLength(32)]
        public string OrderId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Range(0, double.MaxValue)]
        [DefaultValue(0)]
        public double Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = default!;

        public string? TxnRef { get; set; } = default!;

        public DateTime PaidAt { get; set; }

        [Required]
        [StringLength(32)]
        public string MethodId { get; set; } = default!;
    }

    public class RequestUpdatePayment : RequestCreatePayment
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
