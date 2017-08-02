using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class OrderTransactionMap: EntityTypeConfiguration<OrderTransaction>
    {
        public OrderTransactionMap()
        {
            HasKey(t => t.Id);
            Property(t => t.OrderId)
                .IsRequired();

            Property(t => t.TransactionId)
               .IsRequired()
               .HasMaxLength(50);

            ToTable("order_transaction");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.OrderId).HasColumnName("order_id");
            Property(t => t.TransactionId).HasColumnName("transaction_id");
            Property(t => t.CreatedAt).HasColumnName("created_at");
            Property(t => t.FinishAt).HasColumnName("finish_at");
            Property(t => t.Status).HasColumnName("status");
        }

    }
}