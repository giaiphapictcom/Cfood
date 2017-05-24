using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class FileUrlHelper
    {
        public static string FileIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "file",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string FileCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "file",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}