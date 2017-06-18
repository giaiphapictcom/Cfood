using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductColorUrlHelper
    {
        public static string ProductColorIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "productcolor",
         string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductColorCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "productcolor",
         string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductColorEditUrl(this UrlHelper helper, object routeValue = null, string controller = "productcolor",
         string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductColorDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "productcolor",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }

    }

}