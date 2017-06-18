using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Common;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Nhà sản xuất")]
    public class ProductManufacturerController : BaseController
    {        
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            var data = ProductManufacturerService.GetAll();
            return View("Index", data);
        }       
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {          
            var manufacturer = new ProductManufacturerModels();
            return View("Create", manufacturer);
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnCreate(ProductManufacturerModels manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturer.ImageUrl = manufacturer.Image != null ?
                    manufacturer.Image.Upload() :
                    manufacturer.ImageUrl;
                var result = ProductManufacturerService.Insert
                    (
                        manufacturer.Name,manufacturer.ImageUrl,
                        manufacturer.Detail,manufacturer.Status,
                        manufacturer.Order,manufacturer.CreatedDate
                    );
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Tên Nhà sản xuất '{manufacturer.Name}' đã tồn tại trên hệ thống.");                    
                    return View("Create", manufacturer);
                }
                SetFlashMessage($"Thêm Nhà sản xuất '{manufacturer.Name}' thành công.");
                if (manufacturer.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                return View("Create", manufacturer.ResetValue());
            }            
            return View("Create", manufacturer);
        }       
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var manufacturer = ProductManufacturerService.Find(id);
            if (manufacturer == null)
            {
                return RedirectToAction("Index");

            }
           
            var data = new ProductManufacturerModels
            {
                Id = manufacturer.ID,
                Name = manufacturer.Name,
                ImageUrl = manufacturer.Image,
                Detail = manufacturer.Detail,
                Status = manufacturer.Status ?? false,
                Order =  manufacturer.Number ?? 0
            };
            return View("Edit", data);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]        
        public ActionResult OnEdit(ProductManufacturerModels manufacturer)
        {
            if (ModelState.IsValid)
            {
               

                manufacturer.ImageUrl = manufacturer.Image != null?
                    manufacturer.Image.Upload(): 
                    manufacturer.ImageUrl.ToImageOriginalPath();

                var result = ProductManufacturerService.Update(
                    manufacturer.Id, manufacturer.Name,
                    manufacturer.ImageUrl, manufacturer.Detail,
                    manufacturer.Status, manufacturer.Order,
                    manufacturer.CreatedDate);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Nhà sản xuất tồn tại trên hệ thống.");                   
                    return View("Edit", manufacturer);
                }
                SetFlashMessage($"Cập nhật nhà sản xuất '{manufacturer.Name}' thành công.");
                if (manufacturer.SaveList)
                {
                    return RedirectToAction("Index");
                }
                return View("Edit", manufacturer);
            }          
            return View("Edit", manufacturer);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]       
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = ProductManufacturerService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa nhà sản xuất thành công." :
                "Nhà sản xuất không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }

    }
}