using System.ComponentModel.DataAnnotations;
using V308CMS.DataAnnotations;

namespace V308CMS.Models
{
    public class ContactModels
    {
        [Required(ErrorMessage = "Vui lòng nhập Họ tên của bạn.")]
        [Display(Name="Họ tên")]
        public  string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email của bạn.")]
        [Display(Name = "Email")]
        [CheckEmail]
        public string Email { get; set; }       
        [Display(Name = "Số điện thoại")]
        [Checkphone]
        public string Phone { set;get; }
        [Required(ErrorMessage = "Vui lòng nhập lời nhắn của bạn.")]
        [Display(Name = "Lời nhắn")]
        public string Message { get; set; }
    }
}