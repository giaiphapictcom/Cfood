using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(SiteConfigMetadata))]
    [Table("siteconfig")]
    public class SiteConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public string Site { get; set; }
    }
}