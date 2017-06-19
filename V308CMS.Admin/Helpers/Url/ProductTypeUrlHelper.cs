using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductTypeUrlHelper
    {
        public static string ProductTypeIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "producttype",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductTypeCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "producttype",
          string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string ProductTypeDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "producttype",
          string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductTypeEditUrl(this UrlHelper helper, object routeValue = null, string controller = "producttype",
           string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductTypeChangeStateUrl(this UrlHelper helper, object routeValue = null, string controller = "producttype",
           string action = "changestate")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}