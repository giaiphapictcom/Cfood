using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Helpers
{
    public static class MemberWishListUrlHelper
    {
        public static string MemberWishListIndexUrl(this UrlHelper helper, string controller = "memberwishlist",
        string action = "index")
        {
            return helper.Action(action, controller);
        }

    }
}