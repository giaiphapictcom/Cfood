using System;
using System.Web;

namespace V308CMS.Sale.Models
{
    public class CouponModel
    {
        public CouponModel()
        {
            Created = DateTime.Now;
            start_date = DateTime.Now;
            Status = 1;
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime Created { get; set; }

        public HttpPostedFileBase File { get; set; }
        public string Image { get; set; }
    }
}