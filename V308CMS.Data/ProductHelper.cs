using System.Collections.Generic;
using System.Linq;

//using System.Collections.Specialized;

namespace V308CMS.Data
{
    public class ProductHelper
    {


        public static int ProductShowLimit = 18;

        public static ProductCategoryPage GetCategoryPage(ProductType productCategory, int nPage = 1, bool includeProductImages = false)
        {
            using (var mEntities = new V308CMSEntities())
            {

                var modelPage = new ProductCategoryPage
                {
                    Name = productCategory.Name,
                    Id = (int)productCategory.ID,
                    Image = productCategory.Image
                };

                var products = from p in mEntities.Product
                               where p.Type == productCategory.ID && p.Status == true
                               orderby p.ID descending
                               select p
                                ;
                modelPage.ProductTotal = products.Count();
                if (nPage * ProductShowLimit > modelPage.ProductTotal)
                {
                    nPage = 1;
                }
                modelPage.Paging = modelPage.ProductTotal > ProductShowLimit;

                modelPage.List = products.Skip((nPage - 1) * ProductShowLimit).Take(ProductShowLimit).ToList(); ;
                return modelPage;
            }

        }

        public static CategoryPage getProductsByCategory(int categoryId, int nPage = 1)
        {
            var productRespository = new ProductRepository();
            CategoryPage modelPage = new CategoryPage
            {
                Products = productRespository.getProductsByCategory(categoryId, ProductShowLimit, nPage),
                ProductTotal = productRespository.getProductTotalByCategory(categoryId)
            };
            return modelPage;

        }

        public static List<ProductImage> getProductImages(int? ProductID, int limit = 0)
        {
            using (var mEntities = new V308CMSEntities())
            {
                var imgEntities = (from img in mEntities.ProductImage
                                   where img.ProductID == ProductID
                                   orderby img.ID descending
                                   select img);

                var images = limit > 0 ? imgEntities.Take(limit).ToList() : imgEntities.ToList();
                return images;
            }

        }


    }
}