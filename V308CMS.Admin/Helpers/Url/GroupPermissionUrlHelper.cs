using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class GroupPermissionUrlHelper
    {
        public static string GroupPermissionIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "grouppermission",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string GroupPermissionCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "grouppermission",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string GroupPermissionEditUrl(this UrlHelper helper, object routeValue = null, string controller = "grouppermission",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string GroupPermissionDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "grouppermission",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}