using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
   
  
    public class EmailConfigMetadata
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public byte Type { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        [Required]
        public string Host { get; set; }
        [MaxLength(255)]
        [Required]
        public string Port { get; set; }
        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte State { get; set; }
    }
}