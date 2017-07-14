using System;
using System.Web;

namespace V308CMS.Sale.Models
{
    public class BannerModel
    {
        public BannerModel()
        {
            Created = DateTime.Now;
            Status = 1;
        }

        public int Id { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string URl { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        
    }
}