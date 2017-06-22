using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.Models
{
    public class ProfileChangePassworModels : BaseModels
    {
        public string UserName { get; set; }
        public int Id { get; set; }
        [Display(Name = "Mật khẩu hiện tại")]
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu hiện tại.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu mới.")]
        [StringLength(32, ErrorMessage = "{0} từ 6 đến 32 ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu xác nhận.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        [Display(Name = "Mật khẩu xác nhận")]
        public string ConfirmPassword { get; set; }

        public int ChangePassword { get; set; }
    }
    public class ProfileViewModels
    {
        public ProfileViewModels()
        {
            AdminProfileModels = new AdminModels();
            AdminChangePasswordeModels = new ProfileChangePassworModels();
        }
        public AdminModels AdminProfileModels { get; set; }
        public ProfileChangePassworModels AdminChangePasswordeModels { get; set; }
    }
}