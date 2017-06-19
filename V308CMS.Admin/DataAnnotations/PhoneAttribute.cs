using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.DataAnnotations
{
    public class CheckphoneAttribute : RegularExpressionAttribute
    {
        public CheckphoneAttribute()
            : base(@"^(84|0)(90|91|92|93|94|95|96|97|98|
         120|121|122|123|124|125|126|127|128|129|
         163|164|165|166|167|168|169|
         993|994|995|996|188|199)\d{7}$")
        {
            ErrorMessage = "Số điện thoại không đúng.";
        }
    }
}