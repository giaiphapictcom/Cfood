using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;
using V308CMS.Data.Models;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Cấu hình Menu")]
    public class MenuConfigController : BaseController
    {
              
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {         
            return View("Index", MenuConfigService.GetList());
        }        
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            AddViewData("ListState", DataHelper.ListEnumType<StateEnum>());                 
            return View("Create", new MenuConfigModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnCreate(MenuConfigModels config)
        {
            if (ModelState.IsValid)
            {
                var result = MenuConfigService.Insert(
                   config.CloneTo<MenuConfig>()
                    );
                if (result == Result.Exists)
                {
                    AddViewData("ListState", DataHelper.ListEnumType<StateEnum>());                   
                    ModelState.AddModelError("", $"Tên Menu '{config.Name}' đã tồn tại trên hệ thống.");
                    return View("Create", config);
                }
                SetFlashMessage($"Thêm Menu '{config.Name}' thành công.");
                if (config.SaveList)
                {
                    return RedirectToAction("Index");
                }
                AddViewData("ListState", DataHelper.ListEnumType<StateEnum>());
                ModelState.Clear();
                return View("Create", config.ResetValue());

            }
            AddViewData("ListState", DataHelper.ListEnumType<StateEnum>());
            return View("Create", config);
        }       
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var config = MenuConfigService.Find(id);
            if (config == null)
            {

                return RedirectToAction("Index");

            }
            AddViewData("ListState", DataHelper.ListEnumType<StateEnum>());
            var data = config.CloneTo<MenuConfigModels>();
            return View("Edit", data);

        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
       
        public ActionResult OnEdit(MenuConfigModels config)
        {
            if (ModelState.IsValid)
            {
                var result = MenuConfigService.Update(config.CloneTo<MenuConfig>());
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    AddViewData("ListState", DataHelper.ListEnumType<StateEnum>());
                    return View("Edit", config);
                }
                SetFlashMessage($"Cập nhật Menu '{config.Name}' thành công.");
                if (config.SaveList)
                {
                    return RedirectToAction("Index");
                }
                AddViewData("ListState", DataHelper.ListEnumType<StateEnum>());
                return View("Edit", config);
            }
            AddViewData("ListState", DataHelper.ListEnumType<StateEnum>());
            return View("Edit", config);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]        
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = EmailConfigService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa Menu thành công." :
                "Thông tin Menu không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }

    }
}
