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
        #region Repository
        static V308CMSEntities mEntities;
        static ProductRepository ProductRepos;

        private static void CreateRepos()
        {
            mEntities = new V308CMSEntities();
            ProductRepos = new ProductRepository(mEntities);

        }
        private static void DisposeRepos()
        {
            mEntities.Dispose();
            ProductRepos.Dispose();
        }
        #endregion

        public static int ProductShowLimit = 18;

        public static ProductCategoryPage GetCategoryPage(ProductType ProductCategory,int nPage = 1)
        {
            CreateRepos();
            ProductCategoryPage ModelPage = new ProductCategoryPage();
            try{
                
                ModelPage.Name = ProductCategory.Name;
                ModelPage.Id = (int)ProductCategory.ID;
                ModelPage.Image = ProductCategory.Image;
                ModelPage.ProductTotal = ProductRepos.getProductTotal(ProductCategory.ID, ProductCategory.Level);
                if (nPage * ProductShowLimit > ModelPage.ProductTotal)
                {
                    nPage = 1;
                }
                ModelPage.Paging = ModelPage.ProductTotal > ProductShowLimit;

                List<Product> ProductItems = ProductRepos.LayTheoTrangAndType(nPage, ProductShowLimit, ProductCategory.ID, ProductCategory.Level);
                ModelPage.List = ProductItems;

                
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                DisposeRepos();
            }
            return ModelPage;
        }

        public static CategoryPage getProductsByCategory(int CategoryID, int nPage = 1)
        {
            CreateRepos();
            CategoryPage ModelPage = new CategoryPage();
            try {

                ModelPage.Products = ProductRepos.getProductsByCategory(CategoryID, ProductShowLimit, nPage);
                ModelPage.ProductTotal = ProductRepos.getProductTotalByCategory(CategoryID);

                
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                DisposeRepos();
            }
            return ModelPage;            
        }

        
    }
}