using System.ComponentModel;
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
    [CheckGroupPermission(true, "Cấu hình Email")]
    public class EmailConfigController : BaseController
    {
        //
        // GET: /SiteConfig/      
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            return View("Index", EmailConfigService.GetAll());
        }       
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            AddViewData(
                "ListEmailType", DataHelper.ListEnumType<EmailType>(),
                "ListState", DataHelper.ListEnumType<StateEnum>());
            return View("Create", new EmailConfigModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]     
        public ActionResult OnCreate(EmailConfigModels config)
        {
            if (ModelState.IsValid)
            {
                var result = EmailConfigService.Insert(
                             config.CloneTo<EmailConfig>()
                    );
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", string.Format("Thông tin Email '{0}' đã tồn tại trên hệ thống.",config.Name) );
                    AddViewData(
                       "ListEmailType", DataHelper.ListEnumType<EmailType>(),
                       "ListState", DataHelper.ListEnumType<StateEnum>());
                    return View("Create", config);
                }
                SetFlashMessage( string.Format("Thêm thông tin Email '{0}' thành công.",config.Name));
                if (config.SaveList)
                {
                    return RedirectToAction("Index");

                }
                AddViewData(
                 "ListEmailType", DataHelper.ListEnumType<EmailType>(),
                 "ListState", DataHelper.ListEnumType<StateEnum>()
                 );
                ModelState.Clear();
                return View("Create", config.ResetValue());
            }
            AddViewData(
                "ListEmailType", DataHelper.ListEnumType<EmailType>(),
                "ListState", DataHelper.ListEnumType<StateEnum>()
                );
            return View("Create", config);
        }
        [CheckPermission(2, "Sửa")]        
        public ActionResult Edit(int id)
        {
            var config = EmailConfigService.Find(id);
            if (config == null)
            {
                return RedirectToAction("Index");
            }

            AddViewData(
                "ListEmailType", DataHelper.ListEnumType<EmailType>(),
                "ListState", DataHelper.ListEnumType<StateEnum>()
                );
            var data = config.CloneTo<EmailConfigModels>();
            return View("Edit", data);

        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        
        public ActionResult OnEdit(EmailConfigModels config)
        {
            if (ModelState.IsValid)
            {
                var result = EmailConfigService.Update(config.CloneTo<EmailConfig>());
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Thông tin Email không tồn tại trên hệ thống.");
                    AddViewData(
                        "ListEmailType", DataHelper.ListEnumType<EmailType>(),
                        "ListState", DataHelper.ListEnumType<StateEnum>()
                    );
                    return View("Edit", config);
                }
                SetFlashMessage(string.Format("Sửa cấu hình Email '{0}' thành công.", config.Name));
                if (config.SaveList)
                {
                    return RedirectToAction("Index");
                }
                AddViewData(
                 "ListEmailType", DataHelper.ListEnumType<EmailType>(),
                 "ListState", DataHelper.ListEnumType<StateEnum>()
                 );
                return View("Edit", config);
            }
            AddViewData(
                "ListEmailType", DataHelper.ListEnumType<EmailType>(),
                "ListState", DataHelper.ListEnumType<StateEnum>()
                );
            return View("Edit", config);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]      
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = EmailConfigService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa cấu hình Email thành công." :
                "Cấu hình Email không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }

    }
}
