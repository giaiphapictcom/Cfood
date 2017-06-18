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
                    nameof(banner.Image),
                    nameof(banner.StartDate),
                    nameof(banner.EndDate) });
                newBanner.StartDate = banner.StartDate;
                newBanner.EndDate = banner.EndDate;
                var result = BannerService.Insert(newBanner);
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Banner '{banner.Name}' đã tồn tại trên hệ thống.");
                    ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();                  
                    return View("Create", banner);
                }
                SetFlashMessage($"Thêm banner '{banner.Name}' thành công.");
                if (banner.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
                return View("Create", banner.ResetValue());
            }
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
            var bannerEdit = banner.CloneTo<BannerModels>(new[] {
                    nameof(banner.StartDate),
                    nameof(banner.EndDate) });
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
                    nameof(banner.Image),
                    nameof(banner.StartDate),
                    nameof(banner.EndDate) });
                bannerUpdate.StartDate = banner.StartDate;
                bannerUpdate.EndDate = banner.EndDate;
                var result = BannerService.Update(bannerUpdate);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Banner không tồn tại trên hệ thống.");
                    ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
                    return View("Edit", banner);
                }
                SetFlashMessage($"Cập nhật Banner '{banner.Name}' thành công.");
                if (banner.SaveList)
                {
                    return RedirectToAction("Index");
                }             
                ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
                return View("Edit", banner);
            }
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
