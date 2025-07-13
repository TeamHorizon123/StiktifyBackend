using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreatePaymentRefund
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to 0.")]
        public double Amount { get; set; }

        [Required]
        public string Reason { get; set; } = default!;

        public DateTime RefundAt { get; set; }
    }
}
