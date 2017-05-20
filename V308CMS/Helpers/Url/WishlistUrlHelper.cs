using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class WishlistUrlHelper
    {
        public static string WishlistIndexUrl(this UrlHelper helper, string controller = "wishlist", string action = "listproduct")
        {
            return helper.Action(action, controller);
        }
    }
}