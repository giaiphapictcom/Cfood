using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
   
   
    public class ProductBrandMetadata
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }


    }
}