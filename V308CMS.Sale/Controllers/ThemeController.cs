using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Sale.Controllers
{
    public class ThemeController : Controller
    {

        static string MainController = "";
        public ThemeController() {
            MainController = "Affiliate";
        }

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
        

        public ActionResult Header()
        {
            CreateRepos();
            try
            {
                var menu = NewsRepos.GetNewsGroup();

                string view = "~/Views/" + MainController + "/Elements/Header.cshtml";
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

        public ActionResult Footer()
        {
            CreateRepos();
            try
            {
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

                string view = "~/Views/" + MainController + "/Elements/Footer.cshtml";
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

        public ActionResult HomeSlides()
        {
            string view = "~/Views/" + MainController + "/Blocks/HomeSlides.cshtml";
            return View(view);
        }
    }
}
