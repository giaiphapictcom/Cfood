using System.Web.Mvc;
using System.Web;

namespace V308CMS.Admin.Helpers.Url
{
    public static class BannerUrlHelper
    {
        public static string BannerIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "banner",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string BannersAffiliateIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "banner",
           string action = "affiliatebanner")
        {
            return helper.Action(action, controller, routeValue);
        }


        public static string BannerCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "banner",
           string action = "create")
        {

            switch (HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString())
            {
                case "affiliatebanner":
                    action = "affiliateCreate";
                    break;
            }
            return helper.Action(action, controller, routeValue);
        }
        public static string BannerEditUrl(this UrlHelper helper, object routeValue = null, string controller = "banner",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string BannerDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "banner",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string BannerChangeStatusUrl(this UrlHelper helper, object routeValue = null, string controller = "banner",
        string action = "changestatus")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}