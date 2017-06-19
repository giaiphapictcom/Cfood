using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class PermissionUrlHelper
    {
        public static string PermissionIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "permission",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string PermissionCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "permission",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string PermissionEditUrl(this UrlHelper helper, object routeValue = null, string controller = "permission",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string PermissionDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "permission",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}