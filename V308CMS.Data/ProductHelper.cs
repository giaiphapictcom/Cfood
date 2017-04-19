using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Collections.Specialized;

namespace V308CMS.Data
{
    public class ProductHelper
    {
        static V308CMSEntities mEntities = new V308CMSEntities();
        static ProductRepository productRepos = new ProductRepository(mEntities);
        public static int ProductShowLimit = 9;

        public static ProductCategoryPage GetCategoryPage(ProductType ProductCategory,int nPage = 1)
        {
            
            ProductCategoryPage ModelPage = new ProductCategoryPage();
            ModelPage.Name = ProductCategory.Name;
            ModelPage.Id = (int)ProductCategory.ID;
            ModelPage.Image = ProductCategory.Image;
            ModelPage.ProductTotal = productRepos.getProductTotal(ProductCategory.ID, ProductCategory.Level);
            if (nPage * ProductShowLimit > ModelPage.ProductTotal) {
                nPage = 1;
            }
            ModelPage.Paging = ModelPage.ProductTotal > ProductShowLimit;

            List<Product> ProductItems = productRepos.LayTheoTrangAndType(nPage, ProductShowLimit, ProductCategory.ID, ProductCategory.Level);
            ModelPage.List = ProductItems;

            return ModelPage;
        }
    }
}