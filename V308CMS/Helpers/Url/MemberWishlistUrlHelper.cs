using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class MemberWishlistUrlHelper
    {
        public static string MemberWishlistIndexUrl(this UrlHelper helper, string controller = "MemberWishList",
           string action = "index")
        {
            return helper.Action(action, controller);
        }

    }
}