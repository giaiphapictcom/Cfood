using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(CountryMetadata))]
    [Table("country")]
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}