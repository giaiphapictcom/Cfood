using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.DataAnnotations
{
    public class CheckUserNameAttribute : RegularExpressionAttribute
    {
        public CheckUserNameAttribute() : base(@"^[a-zA-Z0-9_]{6,25}$")
        {
            ErrorMessage = "Tên tài khoản từ 6 đến 25 ký tự ko được chứa khoảng trắng, ký tự đặc biệt.";
        }
    }
}