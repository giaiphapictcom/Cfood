using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class BannerMap: EntityTypeConfiguration<Banner>
    {
        public BannerMap()
        {

            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
             .HasMaxLength(250);

            this.Property(t => t.ImageUrl)
             .HasMaxLength(2500);

            this.ToTable("banner");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.Position).HasColumnName("position");
            this.Property(t => t.Width).HasColumnName("width");
            this.Property(t => t.Height).HasColumnName("height");
            this.Property(t => t.StartDate).HasColumnName("start_date");
            this.Property(t => t.EndDate).HasColumnName("end_date");
            this.Property(t => t.ImageUrl).HasColumnName("image_url");
            this.Property(t => t.Status).HasColumnName("status");
            this.Property(t => t.CreatedAt).HasColumnName("created_at");
            this.Property(t => t.UpdatedAt).HasColumnName("updated_at");
        }

    }
}