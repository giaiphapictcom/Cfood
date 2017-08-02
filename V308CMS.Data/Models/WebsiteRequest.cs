using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V308CMS.Data.Metadata;

namespace V308CMS.Data.Models
{
    [MetadataType(typeof(WebsiteRequestMetadata))]
    [Table("website_request")]
    public class WebsiteRequest
    {
        public WebsiteRequest()
        {
            created = DateTime.Now;
            status = 1;
            created_by = 0;

        }
        public int id { get; set; }
        public string domain { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string content { get; set; }
        public DateTime created { get; set; }
        public int created_by { get; set; }
        public int status { get; set; }
    }
}