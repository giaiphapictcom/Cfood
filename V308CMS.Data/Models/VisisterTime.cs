using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Enum;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
 
    [Table("visister_time")]
    public class VisisterTime
    {
        public VisisterTime()
        {
            created = DateTime.Now;
            updated = DateTime.Now;
        }
        public int id { get; set; }
        public int visister_id { get; set; }

        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int count { get; set; }


    }
}