using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Enum;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("email_config")]
    [MetadataType(typeof(EmailConfigMetadata))]
    public class EmailConfig
    {
        public EmailConfig()
        {
            State = (byte)StateEnum.Active;
            CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public byte Type { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte State { get; set; }
    }
}