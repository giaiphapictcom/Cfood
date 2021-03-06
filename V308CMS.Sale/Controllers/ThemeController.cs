﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Respository;

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
        AccountRepository accountRepos;
        MenuConfigRespository MenuRepos;

        private void CreateRepos()
        {
            mEntities = new V308CMSEntities();
            ProductRepos = new ProductRepository(mEntities);
            imagesRepos = new ImagesRepository(mEntities);
            NewsRepos = new NewsRepository(mEntities);
            accountRepos = new AccountRepository(mEntities);
            MenuRepos = new MenuConfigRespository(mEntities);

        }
        private void DisposeRepos()
        {
           
            ProductRepos.Dispose();
            imagesRepos.Dispose();
            NewsRepos.Dispose();
            accountRepos.Dispose();
            MenuRepos.Dispose();
            mEntities.Dispose();

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
                
                Model.menu = MenuRepos.GetAll("affiliate");

                //NewsGroups MenuCategory = NewsRepos.SearchNewsGroup("menu-affiliate");
                //if (MenuCategory != null)
                //{
                //    //Model.menu = NewsRepos.GetNewsGroup(MenuCategory.ID, true, 6);
                    
                //}
                if (HttpContext.User.Identity.IsAuthenticated == true && Session["UserId"] != null)
                {
                    //lay thong tin chi tiet user
                    Model.Account = accountRepos.LayTinTheoId(Int32.Parse(Session["UserId"].ToString()));
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

        public ActionResult Footer()
        {
            CreateRepos();
            try
            {
                PageFooterControl Model = new PageFooterControl();
                List<NewsGroupPage> NewsCategorys = new List<NewsGroupPage>(); ;

                NewsGroups footerCate = NewsRepos.SearchNewsGroup("footer-affiliate");
                if (footerCate != null)
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

        public ActionResult BlockNews12(News article=null)
        {
            
            try
            {
                CreateRepos();
                if (article == null || article.ID < 1)
                {
                    article = NewsRepos.SearchNews("affiliate-co-gi-khac-biet");
                }
                string view = "~/Views/" + MainController + "/Blocks/BlockNews12.cshtml";
                return View(view, article);
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
    }
}
