using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Admin.Models.UI;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Tin tức")]
    public class NewsController : BaseController
    {
        [NonAction]
        private List<MutilCategoryItem> BuildListCategory(string site="")
        {
            return NewsGroupService.GetAll(true,site).Select
                (
                    cate => new MutilCategoryItem
                    {
                        Name = cate.Name,
                        Id = cate.ID,
                        ParentId = cate.Parent
                    }
                ).ToList();
        }
        //
        // GET: /News2/       
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index(int categoryId =0, string site ="")
        {
            if (categoryId > 0) {
                var category = NewsService.LayDanhNhomTin(categoryId);
                if (category != null) {
                    site = category.Site;
                }
            }
            ViewBag.ListCategory = BuildListCategory(site);

            ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
            if (site.Length < 1) {
                site = "home";
            }
            

            var model = new NewsViewModels
            {
                CategoryId = categoryId,
                Site = site,
                Data = NewsService.GetList(categoryId, site)
            };

            return View("Index", model);
        }        
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create(string site = "")
        {
            if (site.Length < 1) {
                site = "home";
            }
            ViewBag.ListCategory = BuildListCategory(site);
            ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
            var Model = new NewsModels();

            return View("Create", Model);

        }

        [HttpPost]
        [CheckPermission(1, "Thêm mới")]        
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        [ValidateInput(false)]
        public ActionResult OnCreate(NewsModels news)
        {
            var category = NewsService.LayTheLoaiTinTheoId(int.Parse(news.CategoryId.ToString()));
            string formView = category.Site == "affiliate" ? "affiliateCreate" : "Create";
            

            if (ModelState.IsValid)
            {
                news.ImageUrl = news.Image != null ?
                    news.Image.Upload() :
                    news.ImageUrl;
                var newsItem = new News
                {
                    Title = news.Title,
                    Alias = news.Alias,
                    TypeID = news.CategoryId,
                    Image = news.ImageUrl,
                    Summary = news.Summary,
                    Detail = news.Detail,
                    Keyword = news.Keyword,
                    Description = news.Description,
                    Order = news.Order,
                    Hot = news.IsFast,
                    Fast = news.IsFast,
                    Featured = news.IsFeatured,
                    Status = news.Status,
                    Date = news.CreatedAt,
                    Site = news.Site

                };
                var result = NewsService.Insert(newsItem);

                

                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", string.Format("Tin tức '{0}' đã tồn tại trên hệ thống.",news.Title) );
                    ViewBag.ListCategory = BuildListCategory();

                    ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
                    return View(formView, news);

                }
                SetFlashMessage( string.Format("Thêm tin tức '{0}' thành công.",news.Title));
                if (news.SaveList)
                {
                    string listViewAction = category.Site == "affiliate" ? "AffiliateIndex" : "Index";
                    return RedirectToAction(listViewAction);
                }
                ModelState.Clear();
                ViewBag.ListCategory = BuildListCategory();

                ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
                return RedirectToAction(formView);

            }

            ViewBag.ListCategory = NewsGroupService.GetAll().Select
                 (
                       cate => new MutilCategoryItem
                       {
                           Name = cate.Name,
                           Id = cate.ID,
                           ParentId = cate.Parent
                       }
                 ).ToList();
            return RedirectToAction(formView);
            //return View("Create", news);
        }

        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var newsItem = NewsService.Find(id);
            if (newsItem == null)
            {
                SetFlashMessage("Tin tức cần sửa không tồn tại trên hệ thống.");
                return RedirectToAction("Index");
            }
            var newsEdit = new NewsModels
            {
                Id = newsItem.ID,
                Title = newsItem.Title,
                CategoryId = newsItem.TypeID ?? 0,
                ImageUrl = newsItem.Image,
                Summary = newsItem.Summary,
                Detail = newsItem.Detail,
                Keyword = newsItem.Keyword,
                Description = newsItem.Description,
                Order = newsItem.Order ?? 0,
                IsHot = newsItem.Hot ?? false,
                IsFast = newsItem.Fast ?? false,
                IsFeatured = newsItem.Featured ?? false,
                Status = newsItem.Status ?? false,
                Site = newsItem.Site
               
            };

            var CategoryOld = NewsService.LayNhomTinAn((int)newsItem.TypeID);

            ViewBag.ListCategory = BuildListCategory(CategoryOld.Site);
            ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
            return View("Edit", newsEdit);

        }

        [HttpPost]
        [CheckPermission(2, "Sửa")]        
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult OnEdit(NewsModels news)
        {
            if (ModelState.IsValid)
            {
                news.ImageUrl = news.Image != null ?
                    news.Image.Upload() :
                    news.ImageUrl.ToImageOriginalPath();               
                var newsItem = new News
                {
                    ID = news.Id,
                    Title = news.Title,
                    Alias = news.Alias,
                    TypeID = news.CategoryId,
                    Image = news.ImageUrl,
                    Summary = news.Summary,
                    Detail = news.Detail,
                    Keyword = news.Keyword,
                    Description = news.Description,
                    Order = news.Order,
                    Hot = news.IsFast,
                    Fast = news.IsFast,
                    Featured = news.IsFeatured,
                    Status = news.Status,
                    Date = news.CreatedAt,
                    Site = news.Site
                };
                var CategoryOld = NewsService.LayNhomTinAn((int)newsItem.TypeID);
                var result = NewsService.Update(newsItem);

                ViewBag.ListCategory = BuildListCategory(CategoryOld.Site);
                ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Tin tức không tồn tại trên hệ thống.");
                    
                    
                    return View("Edit", news);
                }
                SetFlashMessage( string.Format("Sửa tin tức '{0}' thành công.",news.Title) );
                if (news.SaveList)
                {
                    var category = NewsService.LayTheLoaiTinTheoId(int.Parse(news.CategoryId.ToString()));
                    string listViewAction = category.Site == "affiliate" ? "AffiliateIndex" : "Index";
                    return RedirectToAction(listViewAction);
                   
                }
                //ViewBag.ListCategory = BuildListCategory(CategoryOld.Site);
                //ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
                return View("Edit", news);

            }
            ViewBag.ListCategory = BuildListCategory();
            ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
            return View("Edit", news);
        }        
        
        [CheckPermission(3, "Xóa")]
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult OnDelete(int id)
        {
            var news = NewsService.Find(id);
            var category = NewsService.LayTheLoaiTinTheoId(int.Parse(news.TypeID.ToString()));

            var result = NewsService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa tin tức thành công." : 
                "Tin tức không tồn tại trên hệ thống.");

            
            string listViewAction = category.Site == "affiliate" ? "AffiliateIndex" : "Index";
            return RedirectToAction(listViewAction);
            
        }


        
        #region affiliate
        public ActionResult AffiliateIndex(int categoryId = 0)
        {
            return Index(categoryId, "affiliate");
        }

        [CheckPermission(1, "Thêm mới")]
        public ActionResult affiliateCreate()
        {
            return Create("affiliate");
        }
        #endregion
    }
}