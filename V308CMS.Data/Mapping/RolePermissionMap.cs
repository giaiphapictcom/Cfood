using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class RolePermissionMap: EntityTypeConfiguration<RolePermission>
    {
        public RolePermissionMap()
        {
            HasKey(t => t.RoleId);
            HasKey(t => t.PermissionId);
           
            ToTable("role_permission");
            
            Property(t => t.RoleId).HasColumnName("role_id");
            Property(t => t.PermissionId).HasColumnName("permission_id");
        }
    }
}