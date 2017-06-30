using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class ProductWishlistMap: EntityTypeConfiguration<ProductWishlist>
    {
        public ProductWishlistMap()
        {

           HasKey(t => t.Id);
           Property(t => t.UserId)
                .IsRequired();
           Property(t => t.ListProduct)
                .HasMaxLength(2500)
                .IsRequired();

           ToTable("product_wishlist");
           Property(t => t.Id).HasColumnName("id");
           Property(t => t.UserId).HasColumnName("user_id");
            Property(t => t.ListProduct).HasColumnName("list_product");

        }

    }
}