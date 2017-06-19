using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Cấu hình")]
    public class SiteConfigController : BaseController
    {
        //
        // GET: /SiteConfig/       
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {           
            return View("Index", SiteConfigService.GetAll());
        }       
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            return View("Create", new SiteConfigModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult OnCreate(SiteConfigModels config)
        {
            if (ModelState.IsValid)
            {
                var result = SiteConfigService.Insert(config.CloneTo<SiteConfig>());

                if (result == "exists")
                {
                    ModelState.AddModelError("", string.Format("Tên cấu hình {0} đã tồn tại trên hệ thống.",config.Name) );
                    return View("Create", config);
                }
                SetFlashMessage( string.Format("Thêm cấu hình '{0}' thành công.",config.Name) );
                if (config.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                return View("Create", config.ResetValue());
            }
            return View("Create", config);
        }     
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var config = SiteConfigService.Find(id);
            if (config == null)
            {
                
                return RedirectToAction("Index");

            }        
            return View("Edit", config.CloneTo<SiteConfigModels>());

        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]      
        public ActionResult OnEdit(SiteConfigModels config)
        {
            if (ModelState.IsValid)
            {
                var result = SiteConfigService.Update(config.CloneTo<SiteConfig>());
                if (result == "not_exists")
                {
                    ModelState.AddModelError("", "Cấu hình không tồn tại trên hệ thống.");
                    return View("Edit", config);
                }
                SetFlashMessage( string.Format("Sửa cấu hình '{0}' thành công.",config.Name) );
                if (config.SaveList)
                {
                    return RedirectToAction("Index");
                }
                return View("Edit", config);
            }
            return View("Edit", config);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]        
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = SiteConfigService.Delete(id);
            SetFlashMessage(result == "ok" ?"Xóa cấu hình thành công.":"Cấu hình không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }


    }
}