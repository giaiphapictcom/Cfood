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

        public static string LoadListProductBestSellerAsyncUrl(this UrlHelper helper, int categoryId, int limit = 5,  string controller = "Async", string action = "LoadProductsBestSellerAsync")
        {
            return helper.Action(action, controller, new { categoryId, limit });
        }


        public static string LoadListProductBrandFilterAsyncUrl(this UrlHelper helper, int categoryId, string controller = "Async", string action = "LoadListProductBrandFilterAsync")
        {
            return helper.Action(action, controller, new { categoryId });
        }

        public static string LoadListLoadListManufacturerFilterAsyncUrl(this UrlHelper helper, string controller = "Async", string action = "LoadListManufacturerFilterAsync")
        {
            return helper.Action(action, controller);
        }
    }
}