using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StiktifyShop.Domain.Entity
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "timestamp")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
