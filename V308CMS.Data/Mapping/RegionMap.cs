using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class RegionMap : EntityTypeConfiguration<Region>
    {
        public RegionMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Code)               
               .HasMaxLength(10);

            ToTable("region");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Name).HasColumnName("name");
            Property(t => t.Code).HasColumnName("code");
            Property(t => t.ParentId).HasColumnName("parent_id");
        }
    }
}