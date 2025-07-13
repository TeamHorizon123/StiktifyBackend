using Microsoft.AspNetCore.Authentication;
using StiktifyShop.Application.DTOs.Requests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [StringLength(32)]
        public string? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual Category? Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; } = default!;
        public virtual ICollection<Product> Products { get; set; } = default!;
    }


}
