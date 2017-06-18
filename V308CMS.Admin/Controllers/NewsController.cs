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
        private List<MutilCategoryItem> BuildListCategory()
        {
            return NewsGroupService.GetAll().Select
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
        public ActionResult Index(int categoryId =0, int site =0)
        {
            ViewBag.ListCategory = BuildListCategory();
            ViewBag.ListSite = DataHelper.ListEnumType<NewsSiteEnum>();
            var model = new NewsViewModels
            {
                CategoryId = categoryId,
                Site = site,
                Data = NewsService.GetList(categoryId, site)
            };
            return View("Index", model);
        }        
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            ViewBag.ListCategory = BuildListCategory();
            ViewBag.ListSite = DataHelper.ListEnumType<NewsSiteEnum>();
            return View("Create", new NewsModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]        
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        [ValidateInput(false)]
        public ActionResult OnCreate(NewsModels news)
        {
            if (ModelState.IsValid)
            {
                news.ImageUrl = news.Image != null ?
                    news.Image.Upload() :
                    news.ImageUrl;
                var newsItem = new News
                {
                    Title = news.Title,
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
                    ModelState.AddModelError("", $"Tin tức '{news.Title}' đã tồn tại trên hệ thống.");
                    ViewBag.ListCategory = BuildListCategory();
                    ViewBag.ListSite = DataHelper.ListEnumType<NewsSiteEnum>();
                    return View("Create", news);
                }
                SetFlashMessage($"Thêm tin tức '{news.Title}' thành công.");
                if (news.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                ViewBag.ListCategory = BuildListCategory();
                ViewBag.ListSite = DataHelper.ListEnumType<NewsSiteEnum>();
                return View("Create", news.ResetValue());
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
            return View("Create", news);
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
            ViewBag.ListCategory = BuildListCategory();
            ViewBag.ListSite = DataHelper.ListEnumType<NewsSiteEnum>();
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
                var result = NewsService.Update(newsItem);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Tin tức không tồn tại trên hệ thống.");
                    ViewBag.ListCategory = BuildListCategory();
                    ViewBag.ListSite = DataHelper.ListEnumType<NewsSiteEnum>();
                    return View("Edit", news);
                }
                SetFlashMessage($"Sửa tin tức '{news.Title}' thành công.");
                if (news.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListCategory = BuildListCategory();
                ViewBag.ListSite = DataHelper.ListEnumType<NewsSiteEnum>();
                return View("Edit", news);

            }
            ViewBag.ListCategory = BuildListCategory();
            ViewBag.ListSite = DataHelper.ListEnumType<NewsSiteEnum>();
            return View("Edit");
        }        
        [CheckPermission(3, "Xóa")]
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult OnDelete(int id)
        {
            var result = NewsService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa tin tức thành công." : 
                "Tin tức không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }

    }
}
