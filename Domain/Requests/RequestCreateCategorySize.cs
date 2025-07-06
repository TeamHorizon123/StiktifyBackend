using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateCategorySize
    {
        [StringLength(32)]
        public string? CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string Size { get; set; } = default!;
    }

    public class RequestUpdateCategorySize : RequestCreateCategorySize
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}