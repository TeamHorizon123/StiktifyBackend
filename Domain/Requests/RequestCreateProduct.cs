using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Requests
{
    public class RequestCreateProduct
    {
        [Required]
        [StringLength(32)]
        public string ShopId { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        public string Thumbnail { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        [Range(0, double.MaxValue)]
        [DefaultValue(0)]
        public double Price { get; set; }

        [Range(0, 1)]
        [DefaultValue(0)]
        public double Discount { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [Required]
        public string[] CategoryId { get; set; } = default!;
    }

    public class RequestUpdateProduct : RequestCreateProduct
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
