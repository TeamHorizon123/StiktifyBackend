using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateOrderDetail
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string PurchaseMethod { get; set; } = default!;
        public DateTime DateOfPurchase { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public DateTime DateOfShipping { get; set; }
    }

    public class RequestUpdateOrderDetail : RequestCreateOrderDetail { }
}
