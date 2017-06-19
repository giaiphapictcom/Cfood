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
        public int Id { get; set; }
        public string GroupPermission { get; set; }
        public  int Value { get; set; } 
        public int RoleId { get; set; }

        [ForeignKey("Role")]
        public Role RoleInfo { get; set; }

    }
}