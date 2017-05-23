using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using V308CMS.DataAnnotations;

namespace V308CMS.Models
{
    public class ChangePasswordModels
    {  
        public string Token { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu hiện tại.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu hiện tại.")]
        [StringLength(32, ErrorMessage = "{0} từ 6 đến 32 ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu xác nhận.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        [Display(Name = "Mật khẩu xác nhận")]
        public string ConfirmPassword { get; set; }

    }
    public class RegisterModels
    {
        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [CheckEmail]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu.")]
        [StringLength(32, ErrorMessage = "{0} từ 6 đến 32 ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu xác nhận.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        [Display(Name = "Mật khẩu xác nhận")]
        public string ConfirmPassword { get; set; }

        public void ResetPasswordValue()
        {
            if (!string.IsNullOrWhiteSpace(Password))
            {
                Password = string.Empty;
            }
            if (!string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ConfirmPassword = string.Empty;
            }
        }

    }
    public class LoginModels
    {
        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [Display(Name = "Email :")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu tài khoản.")]
        [Display(Name = "Mật khẩu :")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ActiveModels
    {
        [Required(ErrorMessage = "Vui lòng nhập Mã kích hoạt tài khoản.")]
        [Display(Name = "Mã kích hoạt tài khoản")]
        public string Token { get; set; }
    }

    public class GetTokenModels
    {
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class AccountForgotPasswordModels
    {
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

}