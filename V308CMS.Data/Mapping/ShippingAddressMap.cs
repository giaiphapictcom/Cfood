using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class ShippingAddressMap: EntityTypeConfiguration<ShippingAddress>
    {
        public ShippingAddressMap()
        {
            HasKey(t => t.Id);

            Property(t => t.FullName)                
                .HasMaxLength(50);

            Property(t => t.Phone)
               .HasMaxLength(15);

            Property(t => t.Region)
               .HasMaxLength(250);

            Property(t => t.City)
               .HasMaxLength(250);

            Property(t => t.Ward)
              .HasMaxLength(250);

            Property(t => t.Address)
              .HasMaxLength(500);

            ToTable("shipping_address");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.UserId).HasColumnName("user_id");
            Property(t => t.FullName).HasColumnName("full_name");
            Property(t => t.Phone).HasColumnName("phone");
            Property(t => t.Region).HasColumnName("region");
            Property(t => t.City).HasColumnName("city");
            Property(t => t.Ward).HasColumnName("ward");
            Property(t => t.Address).HasColumnName("address");
            Property(t => t.Default).HasColumnName("default");
            Property(t => t.UpdatedAt).HasColumnName("updated_at");

        }
    }
}

