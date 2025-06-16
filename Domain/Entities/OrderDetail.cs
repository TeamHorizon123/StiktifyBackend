using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        [ForeignKey(nameof(Id))]
        public virtual Order Order { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string PurchaseMethod { get; set; } = default!;

        [Column(TypeName = "timestamp")]
        public DateTime DateOfPurchase { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime DateOfDelivery { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime DateOfShipping { get; set; }
    }
}
