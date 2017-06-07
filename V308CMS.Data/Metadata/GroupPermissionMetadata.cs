using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class GroupPermissionMetadata
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
      
        [MaxLength(255)]
        public  string Description { get; set; }

    }
}