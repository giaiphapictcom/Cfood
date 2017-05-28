using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class SupportUrlHelper
    {
        public static string SupportIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "support",
          string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string SupportCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "support",
          string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string SupportEditUrl(this UrlHelper helper, object routeValue = null, string controller = "support",
           string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}