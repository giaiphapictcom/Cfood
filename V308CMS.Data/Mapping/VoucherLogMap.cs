using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class VoucherLogMap : EntityTypeConfiguration<VoucherLog>
    {
        public VoucherLogMap()
        {
            HasKey(t => t.Id);
            Property(t => t.UserId)
                .IsRequired();
            Property(t => t.VoucherId)
                .IsRequired();
            Property(t => t.VoucherCode)
             .IsRequired()
             .HasMaxLength(6);


            ToTable("voucher_log");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.UserId).HasColumnName("user_id");
            Property(t => t.VoucherId).HasColumnName("voucher_id");
            Property(t => t.VoucherCode).HasColumnName("voucher_code");
            Property(t => t.CreatedAt).HasColumnName("created_at");

        }


    }
}