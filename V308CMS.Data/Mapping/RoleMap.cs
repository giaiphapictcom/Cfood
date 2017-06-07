using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Name)
             .IsRequired()
             .HasMaxLength(250);

            Property(t => t.Description)          
            .HasMaxLength(500);

            ToTable("role");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Name).HasColumnName("name");
            Property(t => t.Description).HasColumnName("description");
            Property(t => t.Status).HasColumnName("status");
        }


    }
}