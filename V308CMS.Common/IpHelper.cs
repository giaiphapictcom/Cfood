using System.Web;

namespace V308CMS.Common
{
    public class IpHelper
    {
        public static string ClientIpAddress
        {
            get
            {
                var userHostAddress = HttpContext.Current.Request.UserHostAddress;
                if (userHostAddress != null && userHostAddress.Equals(""))
                {
                    userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    if (userHostAddress.Equals(""))
                        userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                return userHostAddress;
            }
        }
    }
}