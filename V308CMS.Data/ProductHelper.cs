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

        public static ProductCategoryPage GetCategoryPage(ProductType ProductCategory,int nPage = 1,bool includeProductImages =false)
        {
            CreateRepos();
            ProductCategoryPage ModelPage = new ProductCategoryPage();
            try{
                
                ModelPage.Name = ProductCategory.Name;
                ModelPage.Id = (int)ProductCategory.ID;
                ModelPage.Image = ProductCategory.Image;

                var products = from p in mEntities.Product
                              
                               where p.Type == ProductCategory.ID && p.Status == true
                                orderby p.ID descending
                                select p

                                ;

                

                
                ModelPage.ProductTotal = products.Count();
                if (nPage * ProductShowLimit > ModelPage.ProductTotal)
                {
                    nPage = 1;
                }
                ModelPage.Paging = ModelPage.ProductTotal > ProductShowLimit;
                
                ModelPage.List = products.Skip((nPage - 1) * ProductShowLimit).Take(ProductShowLimit).ToList(); ;

                
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                //DisposeRepos();
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
                //DisposeRepos();
            }
            return ModelPage;            
        }

        public static List<ProductImage> getProductImages(int? ProductID, int limit = 0)
        {
            CreateRepos();
            List<ProductImage> images = null;
            try
            {
                var imgEntities = (from img in mEntities.ProductImage
                          where img.ProductID == ProductID
                          orderby img.ID descending
                          select img);

                if (limit > 0)
                {
                    images = imgEntities.Take(limit).ToList();
                }
                else {
                    images = imgEntities.ToList();
                }
                return images;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
            finally
            {
                //DisposeRepos();
            }
            
        }

        
    }
}