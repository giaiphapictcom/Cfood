using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class RoleUrlHelper
    {
        public static string RoleIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "role",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string RoleCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "role",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string RoleEditUrl(this UrlHelper helper, object routeValue = null, string controller = "role",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string RoleDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "role",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}