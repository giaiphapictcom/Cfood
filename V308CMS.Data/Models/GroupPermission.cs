using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("group_permission")]
    [MetadataType(typeof(GroupPermissionMetadata))]
    public class GroupPermission
    {
        public GroupPermission()
        {
            this.Permissions = new HashSet<Permission>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte Status { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}