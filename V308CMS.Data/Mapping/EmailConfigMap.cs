using System.Data.Entity.ModelConfiguration;
using V308CMS.Data.Models;

namespace V308CMS.Data.Mapping
{
    public class EmailConfigMap: EntityTypeConfiguration<EmailConfig>
    {
        public EmailConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Host)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Port)
                .IsRequired();

            this.Property(t => t.UserName)
                 .IsRequired();

            this.Property(t => t.Password)
               .IsRequired();

            this.Property(t => t.CreatedDate).IsOptional();
            this.Property(t => t.State);

            // Table & Column Mappings
            this.ToTable("email_config");
            this.Property(t => t.Id).HasColumnName("id");
            this.Property(t => t.Type).HasColumnName("type");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Host).HasColumnName("host");
            this.Property(t => t.UserName).HasColumnName("username");
            this.Property(t => t.Password).HasColumnName("password");
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
            this.Property(t => t.State).HasColumnName("state");
        }
    }

}