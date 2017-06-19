using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class VoucherMetadata
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]

        public string Description { get; set; }
        [Key]
        public int Id { get; set; }
    }
}