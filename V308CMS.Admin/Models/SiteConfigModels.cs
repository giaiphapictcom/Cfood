using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.Models
{

    public class SiteConfigModels:BaseModels

    {
        [Required(ErrorMessage = "Vui lòng nhập tên cấu hình.")]
        [Display(Name = "Tên thuộc tính : ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá trị cấu hình.")]
        [Display(Name = "Giá trị :")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Id trống.")]
        public int Id { get; set; }

        public byte Site { get; set; }
    }
}