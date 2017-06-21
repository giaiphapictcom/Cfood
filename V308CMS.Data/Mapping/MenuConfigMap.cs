using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class MenuConfigMap: EntityTypeConfiguration<MenuConfig>
    {
        public MenuConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(t => t.Description)
                .HasMaxLength(255);
            this.Property(t => t.Code)
               .HasMaxLength(50);
            this.Property(t => t.Link)
               .HasMaxLength(500);
            this.Property(t => t.Target)
              .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("menu_config");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.Code).HasColumnName("code");
            this.Property(t => t.Link).HasColumnName("link");
            this.Property(t => t.State).HasColumnName("state");
            this.Property(t => t.Order).HasColumnName("order");
            this.Property(t => t.Target).HasColumnName("target");
            this.Property(t => t.Site).HasColumnName("site");
            this.Property(t => t.CreatedAt).HasColumnName("created_at");
            this.Property(t => t.UpdatedAt).HasColumnName("updated_at");

        }
    }
}