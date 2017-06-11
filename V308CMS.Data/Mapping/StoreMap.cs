using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class StoreMap : EntityTypeConfiguration<Store>
    {
        public StoreMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Phone)              
                .HasMaxLength(15);

            this.Property(t => t.Manager)
               .HasMaxLength(50);

            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(500);

            this.ToTable("store");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Phone).HasColumnName("phone");
            this.Property(t => t.Manager).HasColumnName("manager");            
            this.Property(t => t.Address).HasColumnName("address");
            this.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            this.Property(t => t.CreatedAt).HasColumnName("created_at");
            this.Property(t => t.State).HasColumnName("state");

        }
    }
}