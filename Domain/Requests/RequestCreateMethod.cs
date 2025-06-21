using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateMethod
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        public bool Enable { get; set; }
    }

    public class RequestUpdateMethod : RequestCreateMethod
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
