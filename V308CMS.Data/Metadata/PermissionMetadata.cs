using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class PermissionMetadata
    {        
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int GroupId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Action { get; set; }
        [Required]
        public int Value { get; set; }
    }
}