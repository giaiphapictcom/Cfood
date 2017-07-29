using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Enum;

namespace V308CMS.Helpers.Url
{
    public static class ProductUrlHelper
    {
        public static string ProductDetailUrl(this UrlHelper helper, Product product)
        {
            return url.productURL(product.Name, product.ID);

        }

        public static string ProductCategoryUrl(this UrlHelper helper, ProductType category)
        {
            return url.productCategoryURL(category.Name, category.ID);
        }

        public static string ProductSaleOffUrl(this UrlHelper helper, int saleOff = 15, int sort = (int)SortEnum.Default,
            int page = 1,
            string controller = "product", string action = "bigSale")
        {
            return helper.Action(action, controller, new
            {
                saleOff,
                sort,
                page
            });
        }

    }
}