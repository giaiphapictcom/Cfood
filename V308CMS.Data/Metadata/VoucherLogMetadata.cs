using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class VoucherLogMetadata
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int VoucherId { get; set; }

        [Required]
        [MaxLength(50)]
        public string VoucherCode { get; set; }
    }
}