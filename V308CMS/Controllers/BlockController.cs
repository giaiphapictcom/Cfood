using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    public class BlockController : Controller
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
        

        #region Common actions for all Pages
        public ActionResult Resources()
        {
            string view = "~/Views/themes/" + Theme.domain + "/Layout/Resources.cshtml";
            return View(view);
        }

        public ActionResult MainMenu()
        {
            CreateRepos();
            try
            {
                var menu = NewsRepos.GetNewsGroup();
                string view = "~/Views/themes/" + Theme.domain + "/Blocks/MainMenu.cshtml";
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

        public ActionResult LeftColumn()
        {
            CreateRepos();
            try
            {
                PageLeftColControl Model = new PageLeftColControl();
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

                        CategoryPages.Add(categoryPage);
                    }
                }
                Model.LinkCategorys = CategoryPages;
                Model.PromotionHot = ProductRepos.LaySanPhamKhuyenMai(1,3);
                Model.Recommend = ProductRepos.getProductsRandom(9);

                NewsGroups videoGroup = NewsRepos.SearchNewsGroup("video");
                if (videoGroup != null)
                {
                    Model.News = NewsRepos.LayTinTheoGroupId(videoGroup.ID);
                }


                string view = "~/Views/themes/" + Theme.domain + "/Blocks/LeftColumn.cshtml";
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
        public ActionResult Header()
        {
            var shoppingCart = ShoppingCart.Instance;
            var result = new Models.ShoppingCartModels
            {
                item_count = shoppingCart.Items.Count,
                items = shoppingCart.Items.Select(product => new ProductsCartModels
                {
                    Id = product.ProductItem.Id,
                    Url = url.productURL(product.ProductItem.Name, product.ProductItem.Id),
                    Title = product.ProductItem.Name,
                    Quanity = product.Quantity,
                    Image = product.ProductItem.Avatar,
                    Price = product.ProductItem.Price.ToString("N0")
                }).ToList(),
                total_price = shoppingCart.SubTotal

            };
          
            string view = "~/Views/themes/" + Theme.domain + "/Blocks/Header.cshtml";
            return View(view, result);
        }
        public ActionResult Footer()
        {
            CreateRepos();
            try {
                PageFooterControl Model = new PageFooterControl();
                List<NewsGroupPage> NewsCategorys = new List<NewsGroupPage>(); ;

                NewsGroups footerCate = NewsRepos.SearchNewsGroup("footer");
                if (footerCate.ID > 0)
                {
                    List<NewsGroups> categorys = NewsRepos.GetNewsGroup(footerCate.ID, true, 3);
                    if (categorys.Count() > 0)
                    {
                        foreach (NewsGroups cate in categorys)
                        {
                            NewsGroupPage NewsCategory = new NewsGroupPage();
                            NewsCategory.Name = cate.Name;
                            NewsCategory.NewsList = NewsRepos.LayDanhSachTinMoiNhatTheoGroupId(5, cate.ID);
                            NewsCategorys.Add(NewsCategory);
                        }
                    }
                }
                Model.NewsCategorys = NewsCategorys;

                NewsGroups WhoSale = NewsRepos.LayNhomTinAn(29);
                if (WhoSale.ID > 0)
                {
                    NewsGroupPage WhoSalePage = new NewsGroupPage();
                    WhoSalePage.Name = WhoSale.Name;
                    WhoSalePage.NewsList = NewsRepos.LayDanhSachTinMoiNhatTheoGroupId(5, WhoSale.ID);

                    Model.CategoryWhoSale = WhoSalePage;
                }

                NewsGroups MenusFooter = NewsRepos.SearchNewsGroup("MenusFooter");
                if (MenusFooter != null && MenusFooter.ID > 0)
                {
                    Model.MenusFooter = NewsRepos.GetNewsGroup(MenusFooter.ID, true, 6);
                }
                
                string view = "~/Views/themes/" + Theme.domain + "/Blocks/Footer.cshtml";

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
        
        #endregion

        #region Action for Home Page
        
        public ActionResult HomeSlides()
        {
            string view = "~/Views/themes/" + Theme.domain + "/Blocks/HomeSlides.cshtml";
            return View(view);
        }
        public ActionResult HomeAdsProduct()
        {
            CreateRepos();
            try {
                var images = imagesRepos.GetImagesByGroupAlias("home-product", 2);
                string view = "~/Views/themes/" + Theme.domain + "/Ads/HomeProduct.cshtml";
                return View(view, images);
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
        public ActionResult YoutubeBlock(V308CMS.Data.News video)
        {
            string view = "~/Views/themes/" + Theme.domain + "/Product_div/Youtube.cshtml";
            return View(view, video);
        }
        
        #endregion


        #region block View Product item
        public ActionResult ProductBlockType1(Product pro) {
            string view = "~/Views/themes/" + Theme.domain + "/Product_div/Type1.cshtml";
            return View(view, pro);
        }
        public ActionResult ProductBlockType1_4(Product pro)
        {
            string view = "~/Views/themes/" + Theme.domain + "/Product_div/Type1_4.cshtml";
            return View(view, pro);
        }
        public ActionResult ProductBlockType2(Product pro)
        {
            string view = "~/Views/themes/" + Theme.domain + "/Product_div/Type2.cshtml";
            return View(view, pro);
        }
        public ActionResult ProductDetailSlide(Product pro)
        {
            CreateRepos();
            try {
                ProductSlideShow ProductImages = new ProductSlideShow();

                if (pro.ID > 0)
                {
                    ProductImages.Images = ProductRepos.LayProductImageTheoIDProduct(pro.ID);
                }

                string view = "~/Views/themes/" + Theme.domain + "/Product_div/SlideShowDetail.cshtml";
                return View(view, ProductImages);
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