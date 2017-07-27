using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Metadata
{
    public class AffilateUserMetadata
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AffilateId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte Status { get; set; }
    }
}