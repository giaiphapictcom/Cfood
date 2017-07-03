using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Respository;

namespace V308CMS.Controllers
{
    public class MyShopifyController : BaseController
    {
        public ActionResult CategoryMenu()
        {

            return View("CategoryMenu", ProductTypeService.GetAllWeb());
        }

        public ActionResult Mainmenu()
        {
            return View("Mainmenu", MenuConfigService.GetAll());

        }

        public ActionResult OffCanvas()
        {
            return View("OffCanvas", MenuConfigService.GetAll());
        }

        public ActionResult ProductMost()
        {
            List<Product> products = ProductsService.LaySanPhamBanChay(1, 3);
            if (!products.Any())
            {
                products = ProductsService.getProductsRandom(3);
            }

            return View("ProductMost", products);
        }

        public ActionResult ProductHot()
        {

            List<Product> products = ProductsService.getProductsLastest(10);
            if (!products.Any())
            {
                products = ProductsService.getProductsRandom(10);
            }
            List<ProductDetail> productDetails = products.Select(pro => new ProductDetail
            {
                Product = pro,
                Images = ProductsService.LayProductImageTheoIDProduct(pro.ID)
            }).ToList();
            return View("ProductHot", productDetails);

        }

        public ActionResult ProductHomeCategory(int categoryId)
        {
            ProductCategoryPageContainer model = new ProductCategoryPageContainer();

            List<ProductCategoryPage> mProductPageList = new List<ProductCategoryPage>();
            ProductType productCategory = ProductsService.LayLoaiSanPhamTheoId(categoryId);
            if (productCategory != null)
            {
                List<ProductType> mProductTypeList = ProductsService.getProductTypeByProductType(productCategory.ID, 3);
                if (mProductTypeList.Count > 0)
                {
                    mProductPageList.AddRange(mProductTypeList.Select(it => ProductHelper.GetCategoryPage(it, 1, true)));
                }
            }
            model.List = mProductPageList;
            model.ProductType = productCategory;
            model.Brands = ProductsService.getRandomBrands(categoryId, 12);
            return View("HomeCategory", model);
        }
        [ChildActionOnly]
        public ActionResult HomeYoutube()
        {
            List<News> videos = new List<News>();
            NewsGroups videoGroup = NewsService.SearchNewsGroupWithNews("video");
            if (videoGroup != null)
            {
                videos = videoGroup.ListNews.ToList();
            }
            return View("HomeYoutube", videos);


        }
        [ChildActionOnly]
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
        public ActionResult FillerProductList()
        {
            return View("ProductList");
        }
        #endregion

        #region Widget Left
        public ActionResult WidgetLeftHotProducts()
        {
            return View("HotProductLeft", ProductsService.getProductsRandom(5));

        }

        public ActionResult ProductBlockLeft(Product product = null)
        {
            return View("ProductBlockLeft", product);

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

        public ActionResult CategoryAdv()
        {

            var images = ImagesService.GetImagesByGroupAlias("category-adv");
            if (images.Any())
            {
                Image img = images.First();
                return View("CategoryAdv", img);
            }
            return View("CategoryAdv");

        }
        public ActionResult LeftProductAdv()
        {

            var images = ImagesService.GetImagesByGroupAlias("product-col-left");
            if (images.Any())
            {
                Image img = images.First();
                return View("LeftProductAdv", img);
            }
            return View("LeftProductAdv");

        }
        #endregion
    }
}