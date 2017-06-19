using System.ComponentModel;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Common;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Nhà phân phối")]
    public class ProductDistributorController : BaseController
    {
        [CheckPermission(0, "Danh sách")]        
        public ActionResult Index()
        {
            var data = ProductDistributorService.GetAll();
            return View("Index", data);
        }
        [CheckPermission(1, "Thêm mới")]      
        public ActionResult Create()
        {
            var distributor = new ProductDistributorModels();
            return View("Create", distributor);
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]      
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult OnCreate(ProductDistributorModels distributor)
        {
            if (ModelState.IsValid)
            {
                distributor.ImageUrl = distributor.Image != null ?
                    distributor.Image.Upload() :
                    distributor.ImageUrl;
                var result = ProductDistributorService.Insert
                    (
                        distributor.Name, distributor.ImageUrl,
                        distributor.Detail, distributor.Status,
                        distributor.Order, distributor.CreatedDate
                    );
                if (result == "exists")
                {
                    ModelState.AddModelError("", string.Format("Tên Nhà phân phối '{0}' đã tồn tại trên hệ thống.",distributor.Name) );
                    return View("Create", distributor);
                }
                SetFlashMessage( string.Format("Thêm Nhà phân phối '{0}' thành công.",distributor.Name) );
                if (distributor.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                return View("Create", distributor.ResetValue());
            }
            return View("Create", distributor);
        }       
        [CheckPermission(2, "Sửa")]     
        public ActionResult Edit(int id)
        {
            var distributor = ProductDistributorService.Find(id);
            if (distributor == null)
            {
                return RedirectToAction("Index");

            }

            var data = new ProductDistributorModels
            {
                Id = distributor.ID,
                Name = distributor.Name,
                ImageUrl = distributor.Image,
                Detail = distributor.Detail,
                Status = distributor.Status ?? false,
                Order = distributor.Number ?? 0
            };
            return View("Edit", data);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]             
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult OnEdit(ProductDistributorModels distributor)
        {
            if (ModelState.IsValid)
            {
              
                distributor.ImageUrl = distributor.Image != null? 
                    distributor.Image.Upload(): 
                    distributor.ImageUrl.ToImageOriginalPath();
                var result = ProductDistributorService.Update(
                    distributor.Id, distributor.Name,
                    distributor.ImageUrl, distributor.Detail,
                    distributor.Status, distributor.Order,
                    distributor.CreatedDate);
                if (result == "not_exists")
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    return View("Edit", distributor);
                }
                SetFlashMessage( string.Format("Cập nhật nhà phân phối '{0}' thành công.",distributor.Name) );
                if (distributor.SaveList)
                {
                    return RedirectToAction("Index");
                }
                return View("Edit", distributor);
            }
            return View("Edit", distributor);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]        
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = ProductDistributorService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa nhà phân phối thành công." :
                "Nhà phân phối không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }
    }
}