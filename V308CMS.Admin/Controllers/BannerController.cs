using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;
using V308CMS.Data.Models;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Banner")]
    public class BannerController : BaseController
    {
        //
        // GET: /Banner/
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index(byte position=0)
        {
            return View("Index",BannerService.GetList(position));
        }
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
            ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
            return View("Create", new BannerModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult OnCreate(BannerModels banner)
        {
            if (ModelState.IsValid)
            {
                banner.ImageUrl = banner.Image != null ?
                    banner.Image.Upload() :
                    banner.ImageUrl;
                var newBanner = banner.CloneTo<Banner>(new[] {
                   "Image",
                   "StartDate",
                   "EndDate"
                });
                newBanner.StartDate = banner.StartDate;
                newBanner.EndDate = banner.EndDate;

                var result = BannerService.Insert(newBanner);
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", string.Format("Banner '{0}' đã tồn tại trên hệ thống.",banner.Name) );
                    ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
                    ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();                  
                    return View("Create", banner);
                }
                SetFlashMessage( string.Format("Thêm banner '{0}' thành công.",banner.Name) );
                if (banner.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
                ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
                return View("Create", banner.ResetValue());
            }
            ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
            ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
            return View("Create", banner);
        }
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var banner = BannerService.Find(id);
            if (banner == null)
            {
                return RedirectToAction("Index");

            }
            ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
            ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
            var bannerEdit = banner.CloneTo<BannerModels>();

            bannerEdit.StartDate = banner.StartDate;
            bannerEdit.EndDate = banner.EndDate;
            return View("Edit", bannerEdit);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult OnEdit(BannerModels banner)
        {
            if (ModelState.IsValid)
            {
                banner.ImageUrl = banner.Image != null ?
                   banner.Image.Upload() :
                   banner.ImageUrl.ToImageOriginalPath();
                var bannerUpdate = banner.CloneTo<Banner>(new[] {
                "Image",
                "StartDate",
                "EndDate" });
                bannerUpdate.StartDate = banner.StartDate;
                bannerUpdate.EndDate = banner.EndDate;
                var result = BannerService.Update(bannerUpdate);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Banner không tồn tại trên hệ thống.");
                    ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
                    ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
                    return View("Edit", banner);
                }
                SetFlashMessage(string.Format("Cập nhật Banner '{0}' thành công.", banner.Name));
                if (banner.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
                ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
                return View("Edit", banner);
            }
            ViewBag.ListSite = DataHelper.ListEnumType<SiteEnum>();
            ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
            return View("Edit", banner);
        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = BannerService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa banner thành công." :
                "Banner không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [CheckPermission(4, "Thay đổi trạng thái")]
        [ActionName("ChangeStatus")]
        public ActionResult OnChangeStatus(int id)
        {
            var result = BannerService.ChangeStatus(id);
            SetFlashMessage(result == Result.Ok ?
                "Thay đổi trạng thái banner thành công." :
                "Banner không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }


    }
}
