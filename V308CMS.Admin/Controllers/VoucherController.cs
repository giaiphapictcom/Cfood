using System;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Mã giảm giá")]
    public class VoucherController : BaseController
    {
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            return View("Index", VoucherRepo.GetList(1, ListViewLimit));
        }

        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            var Model = new VoucherModels();
            return View("Create", Model);
        }

        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        public ActionResult OnCreate(VoucherModels voucher)
        {
            if (ModelState.IsValid)
            {
                if (voucher.StartDate >= voucher.EndDate)
                {
                    ModelState.AddModelError("", "Ngày bắt đầu phải trước ngày kết thúc");
                    return View("Create", voucher);
                }
                var currentUser = System.Web.HttpContext.Current.User as CustomPrincipal;
                var newitem = new Counpon
                {
                    ID = voucher.Id,
                    productcode = voucher.ProductCode,
                    title = voucher.Title,
                    site = voucher.Site,
                    code = voucher.Code,
                    type = voucher.Type,
                    summary = voucher.Summary,
                    content = voucher.Content,

                    created_by = currentUser.UserId,
                    status = voucher.Status ? 1 : 0
                };
                if (voucher.StartDate.HasValue) {
                    newitem.start_date = DateTime.Parse(voucher.StartDate.ToString());
                }
                if (voucher.EndDate.HasValue)
                {
                    newitem.end_date = DateTime.Parse(voucher.EndDate.ToString());
                }

                newitem.image = voucher.ImgNew != null ?
                    voucher.ImgNew.Upload() :
                    voucher.Image;


                var result = VoucherRepo.InsertObject(newitem);
                if (result == Data.Helpers.Result.Exists)
                {
                    ModelState.AddModelError("", string.Format("Voucher '{0}' đã tồn tại trên hệ thống.", voucher.Title));
                    return View("Create", voucher);

                }
                else if (result == Data.Helpers.Result.Ok)
                {
                    SetFlashMessage(string.Format("Thêm Voucher '{0}' thành công.", voucher.Title));
                    return RedirectToAction("Index");
                }

            }

            return View("Create", voucher);
        }

        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var voucher = VoucherRepo.Find(id);
            if (voucher == null)
            {
                return RedirectToAction("Index");
            }

            var VoucherModel = new VoucherModels
            {
                Id= voucher.ID,
                ProductCode = voucher.productcode,
                Site = voucher.site,
                Title = voucher.title,
                Code = voucher.code,
                Type = voucher.type,
                Summary = voucher.summary,
                Image = voucher.image,
                Content = voucher.content,
                StartDate = DateTime.Parse(voucher.start_date.ToString()),
                EndDate = DateTime.Parse(voucher.end_date.ToString()),
                Status = (voucher.status > 0) ? true : false

            };
            //if (VoucherModel.StartDate < new DateTime(2000, 1, 1)) {
            //    VoucherModel.StartDate = "";
            //}
            return View("Edit", VoucherModel);
        }

        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult OnEdit(VoucherModels voucher)
        {
            if (ModelState.IsValid)
            {
                if (voucher.StartDate >= voucher.EndDate) {
                    ModelState.AddModelError("", "Ngày bắt đầu phải trước ngày kết thúc");
                    return View("Edit", voucher);
                }
                var newitem = new Counpon
                {
                    ID = voucher.Id,
                    productcode = voucher.ProductCode,
                    title = voucher.Title,
                    site = voucher.Site,
                    code = voucher.Code,
                    type = voucher.Type,
                    summary = voucher.Summary,
                    content = voucher.Content,
                    //start_date = DateTime.Parse(voucher.StartDate.ToString()),
                    //end_date = DateTime.Parse(voucher.EndDate.ToString()),
                    created_by = 1,
                    status = voucher.Status ? 1 : 0
                };

                if (voucher.StartDate.HasValue)
                {
                    newitem.start_date = DateTime.Parse(voucher.StartDate.ToString());
                }
                if (voucher.EndDate.HasValue)
                {
                    newitem.end_date = DateTime.Parse(voucher.EndDate.ToString());
                }

                newitem.image = voucher.ImgNew != null ?
                    voucher.ImgNew.Upload() :
                    voucher.Image;

                var result = VoucherRepo.UpdateObject(newitem);
                if (result == Data.Helpers.Result.NotExists)
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    return View("Edit", voucher);
                }
                else if (result == Data.Helpers.Result.Exists)
                {
                    SetFlashMessage(string.Format("Sửa Voucher '{0}' trùng lặp mã.", voucher.Title));
                    return View("Edit", voucher);
                }
                else if (result == Data.Helpers.Result.Ok)
                {
                    SetFlashMessage(string.Format("Sửa Voucher '{0}' thành công.", voucher.Title));
                    return RedirectToAction("Index");
                }
            }
            return View("Edit", voucher);

        }


        [HttpPost]
        [CheckPermission(3, "Thay đổi trạng thái")]
        [ActionName("ChangeState")]
        public ActionResult OnChangeState(int id)
        {
            var result = VoucherRepo.ChangeStatus(id);
            SetFlashMessage(result == Data.Helpers.Result.Ok ? "Thay đổi trạng thái Voucher thành công." : "Voucher không tồn tại trên hệ thống.");
            return RedirectToAction("Index");

        }


        [HttpPost]
        [CheckPermission(4, "Xóa")]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = VoucherRepo.Delete(id);
            SetFlashMessage(result == Data.Helpers.Result.Ok ? "Xóa Voucher thành công." : "Voucher không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }

    }
}
