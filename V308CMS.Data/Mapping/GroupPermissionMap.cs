using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class GroupPermissionMap : EntityTypeConfiguration<GroupPermission>
    {
        public GroupPermissionMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Name)
              .IsRequired()
              .HasMaxLength(50);

            Property(t => t.Code)
             .IsRequired()
             .HasMaxLength(50);

            Property(t => t.Description)           
            .HasMaxLength(255);

            ToTable("group_permission");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Name).HasColumnName("name");
            Property(t => t.Code).HasColumnName("code");
            Property(t => t.Description).HasColumnName("description");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.CreatedAt).HasColumnName("created_at");
            Property(t => t.UpdatedAt).HasColumnName("updated_at");
        }

    }
}