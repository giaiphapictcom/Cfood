using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class EmailConfigUrlHelper
    {
        public static string EmailConfigIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "emailconfig",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string EmailConfigCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "emailconfig",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string EmailConfigEditUrl(this UrlHelper helper, object routeValue = null, string controller = "emailconfig",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string EmailConfigDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "emailconfig",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}