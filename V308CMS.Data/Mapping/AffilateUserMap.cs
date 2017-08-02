using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class AffilateUserMap : EntityTypeConfiguration<AffilateUser>
    {
        public AffilateUserMap()
        {
            HasKey(t => t.Id);
            Property(t => t.UserId)
                .IsRequired();
            Property(t => t.AffilateId)
                .IsRequired();

            ToTable("affilate_user");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.AffilateId).HasColumnName("affilate_id");
            Property(t => t.UserId).HasColumnName("user_id");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.CreatedAt).HasColumnName("created_at");
            Property(t => t.UpdatedAt).HasColumnName("updated_at");
            Property(t => t.Amount).HasColumnName("amount");
        }


    }
}