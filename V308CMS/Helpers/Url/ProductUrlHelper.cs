using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;

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

    }
}