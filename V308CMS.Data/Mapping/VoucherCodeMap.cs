using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class VoucherCodeMap: EntityTypeConfiguration<VoucherCode>
    {
        public VoucherCodeMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Code)
              .IsRequired()
              .HasMaxLength(6);
            Property(t => t.VoucherId)
                .IsRequired();      
            ToTable("voucher_code");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Code).HasColumnName("code");
            Property(t => t.VoucherId).HasColumnName("voucher_id");            
            Property(t => t.State).HasColumnName("state");
            Property(t => t.CreatedAt).HasColumnName("created_at");
        }

    }
}

