using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace V308CMS.Data.Models
{
    [Table("email_log")]
    public class EmailLog
    {
        public EmailLog()
        {
            CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string AttachFileUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}