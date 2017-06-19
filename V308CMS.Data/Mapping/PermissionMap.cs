using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class PermissionMap: EntityTypeConfiguration<Permission>
    {

        public PermissionMap()
        {

            this.HasKey(t => t.Id);

            this.Property(t => t.GroupPermission)
                .IsRequired();

            this.Property(t => t.Value)
                .IsRequired();
            this.Property(t => t.RoleId)
             .IsRequired();

            this.ToTable("permission");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.RoleId).HasColumnName("role_id");
            this.Property(t => t.GroupPermission).HasColumnName("group_permission");
            this.Property(t => t.Value).HasColumnName("value");                      

        }

    }
}