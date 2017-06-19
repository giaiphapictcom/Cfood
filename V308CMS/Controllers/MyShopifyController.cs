using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Respository;

namespace V308CMS.Controllers
{
    public class MyShopifyController : Controller
    {

        #region Repository
        static V308CMSEntities mEntities;
        ImagesRepository ImageRepos;
        ProductTypeRepository ProductTypeRepos;
        ProductRepository ProductRepos;
        MenuConfigRespository MenuConfigRepos;
        NewsRepository NewsRepos;

        private void CreateRepos()
        {
            mEntities = new V308CMSEntities();
            ImageRepos = new ImagesRepository(mEntities);
            ProductTypeRepos = new ProductTypeRepository(mEntities);
            ProductRepos = new ProductRepository(mEntities);
            NewsRepos = new NewsRepository(mEntities);
            MenuConfigRepos = new MenuConfigRespository(mEntities);
        }
        private void DisposeRepos()
        {
            mEntities.Dispose();
            ImageRepos.Dispose();
            ProductTypeRepos.Dispose();
            ProductRepos.Dispose();
            NewsRepos.Dispose();
            MenuConfigRepos.Dispose();

        }
        #endregion
       

        public ActionResult CategoryMenu()
        {
            
            try
            {
                CreateRepos();
                return View("CategoryMenu", ProductTypeRepos.GetAllWeb());
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public ActionResult Mainmenu()
        {
            try {
                CreateRepos();
                return View("Mainmenu", MenuConfigRepos.GetAll());
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            
        }

        public ActionResult OffCanvas(){
            try
            {
                CreateRepos();
                return View("OffCanvas", MenuConfigRepos.GetAll());
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public ActionResult ProductMost()
        {
            try {
                CreateRepos();
                List<Product> products = ProductRepos.LaySanPhamBanChay(1, 3);
                if (!products.Any())
                {
                    products = ProductRepos.getProductsRandom(3);
                }

                return View("ProductMost", products);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }
           
        }

        public ActionResult ProductHot()
        {
            
            try {
                CreateRepos();
                List<Product> products = ProductRepos.getProductsLastest(10);
                if (!products.Any())
                {
                    products = ProductRepos.getProductsRandom(10);
                }
                List<ProductDetail> productDetails = products.Select(pro => new ProductDetail
                {
                    Product = pro,
                    Images = ProductRepos.LayProductImageTheoIDProduct(pro.ID)
                }).ToList();
                return View("ProductHot", productDetails);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }

        }

        public ActionResult ProductHomeCategory(ProductType cate)
        {
            try {
                CreateRepos();
                ProductCategoryPageContainer model = new ProductCategoryPageContainer();

                List<ProductCategoryPage> mProductPageList = new List<ProductCategoryPage>();
                ProductType productCategory = ProductRepos.LayLoaiSanPhamTheoId(cate.ID);
                if (productCategory != null)
                {
                    List<ProductType> mProductTypeList = ProductRepos.getProductTypeByProductType(productCategory.ID, 3);
                    if (mProductTypeList.Count > 0)
                    {
                        mProductPageList.AddRange(mProductTypeList.Select(it => ProductHelper.GetCategoryPage(it, 1, true)));
                    }
                }
                model.List = mProductPageList;
                model.ProductType = productCategory;
                model.Brands = ProductRepos.getRandomBrands(cate.ID, 12);
                return View("HomeCategory", model);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public ActionResult HomeYoutube()
        {
            try {
                CreateRepos();
                List<News> videos = new List<News>();
                NewsGroups videoGroup = NewsRepos.SearchNewsGroup("video");
                if (videoGroup != null)
                {
                    videos = NewsRepos.LayTinTheoGroupId(videoGroup.ID);
                }
                return View("HomeYoutube", videos);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }
           

        }

        public ActionResult HomeFooter()
        {           
            return View("HomeFooter");
        }

        


        #region HTML view onlye
        public ActionResult QuickView()
        {
            return View("QuickView");
        }
        public ActionResult WapperPopup()
        {
            return View("WapperPopup");
        }
        public ActionResult FillerProductList() {
            return View("ProductList");
        }
        #endregion

        #region Widget Left
        public ActionResult WidgetLeftHotProducts() {
            try {
                CreateRepos();
                List<Product> products = ProductRepos.getProductsRandom(5);
                return View("HotProductLeft", products);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }

        }

        public ActionResult ProductBlockLeft(Product product = null)
        {
            try {
                
                return View("ProductBlockLeft", product);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            
        }

        public ActionResult WidgetLeftAdv()
        {          
            return View("AdvLeft");

        }

        public ActionResult WidgetFilterPrice()
        {           
            return View("Prices");

        }
        public ActionResult WidgetFilterCategory()
        {         
            return View("Categorys");

        }
        public ActionResult WidgetFilterSize()
        {            
            return View("Size");

        }
        public ActionResult WidgetFilterColor()
        {          
            return View("Color");

        }

        public ActionResult WidgetTags()
        {            
            return View("Tags");
        }
        public ActionResult WidgetRecentArticles()
        {            
            return View("RecentArticles");
        }
        #endregion

        #region Adv banner

        public ActionResult CategoryAdv() {

            try {
                CreateRepos();
                var images = ImageRepos.GetImagesByGroupAlias("category-adv");
            if (images.Count() > 0)
            {
                Image img = images.First();
                return View("CategoryAdv", img);
            }
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            return View("CategoryAdv");
            
        }
        public ActionResult LeftProductAdv()
        {
            
            try
            {
                CreateRepos();
                var images = ImageRepos.GetImagesByGroupAlias("product-col-left");
                if (images.Count() > 0)
                {
                    Image img = images.First();
                    return View("LeftProductAdv", img);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            return View("LeftProductAdv");
            
        }
        
        
        #endregion
    }
}