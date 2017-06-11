using System.Data.Entity.ModelConfiguration;

namespace V308CMS.Data.Mapping
{
    public class SiteConfigMap: EntityTypeConfiguration<SiteConfig>
    {
        public SiteConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Content)
                .IsRequired();
             

            // Table & Column Mappings
            this.ToTable("siteconfig");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Content).HasColumnName("content");
        }
    }
}