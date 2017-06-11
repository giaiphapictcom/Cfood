using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
 
    public class SizeMetadata
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
       
        [MaxLength(50)]
        public string Description { get; set; }
    }
}