using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Requests
{
    public class RequestCreateRating
    {
        [Required]
        [StringLength(32)]
        public string ProductId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string ProductItemId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Range(1, 5)]
        [DefaultValue(5)]
        public int Point { get; set; }

        [Required]
        public string Content { get; set; } = default!;

        public List<string>? Image { get; set; }
    }

    public class RequestUpdateRating : RequestCreateRating
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
