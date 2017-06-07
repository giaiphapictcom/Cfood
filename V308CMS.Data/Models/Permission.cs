using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("permission")]
    [MetadataType(typeof(PermissionMetadata))]
    public  class Permission
    {
        public Permission()
        {
            this.Roles = new HashSet<Role>();
        }
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Action { get; set; }
        public int Value { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Role> Roles { get; set; }

    }
}