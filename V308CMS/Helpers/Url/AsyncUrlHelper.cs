using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class AsyncUrlHelper
    {
        public static string LoadListBrandAsyncUrl(this UrlHelper helper, int categoryId, int limit =6, string controller = "Async", string action = "LoadListBrandAsync")
        {
            return helper.Action(action, controller, new { categoryId, limit });
        }
        public static string LoadListProductByCategoryAsyncUrl(this UrlHelper helper, int categoryId, int limit = 6, string controller = "Async", string action = "LoadListProductByCategoryAsync")
        {
            return helper.Action(action, controller, new { categoryId, limit });
        }
    }
}