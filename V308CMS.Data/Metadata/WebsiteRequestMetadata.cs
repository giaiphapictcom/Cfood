using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Data.Metadata
{
    public class WebsiteRequestMetadata
    {
        [Key]
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