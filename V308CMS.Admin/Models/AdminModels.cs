using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using V308CMS.Admin.DataAnnotations;

namespace V308CMS.Admin.Models
{
    public class AdminEditModels
    {
        
    }
    public class AdminModels:BaseModels
    {
        public AdminModels()
        {
            Status = true;
            Date = DateTime.Now;
        }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Tên tài khoản.")]
        [CheckUserName(ErrorMessage = "Tên tài khoản từ 6 đến 25 ký tự ko được chứa khoảng trắng, ký tự đặc biệt.")]
        [Display(Name = "Tài khoản*")]
        [StringLength(50, ErrorMessage = "Tên tài khoản không được vượt quá 50 ký tư.")]
        public string UserName { get; set; }
        [Display(Name ="Mật khẩu*")]
        [StringLength(50, ErrorMessage = "Mật khẩu không được vượt quá 50 ký tự.")]
        public string Password { get; set; }

        [Display(Name = "Mật khẩu xác nhận*")]
        [StringLength(50, ErrorMessage = "Mật khẩu xác nhận không được vượt quá 50 ký tự.")]
        public string ConfirmPassword { get; set; }

        [Display(Name="Email")]
        [CheckEmail(ErrorMessage = "Địa chỉ Email không đúng.")]
        [StringLength(50, ErrorMessage = "Đại chỉ Email không được vượt quá 50 ký tự.")]
        public string Email { get; set; }
        [Display(Name = "Họ và tên")]
        [StringLength(50, ErrorMessage = "Họ tên không được vượt quá 50 ký tự.")]
        public string FullName { get; set; }
        [Display(Name = "Quyền*")]
        [Required(ErrorMessage = "Vui lòng chọn Quyền của tài khoản")]
        public int Role { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn loại tài khoản.")]
        [Display(Name = "Loại")]
       
        public byte Type { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string AvatarUrl { get; set; }
        public HttpPostedFileBase AvatarFile { get; set; }

    }
}