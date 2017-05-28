using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class SupportTypeUrlHelper
    {
        public static string SupportTypeIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "supporttype",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string SupportTypeCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "supporttype",
          string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string SupportTypeEditUrl(this UrlHelper helper, object routeValue = null, string controller = "supporttype",
           string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}