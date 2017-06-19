using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Đơn vị tính")]
    public class ProductUnitController:BaseController
    {            
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {          
            return View("Index", UnitService.GetAll());
        }       
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();     
            return View("Create", new ProductUnitModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]     
        public ActionResult OnCreate(ProductUnitModels unit)
        {
            if (ModelState.IsValid)
            {

                var result = UnitService.Insert
                    (
                       unit.Name,
                       unit.CreatedAt,
                       unit.UpdatedAt, 
                       unit.State
                    );
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Đơn vị tính '{unit.Name}' đã tồn tại trên hệ thống.");
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                    return View("Create", unit);
                }
                SetFlashMessage($"Thêm đơn vị tính '{unit.Name}' thành công.");
                if (unit.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                return View("Create", unit.ResetValue());

            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            return View("Create", unit);
        }       
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var unit = UnitService.Find(id);
            if (unit == null)
            {

                return RedirectToAction("Index");

            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            var data = new ProductUnitModels
            {
                Id = unit.Id,
                Name = unit.Name,               
                State = unit.State
            };
            return View("Edit", data);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]        
        public ActionResult OnEdit(ProductUnitModels unit)
        {
            if (ModelState.IsValid)
            {

                var result = UnitService.Update(
                    unit.Id, unit.Name,
                    unit.CreatedAt,
                    unit.UpdatedAt, unit.State);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                    return View("Edit", unit);
                }
                SetFlashMessage($"Cập nhật Đơn vị tính '{unit.Name}' thành công.");
                if (unit.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                return View("Edit", unit);
            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            return View("Edit", unit);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]        
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = UnitService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa đơn vị tính thành công." :
                "Đơn vị tính không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }
    }
}