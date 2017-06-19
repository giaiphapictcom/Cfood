using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class SizeUrlHelper
    {
        public static string SizeIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "size",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string SizeCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "size",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string SizeEditUrl(this UrlHelper helper, object routeValue = null, string controller = "size",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string SizeDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "size",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}