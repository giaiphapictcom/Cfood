using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Helpers.Url
{
    public static class NewsUrlHelper
    {
        public static string NewsIndexUrl(this UrlHelper helper, int page = 1, string controller = "news", string action = "index")
        {
            return page ==1?"/tin-tuc.html": $"/tin-tuc/trang-{page}.html";
        }
        
        public static string NewsDetailUrl(this UrlHelper helper, int newsId, string controller = "news", string action = "detail")
        {
            return helper.Action(action, controller, new { newsId});
        }

        public static string NewsDetailUrl(this UrlHelper helper, News newsItem, string controller = "news",
            string action = "detail")
        {
        
            return $"/tin-tuc/{newsItem.Title.ToSlug()}.{newsItem.ID}.html";
        }

        public static string NewsTagUrl(this UrlHelper helper, string tag,int page =1, string controller = "news",
            string action = "ListByTag")
        {
            return helper.Action(action, controller, new
            {
                tag,
                page
            });
        }
        


    }

}