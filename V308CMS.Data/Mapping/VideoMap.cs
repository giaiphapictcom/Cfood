using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class VideoMap: EntityTypeConfiguration<Video>
    {
        public VideoMap()
        {
            HasKey(t => t.Id);
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Link)
               .IsRequired()
               .HasMaxLength(2500);

            ToTable("video");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Title).HasColumnName("title");
            Property(t => t.Link).HasColumnName("link");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.CreatedAt).HasColumnName("created_at");
            Property(t => t.UpdatedAt).HasColumnName("updated_at");
            Property(t => t.TotalView).HasColumnName("total_view");
            Property(t => t.Position).HasColumnName("position");
            Property(t => t.Order).HasColumnName("order");

        }

    }
}