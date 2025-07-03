using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateCategory
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; } = default!;
        public string? ParentId { get; set; }
    }

    public class RequestUpdateCategory : RequestCreateCategory
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
