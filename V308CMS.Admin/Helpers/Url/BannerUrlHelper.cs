using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class BannerUrlHelper
    {
        public static string BannerIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "banner",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string BannerCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "banner",
           string action = "create")
        {
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