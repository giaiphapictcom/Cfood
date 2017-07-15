using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Metadata
{
    public class VoucherCodeMetadata
    {        
        [Required]
        [MaxLength(6)]
        public string Code { get; set; }
        [Required]
        public int VoucherId { get; set; }
        [Required]
        public int UserId { get; set; }        

    }
}