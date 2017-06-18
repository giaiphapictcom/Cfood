using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Models;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Kích cỡ sản phẩm")]
    public class SizeController:BaseController
    {       
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {          
            return View("Index", SizeService.GetAll());
        }       
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            return View("Create", new SizeModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
       
        public ActionResult OnCreate(SizeModels size)
        {
            if (ModelState.IsValid)
            {
                var result = SizeService.Insert(size.CloneTo<Size>());
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Kích cỡ {size.Name} đã tồn tại trên hệ thống.");
                    return View("Create", size);
                }
                SetFlashMessage($"Thêm Kích cỡ '{size.Name}' thành công.");
                if (size.SaveList){
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                return View("Create", size.ResetValue());
            }
            return View("Create", size);
        }        
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var size = SizeService.Find(id);
            if (size == null)
            {

                return RedirectToAction("Index");

            }
            return View("Edit", size.CloneTo<SizeModels>());

        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]               
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult OnEdit(SizeModels size)
        {
            if (ModelState.IsValid)
            {
                var result = SizeService.Update(size.CloneTo<Size>());
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    return View("Edit", size);
                }
                SetFlashMessage($"Sửa Kích cỡ '{size.Name}' thành công.");
                if (size.SaveList)
                {
                    return RedirectToAction("Index");
                }
                return View("Edit", size);
            }
            return View("Edit", size);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]       
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = SizeService.Delete(id);
            SetFlashMessage(result == Result.Ok ? "Xóa Kích cỡ thành công." : "Kích cỡ không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }

    }
}