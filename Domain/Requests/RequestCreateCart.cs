using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class RequestCreateCart
    {
        [Required]
        [StringLength(32)]
        public string UserId { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string SizeColor { get; set; } = default!;

        [Range(1, int.MaxValue)]
        [DefaultValue(0)]
        public int Quantity { get; set; }
    }

    public class RequestUpdateCart : RequestCreateCart
    {
        [Required]
        [StringLength(32)]
        public string Id { get; set; } = default!;
    }
}
