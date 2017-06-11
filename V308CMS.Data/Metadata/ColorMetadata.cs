using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{  
    public class ColorMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}