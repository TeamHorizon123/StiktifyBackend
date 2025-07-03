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
        public int Color { get; set; }

        [StringLength(50)]
        public string Type { get; set; } = default!;
    }

    public class RequestCreateOptionSize
    {
        [StringLength(50)]
        public string SizeValue { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string CategoryMajor { get; set; } = default!;
    }

    public class RequestUpdateOption : RequestCreateOption
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }

    public class RequestUpdateOptionSize : RequestCreateOptionSize
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
