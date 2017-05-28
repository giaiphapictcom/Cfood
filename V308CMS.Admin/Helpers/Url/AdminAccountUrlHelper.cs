using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class AdminAccountUrlHelper
    {
        public static string AdminAccountIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string AdminAccountCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string AdminAccountInfoUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
          string action = "info")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}