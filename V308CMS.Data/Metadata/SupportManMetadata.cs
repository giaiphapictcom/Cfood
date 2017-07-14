using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class SupportManMetadata
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        [MaxLength(50)]
        public string Site { get; set; }
        public int Order { get; set; }
        public bool Status { get; set; }
    }
}