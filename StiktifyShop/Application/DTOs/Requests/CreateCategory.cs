using System.ComponentModel.DataAnnotations;

namespace StiktifyShop.Application.DTOs.Requests
{
    public class CreateCategory
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [StringLength(32)]
        public string? ParentId { get; set; }
    }
    public class UpdateCategory : CreateCategory
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
