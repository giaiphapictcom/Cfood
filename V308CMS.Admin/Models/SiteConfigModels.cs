using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.Models
{

    public class SiteConfigModels:BaseModels

    {
        [Required(ErrorMessage = "Vui lòng nhập tên cấu hình.")]
        [Display(Name = "Tên thuộc tính : ")]
        public string name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá trị cấu hình.")]
        [Display(Name = "Giá trị :")]
        public string content { get; set; }
        [Required(ErrorMessage = "Id trống.")]
        public int id { get; set; }
    }
}