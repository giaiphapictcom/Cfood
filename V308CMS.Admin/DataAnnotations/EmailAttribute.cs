using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.DataAnnotations
{
    public class CheckEmailAttribute : RegularExpressionAttribute
    {
        public CheckEmailAttribute()
            : base(@"^\s*[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}\s*$")
        {
            ErrorMessage = "Địa chỉ Email không đúng.";
        }
    }
}