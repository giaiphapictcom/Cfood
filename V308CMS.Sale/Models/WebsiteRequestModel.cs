using System;
using System.Web;

namespace V308CMS.Sale.Models
{
    public class WebsiteRequestModel
    {
        public WebsiteRequestModel()
        {
            created = DateTime.Now;
            status = 1;
        }
        public int id { get; set; }
        public string domain { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string content { get; set; }
        public DateTime created { get; set; }
        public int status { get; set; }
    }
}