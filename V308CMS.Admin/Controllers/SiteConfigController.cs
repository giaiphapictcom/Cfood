using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;

namespace V308CMS.Admin.Controllers
{
    public class SiteConfigController : BaseController
    {
        //
        // GET: /SiteConfig/

        public ActionResult Index()
        {
            var data = SiteConfigService.GetList();
            return View("Index",data);
        }

        public ActionResult Create()
        {
            return View("Create", new SiteConfigModels());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult OnCreate(SiteConfigModels config)
        {
            if (ModelState.IsValid)
            {
                var result = SiteConfigService.Insert(config.Name, config.Content);
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Tên cấu hình {config.Name} đã tồn tại trên hệ thống.");
                    return View("Create", config);
                }
                SetFlashMessage($"Thêm cấu hình '{config.Name}' thành công.");
                return RedirectToAction("Index");
            }
            return View("Create", config);
        }
        
        public ActionResult Edit(int id)
        {
            var config = SiteConfigService.GetById(id);
            if (config == null)
            {
                
                return RedirectToAction("Index");

            }
            var configModel = new SiteConfigModels
            {
                Id = config.id,
                Name = config.name,
                Content = config.content
            };
            return View("Edit", configModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult OnUpdate(SiteConfigModels config)
        {
            if (ModelState.IsValid)
            {
                var result = SiteConfigService.Update(config.Id,config.Name, config.Content);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    return View("Edit", config);
                }
                SetFlashMessage($"Sửa cấu hình '{config.Name}' thành công.");
                return RedirectToAction("Index");
            }
            return View("Edit", config);

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = SiteConfigService.Delete(id);
            SetFlashMessage(result == Result.Ok?"Xóa cấu hình thành công.":"Cấu hình không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }


    }
}
