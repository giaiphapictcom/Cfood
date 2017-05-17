using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using System.IO;

namespace V308CMS.Sale.Controllers
{
    public class AffiliateController : Controller
    {
        #region Repository
        static V308CMSEntities mEntities;
        ProductRepository ProductRepos;
        AccountRepository AccountRepos;
        NewsRepository NewsRepos;
        TestimonialRepository CommentRepo;
        CategoryRepository CategoryRepo;
        private void CreateRepos()
        {
            mEntities = new V308CMSEntities();
            ProductRepos = new ProductRepository(mEntities);
            AccountRepos = new AccountRepository(mEntities);
            NewsRepos = new NewsRepository(mEntities);
            CommentRepo = new TestimonialRepository(mEntities);
            CategoryRepo = new CategoryRepository(mEntities);
        }
        private void DisposeRepos()
        {
            mEntities.Dispose();
            ProductRepos.Dispose();
            AccountRepos.Dispose();
            NewsRepos.Dispose();
            CommentRepo.Dispose();
            CategoryRepo.Dispose();
        }
        #endregion


        public ActionResult Home()
        {
            try{
                CreateRepos();
                AffiliateHomePage Model = new AffiliateHomePage();
                Model.VideoCategory = NewsRepos.SearchNewsGroup("Affiliate Video");
                if (Model.VideoCategory != null)
                {
                    Model.Videos = NewsRepos.LayDanhSachTinTheoGroupIdWithPage(5, Model.VideoCategory.ID);
                }
                NewsGroups NewsHomeCategory = NewsRepos.SearchNewsGroupByAlias("affiliate-news");
                if (NewsHomeCategory != null)
                {
                    Model.Articles = NewsRepos.LayDanhSachTinTheoGroupIdWithPage(5, NewsHomeCategory.ID);
                }
                Model.Testimonial = CommentRepo.GetRandom(4);

                Model.BrandImages = Directory.GetFiles(Server.MapPath("/Content/Images/brand/"), "*.jpg", SearchOption.TopDirectoryOnly);
                Model.Categorys = CategoryRepo.GetItems(20);
                return View(Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            
        }

    }
}
