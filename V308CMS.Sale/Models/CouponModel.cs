using System;
using System.Web;
using V308CMS.Data;
namespace V308CMS.Sale.Models
{
    public class CouponModel
    {
        public CouponModel()
        {
            Created = DateTime.Now;
            start_date = DateTime.Now;
            Status = 1;
            discount = 2;
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        public float discount { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Created { get; set; }

        public HttpPostedFileBase File { get; set; }
        public string Image { get; set; }

        public Product product { get; set; }
    }
}