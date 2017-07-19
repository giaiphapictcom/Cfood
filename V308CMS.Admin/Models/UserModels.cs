using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using V308CMS.Admin.DataAnnotations;

namespace V308CMS.Admin.Models
{
    public class UserModels:BaseModels
    {
        public UserModels()
        {
            Gender = true;
            Status = true;
            CreateDate = DateTime.Now;

        }
        public int Id { get; set; }
        [Display(Name = "Loại tài khoản")]
        public string Site { get; set; }
        public string AffiliateCode { get; set; }

        [Required(ErrorMessage = "Địa chỉ Email không được để trống.")]
        [Display(Name = "Email*")]
        [StringLength(50, ErrorMessage = "{0} không được vượt quá 50 ký tự.")]
        [CheckEmail(ErrorMessage ="{0} không đúng.")]
        public string Email { get; set; }

        [Display(Name = "Tài khoản")]        
        [StringLength(50,ErrorMessage = "{0} không được vượt quá 50 ký tự.")]
        public string Username { get; set; }

        [Display(Name = "Họ tên")]
        [StringLength(50, ErrorMessage = "{0} không được vượt quá 50 ký tự.")]
        public string FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        [StringLength(50, ErrorMessage = "{0} không được vượt quá 50 ký tự.")]
        [Checkphone(ErrorMessage = "{0} không đúng.")]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(250, ErrorMessage = "{0} không được vượt quá 250 ký tự.")]
        public string Address { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }

        [Display(Name = "Ngày sinh")]
        public string BirthDay { get; set; }
        [Display(Name = "Mật khẩu*")]
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu xác nhận.")]
        [Display(Name = "Xác nhận mật khẩu*")]
        [Compare("Password",ErrorMessage = "{0} không đúng.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool Status { get; set; }

        public DateTime CreateDate { get; set; }

        public string AvatarUrl { get; set; }
         public HttpPostedFileBase Avatar { get; set; }

    }
}