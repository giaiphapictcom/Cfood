using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Kho hàng")]
    public class ProductStoreController : BaseController
    {
        //
        // GET: /ProductStore/        
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            var data = ProductStoreService.GetAll();
            return View("Index", data);
        }
        [CheckPermission(1, "Thêm mới")]     
        public ActionResult Create()
        {
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
          
            var store = new ProductStoreModels();
            return View("Create", store);
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnCreate(ProductStoreModels store)
        {
            if (ModelState.IsValid)
            {
              
                var result = ProductStoreService.Insert
                    (
                       store.Name,
                       store.Phone,
                       store.Manager,
                       store.Address,
                       store.CreatedAt,
                       store.UpdatedAt,
                       store.State
                    );
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Kho hàng '{store.Name}' đã tồn tại trên hệ thống.");
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();                  
                    return View("Create", store);
                }
                SetFlashMessage($"Thêm kho hàng '{store.Name}' thành công.");
                if (store.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                ModelState.Clear();
                return View("Create", store.ResetValue());

            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();          
            return View("Create", store);
        }       
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var store = ProductStoreService.Find(id);
            if (store == null)
            {

                return RedirectToAction("Index");

            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();          
            var data = new ProductStoreModels
            {
                Id = store.Id,
                Name = store.Name,
                Phone =  store.Phone,
                Manager = store.Manager,
                Address = store.Address,
                State = store.State
            };
            return View("Edit", data);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]        
        public ActionResult OnEdit(ProductStoreModels store)
        {
            if (ModelState.IsValid)
            {

                var result = ProductStoreService.Update(
                    store.Id, store.Name, store.Phone,
                    store.Manager, store.Address, store.CreatedAt,
                    store.UpdatedAt, store.State);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Kho hàng không tồn tại trên hệ thống.");
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                    return View("Edit", store);
                }
                SetFlashMessage($"Cập nhật kho hàng '{store.Name}' thành công.");
                if (store.SaveList)
                {
                    return RedirectToAction("Index");

                }
                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                return View("Edit", store);

            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            return View("Edit", store);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]       
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = ProductStoreService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa kho hàng thành công." :
                "Kho hàng không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }
    }
}
