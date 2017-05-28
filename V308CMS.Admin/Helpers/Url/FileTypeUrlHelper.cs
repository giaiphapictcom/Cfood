using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class FileTypeUrlHelper
    {
        public static string FileTypeIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "filetype",
          string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string FileTypeCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "filetype",
        string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}