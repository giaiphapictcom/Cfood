using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class MenuConfigUrlHelper
    {
        public static string MenuConfigIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "menuconfig",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string MenuConfigCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "menuconfig",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string MenuConfigEditUrl(this UrlHelper helper, object routeValue = null, string controller = "menuconfig",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string MenuConfigDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "menuconfig",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}