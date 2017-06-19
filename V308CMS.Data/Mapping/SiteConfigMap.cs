using System.Data.Entity.ModelConfiguration;

namespace V308CMS.Data.Mapping
{
    public class SiteConfigMap: EntityTypeConfiguration<SiteConfig>
    {
        public SiteConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.content)
                .IsRequired();
             

            // Table & Column Mappings
            this.ToTable("siteconfig");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.content).HasColumnName("content");
        }
    }
}