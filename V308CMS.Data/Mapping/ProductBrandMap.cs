using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class ProductBrandMap: EntityTypeConfiguration<Brand>
    {
        public ProductBrandMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.image)               
                .HasMaxLength(250);

            this.ToTable("product_brand");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.category_default).HasColumnName("category_default");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.image).HasColumnName("image");
            this.Property(t => t.status).HasColumnName("status");

        }

        
    }
}