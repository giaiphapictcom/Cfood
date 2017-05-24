using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class UrlContentHelper
    {
        public static string CreateNewImageUrl(this UrlHelper helper)
        {
            return "/Content/Images/them-moi.png";
        }
    }
}