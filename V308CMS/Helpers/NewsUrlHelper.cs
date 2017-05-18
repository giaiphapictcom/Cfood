using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS.Helpers
{
    public static partial class MpStartUrlHelper
    {
        public static string NewsIndexUrl(this UrlHelper helper, int page = 1, string controller = "news", string action = "index")
        {
            return helper.Action(action, controller,page);
        }
        
        public static string NewsDetailUrl(this UrlHelper helper, int newsId, string controller = "news", string action = "detail")
        {
            return helper.Action(action, controller, new { newsId});
        }

        public static string NewsDetailUrl(this UrlHelper helper, News newsItem, string controller = "news",
            string action = "detail")
        {

            return $"/tin-tuc/{newsItem.Title.ToSlug()}.{newsItem.ID}";
        }
        


    }

}