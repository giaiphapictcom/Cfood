using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class AffilateCodeMap: EntityTypeConfiguration<AffilateCode>
    {
        public AffilateCodeMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Code)
                .HasMaxLength(15)
                .IsRequired();

            ToTable("affilate_code");
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.Code).HasColumnName("code");
            Property(t => t.Status).HasColumnName("status");
            Property(t => t.CreatedAt).HasColumnName("created_at");

        }

    }
}