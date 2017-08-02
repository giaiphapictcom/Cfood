using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Models;
using V308CMS.Data.Helpers;
using V308CMS.Data.Enum;

namespace V308CMS.Sale.Controllers
{
    public class ThemeController : BaseController
    {

        static string MainController = "";
        public ThemeController() {
            MainController = "Affiliate";
        }

        public ActionResult CategoryMenu()
        {
            CreateRepos();
            try
            {
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

        public ActionResult Resources()
        {
            return View("~/Views/" + MainController + "/Elements/Resources.cshtml");
        }
        public ActionResult AdminMenu()
        {
            return View("~/Views/" + MainController + "/Elements/AdminMenu.cshtml");
        }


        public class HeaderPage
        {
            public Account Account { get; set; }
            public bool IsAuthenticated { get; set; }
            public List<V308CMS.Data.Models.MenuConfig> menu { get; set; }
        }

        public ActionResult Header()
        {
            
            try
            {
                CreateRepos();
                //var menu = NewsRepos.GetNewsGroup();
                HeaderPage Model = new HeaderPage();

                Model.menu = MenuRepos.GetList(1,5,"affiliate");

                //NewsGroups MenuCategory = NewsRepos.SearchNewsGroup("menu-affiliate");
                //if (MenuCategory != null)
                //{
                //    //Model.menu = NewsRepos.GetNewsGroup(MenuCategory.ID, true, 6);
                    
                //}
                if (HttpContext.User.Identity.IsAuthenticated == true && Session["UserId"] != null)
                {
                    //lay thong tin chi tiet user
                    Model.Account = AccountRepos.LayTinTheoId(Int32.Parse(Session["UserId"].ToString()));
                    Model.IsAuthenticated = true;
                }

                string view = "~/Views/" + MainController + "/Elements/Header.cshtml";
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

        public ActionResult MenuCanvas()
        {

            try
            {
                CreateRepos();
                HeaderPage Model = new HeaderPage();
                Model.menu = MenuRepos.GetList(1, 5, "affiliate");

                string view = "~/Views/" + MainController + "/Elements/MenuCanvas.cshtml";
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
        public ActionResult Footer()
        {
            CreateRepos();
            try
            {
                PageFooterControl Model = new PageFooterControl();
                List<NewsGroupPage> NewsCategorys = new List<NewsGroupPage>(); ;

                //NewsGroups footerCate = NewsRepos.SearchNewsGroup("footer-affiliate");
                //if (footerCate != null)
                //{
                List<NewsGroups> categorys = NewsGroupRepos.GetAll(true, "affiliate",true);
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
                //}
                Model.NewsCategorys = NewsCategorys;

                //NewsGroups WhoSale = NewsRepos.LayNhomTinAn(29);
                //if (WhoSale.ID > 0)
                //{
                //    NewsGroupPage WhoSalePage = new NewsGroupPage();
                //    WhoSalePage.Name = WhoSale.Name;
                //    WhoSalePage.NewsList = NewsRepos.LayDanhSachTinMoiNhatTheoGroupId(5, WhoSale.ID);

                //    Model.CategoryWhoSale = WhoSalePage;
                //}

                string view = "~/Views/" + MainController + "/Elements/Footer.cshtml";
                return View(view, Model);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                //DisposeRepos();
            }

            
        }

        public ActionResult HomeSlides()
        {
            var position = (int)BannerPositionEnum.Slide;
            var items = BannerRepo.GetList(position, Site.affiliate, true,-1);
            string view = "~/Views/" + MainController + "/Blocks/HomeSlides.cshtml";
            return View(view, items);
        }

        public ActionResult NewsBannerBlockRight() {
            var items = BannerRepo.GetList((int)BannerPositionEnum.NewsRight, Site.affiliate, true, 2);
            string view = "~/Views/" + MainController + "/Blocks/BannerBlockRight.cshtml";
            return View(view, items);
        }


        public class PaginationClass
        {
            public int ProductTotal { get; set; }
            public int Page { get; set; }

        }
        public ActionResult BlockPagination(int ProductTotal = 0)
        {
            int nPage = Convert.ToInt32(Request.QueryString["p"]);
            if (nPage < 1)
            {
                nPage = 1;
            }
            PaginationClass Model = new PaginationClass();
            Model.Page = nPage;
            Model.ProductTotal = ProductTotal;


            string view = "~/Views/" + MainController + "/Blocks/BlockPagination.cshtml";
            return View(view, Model);
        }

        public ActionResult BlockNews12(Banner banner=null)
        {
            
            try
            {
                CreateRepos();
                if (banner.Name == null || banner.Name.Length < 1)
                {
                    var banners = BannerRepo.GetList(-1, "affiliate", true, 3);
                    banner = banners.Last();
                }
                string view = "~/Views/" + MainController + "/Blocks/BlockNews12.cshtml";
                return View(view, banner);
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

        public ActionResult SupportRight() {
            string view = "~/Views/" + MainController + "/Elements/SupportRight.cshtml";
            var Model = new SupportMansPage();
            Model.Items = SupportManRepos.GetList(true,Data.Helpers.Site.affiliate);
            return View(view, Model);
        }
    }
}
