using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateProduct
    {
        [Required]
        [StringLength(32)]
        public string ShopId { get; set; } = default!;
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!;
        [Required]
        public string ImageUri { get; set; } = default!;
        [DefaultValue(false)]
        public bool IsHidden { get; set; }
        public string CategoryId { get; set; } = default!;
        public List<CreateProductOption> Options { get; set; } = new();
    }

    public class UpdateProduct : CreateProduct
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
