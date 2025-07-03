using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Requests
{
    public class RequestCreateOrder
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string AddressId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string SizeColor { get; set; } = default!;

        [Range(1, int.MaxValue)]
        [DefaultValue(0)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        [DefaultValue(0)]
        public double Price { get; set; }

        [Range(0, double.MaxValue)]
        [DefaultValue(0)]
        public double Discount { get; set; }

        [Range(0, double.MaxValue)]
        [DefaultValue(0)]
        public double ShippingFee { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = default!;

    }

    public class RequestUpdateOrder : RequestCreateOrder
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
