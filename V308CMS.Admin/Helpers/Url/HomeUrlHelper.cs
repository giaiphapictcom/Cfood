using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class HomeUrlHelper
    {
        public static string HomeIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "home",
          string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string HomeLogoutUrl(this UrlHelper helper, object routeValue = null, string controller = "home",
          string action = "logout")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string HomeLoginUrl(this UrlHelper helper, object routeValue = null, string controller = "home",
          string action = "login")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string HomeChucNangUrl(this UrlHelper helper, object routeValue = null, string controller = "home",
         string action = "chucnang")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}