using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Domain.Entities;

namespace Domain.Requests
{
    public class RequestCreateOption
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;

        [Required]
        public string Image { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Color { get; set; } = default!;

        [StringLength(50)]
        public string Type { get; set; } = default!;

        public ICollection<CategorySize>? CategorySizes { get; set; }
    }

    public class RequestUpdateOption : RequestCreateOption
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }

}
