using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
 

    public class SiteConfigMetadata
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
     
        public string Content { get; set; }
        [Key]
        public int Id { get; set; }

        public byte Site { get; set; }

    }
}