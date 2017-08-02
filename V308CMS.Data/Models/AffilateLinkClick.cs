using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [Table("affiliate_link_click")]
    public class AffilateLinkClick
    {
        public AffilateLinkClick()
        {
            created = DateTime.Now;
            updated = DateTime.Now;
        }
        public int id { get; set; }
        public int link_id { get; set; }

        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int count { get; set; }
    }
}