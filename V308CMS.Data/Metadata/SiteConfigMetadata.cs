using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    [MetadataType(typeof(SiteConfigMetadata))]
    public partial class SiteConfig
    {

    }

    public class SiteConfigMetadata
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
     
        public string Content { get; set; }
        [Key]
        public int Id { get; set; }

    }
}