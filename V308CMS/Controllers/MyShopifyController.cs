using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Controllers
{
    public class MyShopifyController : Controller
    {

        #region Repository
        static V308CMSEntities mEntities;
        ProductRepository ProductRepos;
        ImagesRepository imagesRepos;
        NewsRepository NewsRepos;

        private void CreateRepos()
        {
            mEntities = new V308CMSEntities();
            ProductRepos = new ProductRepository(mEntities);
            imagesRepos = new ImagesRepository(mEntities);
            NewsRepos = new NewsRepository(mEntities);

        }
        private void DisposeRepos()
        {
            mEntities.Dispose();
            ProductRepos.Dispose();
            imagesRepos.Dispose();
            NewsRepos.Dispose();
        }
        #endregion
        

        public ActionResult CategoryMenu()
        {
            CreateRepos();
            try {
                List<ProductTypePage> CategoryPages = new List<ProductTypePage>();
                List<ProductType> catetorys = ProductRepos.getProductTypeParent();

                if (catetorys.Count() > 0)
                {
                    foreach (ProductType cate in catetorys)
                    {
                        ProductTypePage categoryPage = new ProductTypePage();
                        categoryPage.Id = cate.ID;
                        categoryPage.Image = cate.ImageBanner;
                        categoryPage.Name = cate.Name;
                        categoryPage.Icon = cate.Icon;
                        categoryPage.CategorySub = ProductRepos.getProductTypeByParent(cate.ID);

                        CategoryPages.Add(categoryPage);
                    }
                }

                string view = "~/Views/themes/" + Theme.domain + "/Menu/CategoryMenu.cshtml";
                return View(view, CategoryPages);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            
        }

        public ActionResult Mainmenu()
        {
            CreateRepos();
            try {
                var menu = NewsRepos.GetNewsGroup();

                string view = "~/Views/themes/" + Theme.domain + "/Menu/Mainmenu.cshtml";
                return View(view, menu);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public ActionResult ProductMost()
        {
            CreateRepos();
            try {
                List<Product> Products = ProductRepos.LaySanPhamBanChay(1, 3);
                if (Products.Count() < 1)
                {
                    Products = ProductRepos.getProductsRandom(3);
                }

                string view = "~/Views/themes/" + Theme.domain + "/Product/ProductMost.cshtml";
                return View(view, Products);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public ActionResult ProductHot()
        {
            CreateRepos();
            try {
                List<ProductDetail> ProductDetails = new List<ProductDetail>();
                List<Product> Products = ProductRepos.getProductsLastest(10);
                if (Products.Count() < 1)
                {
                    Products = ProductRepos.getProductsRandom(10);
                }


                foreach (Product pro in Products)
                {
                    ProductDetail product = new ProductDetail();
                    product.Product = pro;
                    product.Images = ProductRepos.LayProductImageTheoIDProduct(pro.ID);
                    ProductDetails.Add(product);
                }
                string view = "~/Views/themes/" + Theme.domain + "/Product/ProductHot.cshtml";
                return View(view, ProductDetails);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            
        }

        public ActionResult ProductHomeCategory(ProductType cate)
        {
            CreateRepos();
            try {
                ProductCategoryPageContainer Model = new ProductCategoryPageContainer();
                List<ProductCategoryPage> mProductPageList = new List<ProductCategoryPage>();
                ProductType ProductCategory = ProductRepos.LayLoaiSanPhamTheoId(cate.ID);
                if (ProductCategory != null)
                {
                    List<ProductType> mProductTypeList = ProductRepos.getProductTypeByProductType(ProductCategory.ID, 3);
                    if (mProductTypeList.Count > 0)
                    {
                        //nPage = 1;
                        foreach (ProductType it in mProductTypeList)
                        {
                            mProductPageList.Add(ProductHelper.GetCategoryPage(it, 1));
                        }
                    }
                }
                Model.List = mProductPageList;
                Model.ProductType = ProductCategory;

                string view = "~/Views/themes/" + Theme.domain + "/Product/HomeCategory.cshtml";
                return View(view, Model);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            
        }

        public ActionResult HomeYoutube()
        {
            CreateRepos();
            try {
                List<News> videos = new List<News>();
                NewsGroups videoGroup = NewsRepos.SearchNewsGroup("video");
                if (videoGroup != null)
                {
                    videos = NewsRepos.LayTinTheoGroupId(videoGroup.ID);
                }

                string view = "~/Views/themes/" + Theme.domain + "/Blocks/HomeYoutube.cshtml";
                return View(view, videos);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            
        }

        public ActionResult HomeFooter()
        {
            string view = "~/Views/themes/" + Theme.domain + "/Blocks/HomeFooter.cshtml";
            return View(view);
        }

        public ActionResult QuickView()
        {
            string view = "~/Views/themes/" + Theme.domain + "/Product/QuickView.cshtml";
            return View(view);
        }
        public ActionResult WapperPopup()
        {
            string view = "~/Views/themes/" + Theme.domain + "/Blocks/WapperPopup.cshtml";
            return View(view);
        }


        #region Widget Left
        public ActionResult WidgetLeftHotProducts() {
            CreateRepos();
            try {
                List<Product> products = ProductRepos.getProductsRandom(5);
                string view = "~/Views/themes/" + Theme.domain + "/Widget/HotProductLeft.cshtml";
                return View(view, products);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            
        }

        public ActionResult WidgetLeftAdv()
        {
            CreateRepos();
            try
            {
                string view = "~/Views/themes/" + Theme.domain + "/Widget/AdvLeft.cshtml";
                return View(view);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }

        }

        public ActionResult WidgetFilterPrice()
        {
            CreateRepos();
            try
            {
                string view = "~/Views/themes/" + Theme.domain + "/Blocks/Filter/Prices.cshtml";
                return View(view);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }

        }
        public ActionResult WidgetFilterCategory()
        {
            CreateRepos();
            try
            {
                string view = "~/Views/themes/" + Theme.domain + "/Blocks/Filter/Categorys.cshtml";
                return View(view);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }

        }
        public ActionResult WidgetFilterSize()
        {
            CreateRepos();
            try
            {
                string view = "~/Views/themes/" + Theme.domain + "/Blocks/Filter/Size.cshtml";
                return View(view);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }

        }
        public ActionResult WidgetFilterColor()
        {
            CreateRepos();
            try
            {
                string view = "~/Views/themes/" + Theme.domain + "/Blocks/Filter/Color.cshtml";
                return View(view);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }

        }

        public ActionResult WidgetTags()
        {
            CreateRepos();
            try
            {
                string view = "~/Views/themes/" + Theme.domain + "/Blocks/Widget/Tags.cshtml";
                return View(view);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }

        }
        public ActionResult WidgetRecentArticles()
        {
            CreateRepos();
            try
            {
                string view = "~/Views/themes/" + Theme.domain + "/Blocks/Widget/RecentArticles.cshtml";
                return View(view);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }

        }
        #endregion

    }
}