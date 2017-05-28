using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace V308CMS.Admin.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string Password { get; set; }
        public int Remember { get; set; }
    }
}