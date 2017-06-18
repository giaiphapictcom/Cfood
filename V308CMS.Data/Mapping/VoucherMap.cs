using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class VoucherMap: EntityTypeConfiguration<Voucher>
    {
        public VoucherMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Description)              
               .HasMaxLength(255);

            ToTable("voucher");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Name).HasColumnName("name");
            Property(t => t.Description).HasColumnName("description");
            Property(t => t.CreatedAt).HasColumnName("created_at");
            Property(t => t.UpdatedAt).HasColumnName("updated_at");
            Property(t => t.ExpireDate).HasColumnName("expire_date");
            Property(t => t.DiscountType).HasColumnName("discount_type");
            Property(t => t.Value).HasColumnName("value");
            Property(t => t.State).HasColumnName("state");

        }
    }
}