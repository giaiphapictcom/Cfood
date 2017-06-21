using System.Web.Mvc;
using System.Web;
namespace V308CMS.Admin.Helpers.Url
{
    public static class NewsUrlHelper
    {
        public static string NewsIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "news",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string AffiliateNewsIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "news",
            string action = "AffiliateIndex")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string NewsCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "news",
           string action = "create")
        {
            switch (HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString())
            {
                case "AffiliateIndex":
                    action = "affiliateCreate";
                    break;
            }
            return helper.Action(action, controller, routeValue);
        }

        public static string NewsEditUrl(this UrlHelper helper, object routeValue = null, string controller = "news",
        string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string NewsDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "news",
        string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }


    }
}