using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;
using V308CMS.Data.Models;

namespace V308CMS.Data.Models
{
    [Table("role")]
    [MetadataType(typeof(RoleMetadata))]
    public class Role
    {
        public Role()
        {
            this.Permissions = new HashSet<Permission>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }

        public virtual  ICollection<Permission> Permissions { get; set; }
    }
}