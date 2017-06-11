using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class ProductSizeMap: EntityTypeConfiguration<ProductSize>
    {
        public ProductSizeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Size)
               .IsRequired()
               .HasMaxLength(50);
            this.ToTable("product_size");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.ProductId).HasColumnName("product_id");
            this.Property(t => t.Size).HasColumnName("size");          
        }
    }
}