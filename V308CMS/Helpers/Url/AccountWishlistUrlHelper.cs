using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class AccountWishlistUrlHelper
    {
        public static string WishlistIndexUrl(this UrlHelper helper, string controller = "accountwishlist", string action = "listproduct")
        {
            return helper.Action(action, controller);
        }
    }
}