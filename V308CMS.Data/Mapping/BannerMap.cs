using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class BannerMap: EntityTypeConfiguration<Banner>
    {
        public BannerMap()
        {

            // Primary Key
            HasKey(t => t.Id);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Description)
             .HasMaxLength(250);

            Property(t => t.ImageUrl)
             .HasMaxLength(2500);

            ToTable("banner");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Name).HasColumnName("name");
            Property(t => t.Description).HasColumnName("description");
            Property(t => t.Position).HasColumnName("position");
            Property(t => t.Width).HasColumnName("width");
            Property(t => t.Height).HasColumnName("height");
            Property(t => t.StartDate).HasColumnName("start_date");
            Property(t => t.EndDate).HasColumnName("end_date");
            Property(t => t.ImageUrl).HasColumnName("image_url");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.CreatedAt).HasColumnName("created_at");
            Property(t => t.UpdatedAt).HasColumnName("updated_at");
            Property(t => t.Site).HasColumnName("site");
        }

    }
}