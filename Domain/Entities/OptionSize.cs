using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OptionSize : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string SizeValue { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string CategoryMajor { get; set; } = default!;
    }

    public class OptionSizeColor
    {
        [Required]
        [StringLength(32)]
        public string OptionId { get; set; } = default!;
        [ForeignKey(nameof(OptionId))]
        public virtual ProductOption Option { get; set; } = default!;

        [Required]
        [StringLength(32)]
        public string OptionSize { get; set; } = default!;
        [ForeignKey(nameof(OptionSize))]
        public virtual OptionSize Size { get; set; } = default!;

        [DefaultValue(0)]
        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public double Price { get; set; }
    }
}
