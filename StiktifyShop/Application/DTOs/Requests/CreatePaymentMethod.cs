using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreatePaymentMethod
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        [DefaultValue(true)]
        public bool Enable { get; set; }
    }

    public class UpdatePaymentMethod : CreatePaymentMethod
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
