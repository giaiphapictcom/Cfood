using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class VideoUrlHelper
    {
        public static string VideoIndexUrl(this UrlHelper helper, string controllerName = "video", string actionName = "index")
        {
            return helper.Action(actionName, controllerName);

        }
    }
}