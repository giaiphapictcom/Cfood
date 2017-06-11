using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Metadata
{
    public class ProductColorMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ColorCode { get; set; }

        public int ProductId { get; set; }

    }
}