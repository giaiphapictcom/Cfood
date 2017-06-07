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
                Model.VideoCategory = NewsRepos.SearchNewsGroup("affiliate-video");
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
                Model.Brands = ProductRepos.getRandomBrands(0, 6);
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
        
        [AffiliateAuthorize]
        public ActionResult NewsList(string CategoryAlias = "", string PageTitle="")
        {
            try
            {
                CreateRepos();
                NewsIndexPageContainer Model = new NewsIndexPageContainer();
                Model.NewsGroups = NewsRepos.SearchNewsGroupByAlias(CategoryAlias);
                if (Model.NewsGroups != null) {
                    Model.ListNews = NewsRepos.LayDanhSachTinTheoGroupId(ProductHelper.ProductShowLimit, Model.NewsGroups.ID);
                    Model.PageTitle = Model.NewsGroups.Name;
                } else {
                    InsertNewsGroupDefault(CategoryAlias);
                    Model.PageTitle = PageTitle; 
                }
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

        private void InsertNewsGroupDefault(string NewsGroupAlias="",NewsGroups GroupParent= null) {
            if (GroupParent == null) {
                NewsGroups AffiliateGroup = NewsRepos.SearchNewsGroupByAlias("affiliate-news");
                if (AffiliateGroup.ID < 1)
                {
                    return;
                }
                GroupParent = AffiliateGroup;
            }

            var GroupItem = new NewsGroups() { Link = "", Date = DateTime.Now, Number = 0, Status = true, Parent = GroupParent.ID, Level = "1", Alias = NewsGroupAlias };

            switch (GroupItem.Alias)
            {
                case "chuong-trinh-thuc-day":
                    GroupItem.Name = "Chương trình thúc đẩy";break;
                case "huong-dan":
                    GroupItem.Name = "Hướng Dẫn"; break;
                case "quy-dinh":
                    GroupItem.Name = "Quy Định"; break;
                case "chinh-sach":
                    GroupItem.Name = "Chính Sách"; break;
                case "ho-tro":
                    GroupItem.Name = "Hỗ Trợ"; break;
                case "vinh-danh-ca-nhan":
                    GroupItem.Name = "Vinh Danh Cá Nhân"; break;
                case "top-xuat-sac":
                    GroupItem.Name = "Top Xuất Sắc"; break;
                case "he-thong":
                    GroupItem.Name = "Hệ Thống"; break;
            }
            mEntities.AddToNewsGroups(GroupItem);
            mEntities.SaveChanges();

            News NewsItem = new News() { Date = DateTime.Now, Order = 1, Status = true, Summary = "", Title = GroupItem.Name + " bài viết mẫu", TypeID = GroupItem.ID, Description = "Nội dung của " + GroupItem.Name };
            mEntities.AddToNews(NewsItem);
            mEntities.SaveChanges();
        }
        
        [AffiliateAuthorize]
        public ActionResult News(string NewsAlias = "", string PageTitle="")
        {
            try
            {
                CreateRepos();
                NewsDetailPageContainer Model = new NewsDetailPageContainer();
                Model.NewsItem = NewsRepos.SearchNews(NewsAlias);
                if (Model.NewsItem ==null || Model.NewsItem.ID < 1)
                {
                    NewsGroups AffiliateGroup = NewsRepos.SearchNewsGroupByAlias("affiliate-news");
                    string NewsTitle = "";
                    switch (NewsAlias)
                    {
                        case "ve-affiliate":
                            NewsTitle = "Về Affiliate"; break;
                        default:
                            NewsTitle = "News default title "; break;
                    }
                    News NewsItem = new News() { Date = DateTime.Now, Alias = NewsAlias, Order = 1, Status = true, Summary = NewsTitle+" mô tả ngắn", Title = NewsTitle + " bài viết mẫu", TypeID = AffiliateGroup.ID, Description = "Nội dung của " + NewsTitle };
                    mEntities.AddToNews(NewsItem);
                    mEntities.SaveChanges();
                }
                Model.PageTitle = PageTitle;
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
        
        [AffiliateAuthorize]
        public ActionResult NewsTable(string CategoryAlias = "", string PageTitle = "")
        {
            try
            {
                CreateRepos();
                NewsIndexPageContainer Model = new NewsIndexPageContainer();
                Model.NewsGroups = NewsRepos.SearchNewsGroupByAlias(CategoryAlias);
                if (Model.NewsGroups != null)
                {
                    Model.ListNews = NewsRepos.LayDanhSachTinTheoGroupId(ProductHelper.ProductShowLimit, Model.NewsGroups.ID);
                    Model.PageTitle = Model.NewsGroups.Name; 
                }
                else
                {
                    var GroupItem = new NewsGroups() { Link = "", Date = DateTime.Now, Number = 0, Status = true, Parent = 0, Level = "99", Alias = CategoryAlias };
                    mEntities.AddToNewsGroups(GroupItem);
                    mEntities.SaveChanges();

                    Model.PageTitle = PageTitle;
                }
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
