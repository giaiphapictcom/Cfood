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
        public ActionResult Index(string site= "")
        {
            return View("Index", MenuConfigService.GetList(1,10,site));
        }        
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create(string site = "")
        {
            AddViewData("ListState", DataHelper.ListEnumType<StateEnum>()); 
            var Model = new MenuConfigModels();
            Model.Site = site;
            return View("Create",Model );

        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnCreate(MenuConfigModels config)
        {
            if (ModelState.IsValid)
            {
                var menuAdd = new MenuConfig();
                menuAdd.Id = config.Id;
                menuAdd.Name = config.Name;
                menuAdd.Site = config.Site;
                menuAdd.Description = config.Description;
                menuAdd.Link = config.Link;
                menuAdd.Order = config.Order;
                menuAdd.Target = config.Target;


                var result = MenuConfigService.Insert(menuAdd);

                if (result == Result.Exists)
                {
                    AddViewData("ListState", DataHelper.ListEnumType<StateEnum>(), "ListSite", DataHelper.ListEnumType<SiteEnum>());              
                    ModelState.AddModelError("", string.Format("Tên Menu '{0}' đã tồn tại trên hệ thống.",config.Name) );
                    return View("Create", config);
                }
                SetFlashMessage( string.Format("Thêm Menu '{0}' thành công.",config.Name) );
                if (config.SaveList)
                {
                    string actionReturn = config.Site == "affiliate" ? "affiliatemenu" : "Index";
                    return RedirectToAction(actionReturn);
                }
                AddViewData("ListState", DataHelper.ListEnumType<StateEnum>(), "ListSite", DataHelper.ListEnumType<SiteEnum>());
                ModelState.Clear();
                return View("Create", config.ResetValue());

            }
            AddViewData("ListState", DataHelper.ListEnumType<StateEnum>(), "ListSite", DataHelper.ListEnumType<SiteEnum>());
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
            AddViewData("ListState", DataHelper.ListEnumType<StateEnum>(), "ListSite", DataHelper.ListEnumType<SiteEnum>());
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
                    AddViewData("ListState", DataHelper.ListEnumType<StateEnum>(), "ListSite", DataHelper.ListEnumType<SiteEnum>());
                    return View("Edit", config);
                }
                SetFlashMessage( string.Format("Cập nhật Menu '{0}' thành công.",config.Name) );
                if (config.SaveList)
                {
                    string actionReturn = config.Site == "affiliate" ? "affiliatemenu" : "Index";
                    return RedirectToAction(actionReturn);
                    
                }
                AddViewData("ListState", DataHelper.ListEnumType<StateEnum>(), "ListSite", DataHelper.ListEnumType<SiteEnum>());
                return View("Edit", config);
            }
            AddViewData("ListState", DataHelper.ListEnumType<StateEnum>(), "ListSite", DataHelper.ListEnumType<SiteEnum>());
            return View("Edit", config);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]        
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var menu = MenuConfigService.Find(id);
            string result = MenuConfigService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa Menu thành công." :
                "Thông tin Menu không tồn tại trên hệ thống.");

            string actionReturn = menu.Site == "affiliate" ? "affiliatemenu" : "Index";
            return RedirectToAction(actionReturn);
        }


        #region Affiliate
        [CheckPermission(0, "Danh sách")]
        public ActionResult affiliatemenu()
        {
            return Index("affiliate");
        }
        [CheckPermission(1, "Thêm mới")]
        public ActionResult affiliateCreate()
        {
            return Create("affiliate");
        }
        #endregion
    }
}
