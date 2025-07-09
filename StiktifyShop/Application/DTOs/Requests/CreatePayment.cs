using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreatePayment
    {
        [Required]
        [StringLength(32)]
        public string OrderId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to 0.")]
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

    public class UpdatePayment :CreatePayment
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
