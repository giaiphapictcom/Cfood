using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Màu sắc sản phẩm")]
    public class ProductColorController : BaseController
    {
        //
        // GET: /ProductStore/      
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            var data = ColorService.GetList();
            return View("Index", data);
        }        
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();

            var brand = new ProductColorModels();
            return View("Create", brand);
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]       
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult OnCreate(ProductColorModels color)
        {
            if (ModelState.IsValid)
            {

                var result = ColorService.Insert
                    (
                      color.Name,color.Code,
                      color.Description,color.CreatedAt,
                      color.UpdatedAt,color.State
                    );
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", string.Format("Màu '{0}' đã tồn tại trên hệ thống.",color.Name));
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                    return View("Create", color);
                }
                SetFlashMessage( string.Format("Thêm Màu '{0}' thành công.",color.Name));
                if (color.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                return View("Create", color.ResetValue());
            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            return View("Create", color);
        }        
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var color = ColorService.Find(id);
            if (color == null)
            {

                return RedirectToAction("Index");

            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            var data = new ProductColorModels
            {
                Id = color.Id,
                Name = color.Name,
                Code =  color.Code,
                Description = color.Description,
                State = color.State
            };
            return View("Edit", data);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnEdit(ProductColorModels color)
        {
            if (ModelState.IsValid)
            {

                var result = ColorService.Update(
                    color.Id, color.Name,
                    color.Code,color.Description,
                    color.CreatedAt,color.UpdatedAt,
                    color.State);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Màu sắc không tồn tại trên hệ thống.");
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                    return View("Edit", color);
                }
                SetFlashMessage(string.Format("Cập nhật màu '{0}' thành công.", color.Name));
                if (color.SaveList)
                {
                    return RedirectToAction("Index");

                }
                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                return View("Edit", color);
            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            return View("Edit", color);

        }        
        [CheckPermission(3, "Xóa")]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = ColorService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa màu sắc sản phẩm thành công." :
                "Màu sắc sản phẩm không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }
    }
}
