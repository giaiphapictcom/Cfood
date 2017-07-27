using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Metadata
{
    public class AffilateCodeMetadata
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Code { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}