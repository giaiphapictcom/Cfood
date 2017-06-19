using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.Models
{
    public class SizeModels : BaseModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên kích cỡ.")]
        [StringLength(50, ErrorMessage = "Tên kích cỡ không được vượt quá 50 ký tự.")]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Giá trị kích cỡ không được vượt quá 50 ký tự.")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
    }

}