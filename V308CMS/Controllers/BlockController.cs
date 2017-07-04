using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Models;
using V308CMS.Data.Models;

namespace V308CMS.Controllers
{
    public class BlockController : BaseController
    {
       
        #region Common actions for all Pages
        public ActionResult Resources()
        {         
            return View("Resources");
        }
        [ChildActionOnly]
        public ActionResult MainMenu()

        {
          
            return View("MainMenu", MenuConfigRepos.GetAll(Data.Helpers.Site.home));
        }

        public ActionResult MenuCanvas()
        {

            return View("MenuCanvas", MenuConfigRepos.GetAll(Data.Helpers.Site.home));
        //{          
        //    return View("MainMenu", NewsService.GetNewsGroup());
        }

        public ActionResult LeftColumn()
        {

            var model = new PageLeftColControl();
            var categoryPages = new List<ProductTypePage>();

            var catetorys = ProductsService.getProductTypeParent();

            if (catetorys.Any())
            {
                categoryPages.AddRange(catetorys.Select(cate => new ProductTypePage
                {
                    Id = cate.ID,
                    Image = cate.ImageBanner,
                    Name = cate.Name,
                    Icon = cate.Icon
                }));
            }
            model.LinkCategorys = categoryPages;
            model.PromotionHot = ProductsService.LaySanPhamKhuyenMai(1, 3);
            model.Recommend = ProductsService.getProductsRandom(9);

            NewsGroups videoGroup = NewsService.SearchNewsGroup("video");
            if (videoGroup != null)
            {
                model.News = NewsService.LayTinTheoGroupId(videoGroup.ID,2);
            }                     
            return View("LeftColumn", model);
          
        }
        [ChildActionOnly]
        public ActionResult Header()
        {           
            var result =  new ShoppingCartModels
            {
                item_count = MyCart.Items.Count,
                items = MyCart.Items.Select(product => new ProductsCartModels
                {
                    Id = product.ProductItem.Id,
                    Url = url.productURL(product.ProductItem.Name, product.ProductItem.Id),
                    Title = product.ProductItem.Name,
                    Quanity = product.Quantity,
                    Image = product.ProductItem.Avatar,
                    Price = product.ProductItem.Price.ToString("N0")
                }).ToList(),
                total_price = MyCart.SubTotal

            };                        
            return View("Header", result);
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new PageFooterControl();
            var newsCategorys = new List<NewsGroupPage>(); ;

            var footerCate = NewsService.SearchNewsGroup("footer");
            if (footerCate !=null && footerCate.ID > 0)
            {
                var categorys = NewsService.GetNewsGroup(footerCate.ID, true, 3);
                if (categorys.Any())
                {
                    newsCategorys.AddRange(categorys.Select(cate => new NewsGroupPage
                    {
                        Name = cate.Name, NewsList = NewsService.LayDanhSachTinMoiNhatTheoGroupId(5, cate.ID)
                    }));
                }
            }
            model.NewsCategorys = newsCategorys;
            var whoSale = NewsService.LayNhomTinAn(29);
            if (whoSale.ID > 0)
            {
                var whoSalePage = new NewsGroupPage
                {
                    Name = whoSale.Name,
                    NewsList = NewsService.LayDanhSachTinMoiNhatTheoGroupId(5, whoSale.ID)
                };

                model.CategoryWhoSale = whoSalePage;
            }

            var  menusFooter = NewsService.SearchNewsGroup("MenusFooter");
            if (menusFooter != null && menusFooter.ID > 0)
            {
                model.MenusFooter = NewsService.GetNewsGroup(menusFooter.ID, true, 6);
            }                
            return View("Footer", model);
        }

        #endregion

        #region Action for Home Page
        [ChildActionOnly]
        public ActionResult HomeSlides()
        {

            Banner model = new Banner();
            try {
                var banners = BannerService.GetList(-1, Data.Helpers.Site.home, true, 1);
                if (banners.Count() > 0) {
                    model = banners.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return View("HomeSlides", model);
        }
        public ActionResult HomeAdsProduct()
        {
            var images = ImagesService.GetImagesByGroupAlias("home-product", 2);
            // ReSharper disable once Mvc.ViewNotResolved            
            return View("HomeProduct", images);
        }
        public ActionResult VideoItemBlock(V308CMS.Data.News video)
        {
            return View("VideoItemBlock", video);
        }
        public ActionResult YoutubeBlock(V308CMS.Data.News video)
        {
            return View("VideoItemBlock", video);
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
            ProductSlideShow productImages = new ProductSlideShow();

            if (pro.ID > 0)
            {
                productImages.Images = ProductsService.LayProductImageTheoIDProduct(pro.ID);
            }

            string view = "~/Views/themes/" + Theme.domain + "/Product_div/SlideShowDetail.cshtml";
            return View(view, productImages);
        }
        
        #endregion

    }
}