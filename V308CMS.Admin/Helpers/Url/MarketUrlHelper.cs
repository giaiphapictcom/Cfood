using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class MarketUrlHelper
    {
        public static string MarketIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "market",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string MarketCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "market",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}