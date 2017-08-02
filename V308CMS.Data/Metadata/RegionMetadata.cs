using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Metadata
{
    public class RegionMetadata
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(10)]
        [Required]
        public string Code { get; set; }
        public int ParentId { get; set; }
    }
}