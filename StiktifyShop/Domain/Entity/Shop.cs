using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class Shop : BaseEntity
    {
        [Required]
        [StringLength(32)]
        public required string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public required string ShopName { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public required string Description { get; set; }

        [Column(TypeName = "text")]
        public required string AvartarUri { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [Phone]
        [StringLength(15)]
        public required string Phone { get; set; }

        [StringLength(20)]
        public required string Status { get; set; }

        [Required]
        [StringLength(150)]
        public required string Address { get; set; }

        public string? ShopType { get; set; }

        public virtual ICollection<Product> Products { get; set; } = default!;
    }
}
