using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class EmailLogMap: EntityTypeConfiguration<EmailLog>
    {
        public EmailLogMap()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.To)
                .IsRequired();

            this.Property(t => t.To)
                .IsRequired();

            this.Property(t => t.Subject)
                .IsRequired();

            this.Property(t => t.Content)
                .IsRequired();

            this.ToTable("email_log");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.To).HasColumnName("to");
            this.Property(t => t.Subject).HasColumnName("subject");
            this.Property(t => t.Cc).HasColumnName("cc");
            this.Property(t => t.Bcc).HasColumnName("bcc");
            this.Property(t => t.Content).HasColumnName("content");
            this.Property(t => t.AttachFileUrl).HasColumnName("attach_file_url");

        }

    }
}