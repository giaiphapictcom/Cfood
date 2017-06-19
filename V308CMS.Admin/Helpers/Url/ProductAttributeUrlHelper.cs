using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductAttributeUrlHelper
    {
        public static string ProductAttributeDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "productattribute",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}