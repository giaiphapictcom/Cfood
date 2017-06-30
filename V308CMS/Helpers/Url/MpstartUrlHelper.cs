using System;
using System.Web;

namespace V308CMS.Helpers.Url
{
    public static class MpstartUrlHelper
    {
       
        public static bool IsLocalUrl(this  string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }
            else
            {
                var uri = new Uri(url);
                
                if (uri.Host.Equals(HttpContext.Current.Request.Url.Host))
                {
                    return true;
                }

                if ((url.Length > 1 && url[0] == '~' && url[1] == '/'))
                {
                    return true;

                }
                else if (url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\')))
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

    }
}