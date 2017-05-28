using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductManufacturerUrlHelper
    {
        public static string ProductManufacturerIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "productmanufacturer",
         string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductManufacturerCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "productmanufacturer",
         string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
    }

}