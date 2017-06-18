using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.DataAnnotations
{
    public class CheckBirthDayAttribute : RegularExpressionAttribute
    {
        public CheckBirthDayAttribute() : base(@"^\d{1,2}$")
        {
            ErrorMessage = "Ngày sinh không đúng.";
        }
    }
}