using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductOrderUrlHelper
    {
        public static string ProductOrderIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "productorder",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductOrderCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "productorder",
            string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductOrderEditUrl(this UrlHelper helper, object routeValue = null, string controller = "productorder",
           string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductOrderExportToExcelUrl(this UrlHelper helper, object routeValue = null, string controller = "productorder",
           string action = "orderexporttoexcel")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}