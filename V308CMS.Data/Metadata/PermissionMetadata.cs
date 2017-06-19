using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class PermissionMetadata
    {        
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string GroupPermission { get; set; }
        [Required]
        public int Value { get; set; }
        public int RoleId { get; set; }
    }
}