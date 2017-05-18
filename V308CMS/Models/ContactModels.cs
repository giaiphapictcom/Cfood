using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace V308CMS.Models
{
    public class ContactModels
    {
        [Required(ErrorMessage = "Vui lòng nhập Họ tên của bạn !")]
        [Display(Name="Họ tên")]
        public  string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email của bạn !")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Phone { set;get; }
        [Display(Name = "Ghi chú")]
        public string Message { get; set; }
    }
}