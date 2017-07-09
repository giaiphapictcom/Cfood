using System.Web;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.Models
{
    public class AffiliateCategoryModels : BaseModels
    {
        public AffiliateCategoryModels()
        {
            
            Status = true;
        }

        public int id { get; set; }

        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Summary { get; set; }

        [Display(Name = "URL")]
        public string Link { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Thứ tự")]
        public int Order { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        public HttpPostedFileBase ImgNew { get; set; }



    }
}
