using System.Web;

namespace V308CMS.Helpers
{
    public class MyUser
    {
        public static bool IsAuthenticate
        {
            get
            {
                return (
                     HttpContext.Current.User.Identity.IsAuthenticated ||
                     HttpContext.Current.Session["UserName"] != null
                   );
            }
        }

        public static string UserName
        {
            get
            {
                if (IsAuthenticate)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        return HttpContext.Current.User.Identity.Name;
                    }
                    if (HttpContext.Current.Session["UserName"] != null)
                    {
                        return HttpContext.Current.Session["UserName"].ToString();
                    }
                    
                }
                return string.Empty;

            }
        }

    }
}