using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class PermissionMap: EntityTypeConfiguration<Permission>
    {

        public PermissionMap()
        {

            this.HasKey(t => t.Id);

            this.Property(t => t.GroupId)
                .IsRequired();

            this.Property(t => t.Value)
                .IsRequired();

            this.Property(t => t.Action)
            .IsRequired()
            .HasMaxLength(50);

            this.ToTable("permission");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.GroupId).HasColumnName("group_id");
            this.Property(t => t.Action).HasColumnName("action");          
            this.Property(t => t.Status).HasColumnName("status");
            this.Property(t => t.CreatedAt).HasColumnName("created_at");
            this.Property(t => t.UpdatedAt).HasColumnName("updated_at");

        }

    }
}