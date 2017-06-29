using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace V308CMS.Admin.Models
{
    public class TestimonialModels : BaseModels
    {
        public TestimonialModels()
        {
            
            Status = true;
        }
        public int Id { get; set; }
        public string Site { get; set; }

        [Display(Name = "Nội Dung")]
        public string Content { get; set; }

        [Display(Name = "Họ và Tên")]
        public string Fullname { get; set; }

        [Display(Name = "Taget")]
        public string Taget { get; set; }

        [Display(Name = "Avartar")]
        public string Avartar { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Mobile { get; set; }

        [Display(Name = "Hiển thị")]
        public bool Status { get; set; }

        public HttpPostedFileBase Upload { get; set; }



    }
}