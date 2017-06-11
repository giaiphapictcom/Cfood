using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class ProductColorMap: EntityTypeConfiguration<ProductColor>
    {
        public ProductColorMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.ColorCode)
               .IsRequired()
               .HasMaxLength(50);
            this.ToTable("product_color");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ProductId).HasColumnName("product_id");
            this.Property(t => t.ColorCode).HasColumnName("color_code");          
        }
    }
}