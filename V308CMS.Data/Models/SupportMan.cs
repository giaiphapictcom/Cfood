using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(SupportManMetadata))]
    [Table("support_man")]
    public class SupportMan
    {
        public SupportMan()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
            Site = Helpers.Site.@default;
            Status = true;

        }
        public int id { get; set; }
        public string Site { get; set; }


        public string Name { get; set; }
        public string Skype { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }

        public int Order { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

       

    }
}