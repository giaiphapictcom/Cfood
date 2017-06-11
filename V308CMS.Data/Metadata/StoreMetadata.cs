using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
      
    public class StoreMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }


    }
}