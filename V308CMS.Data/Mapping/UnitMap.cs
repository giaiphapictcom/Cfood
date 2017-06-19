using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class UnitMap : EntityTypeConfiguration<Unit>
    {
        public UnitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
          
            this.ToTable("unit");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.Name).HasColumnName("name");         
            this.Property(t => t.CreatedAt).HasColumnName("created_at");            
            this.Property(t => t.UpdatedAt).HasColumnName("updated_at");         
            this.Property(t => t.State).HasColumnName("state");

        }
    }
}