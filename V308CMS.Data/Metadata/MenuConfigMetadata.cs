using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{   
    public class MenuConfigMetadata
    {
       
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(500)]
        public string Link { get; set; }
        public byte State { get; set; }        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int Order { get; set; }

    }
}