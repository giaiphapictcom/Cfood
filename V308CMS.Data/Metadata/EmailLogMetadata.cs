using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Metadata
{
    public class EmailLogMetadata
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        [Required]
        [MaxLength(250)]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }
        public string AttchFileUrl { get; set; }
    }
}