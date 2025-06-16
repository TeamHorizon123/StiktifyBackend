using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Requests
{
    public class RequestCreateOption
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;

        [Required]
        public string Image { get; set; } = default!;

        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int Quantity { get; set; }

        [StringLength(50)]
        public string Attribute { get; set; } = default!;

        [StringLength(50)]
        public string Value { get; set; } = default!;
    }

    public class RequestUpdateOption : RequestCreateOption
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
