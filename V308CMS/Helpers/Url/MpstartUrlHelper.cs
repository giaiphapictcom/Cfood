using System;
using System.Diagnostics;
using System.Web;

namespace V308CMS.Helpers.Url
{
    public static class MpstartUrlHelper
    {
       
        public static bool IsLocalUrl(this  string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return false;
                }
                var uri = new Uri(url);

                if (uri.Host.Equals(HttpContext.Current.Request.Url.Host))
                {
                    return true;
                }

                if ((url.Length > 1 && url[0] == '~' && url[1] == '/'))
                {
                    return true;

                }
                if (url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\')))
                {

                    return true;
                }
                return false;
            }
            catch (Exception ex){Debug.WriteLine("Error : " + ex); return false;}
         
        }

    }
}