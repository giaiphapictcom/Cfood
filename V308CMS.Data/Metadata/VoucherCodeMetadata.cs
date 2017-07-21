using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class VoucherCodeMetadata
    {        
        [Required]
        [MaxLength(6)]
        public string Code { get; set; }
        [Required]
        public int VoucherId { get; set; }
                

    }
}