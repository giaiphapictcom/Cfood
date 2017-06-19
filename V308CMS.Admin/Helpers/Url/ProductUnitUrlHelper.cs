using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductUnitUrlHelper
    {
        public static string ProductUnitIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "productunit",
         string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductUnitCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "productunit",
         string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductUnitEditUrl(this UrlHelper helper, object routeValue = null, string controller = "productunit",
         string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductUnitDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "productunit",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }

}