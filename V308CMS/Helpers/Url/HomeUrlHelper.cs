using System.Web.Mvc;
using V308CMS.Data.Enum;

namespace V308CMS.Helpers.Url
{
    public static class HomeUrlHelper
    {
        public static string HomeIndexUrl(this UrlHelper helper, string controller = "home",
         string action = "index")
        {
            return helper.Action(action, controller);
        }

        public static string HomeSearchUrl(this UrlHelper helper, string keyword="", int page =1, string controller = "home",
        string action = "search")
        {
            return helper.Action(action, controller, new
            {
                q = keyword, page
            });
        }

        public static string HomeCategoryUrl(this UrlHelper helper, 
            string filter = "",int categoryId = 0,  int sort = (int)SortEnum.Default, 
            int page = 1,
            string controller = "home", string action = "category")
            {

            return helper.Action(action, controller, new
            {
                categoryId,
                filter,
                sort,
                page              
            });
         }
    }
}