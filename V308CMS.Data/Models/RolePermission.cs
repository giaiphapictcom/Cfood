using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Models
{
    public class RolePermission
    {
        [Key]
        [Required]
        public int RoleId { get; set; }
        [Key]
        [Required]
        public int PermissionId { get; set; }
    }
}