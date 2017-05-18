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
        public  string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email của bạn !")]
        public string Email { get; set; }

        public string Phone { set;get; }
        public string Message { get; set; }
    }
}