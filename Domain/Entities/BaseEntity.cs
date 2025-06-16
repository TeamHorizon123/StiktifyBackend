using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        [StringLength(32)]
        [Column(name: "_id")]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Column(TypeName = "timestamp")]
        public DateTime CreateAt { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}
