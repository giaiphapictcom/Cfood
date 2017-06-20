using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{   
    [Authorize]
    [CheckGroupPermission(true, "Tài khoản hệ thống")]
    public class AdminAccountController : BaseController
    {
        //
        // GET: /Admin/       
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {            
            return View("Index", AdminAccountService.GetAll());
        }   
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            ViewBag.ListRole = RoleService.GetAll();
            ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
            return View("Create", new AdminModels());
        }
        [CheckPermission(1, "Thêm mới")]             
        [ActionName("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnCreate(AdminModels model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Password)){

                    ModelState.AddModelError("Password","Vui lòng nhập Mật khẩu tài khoản.");
                    ViewBag.ListRole = RoleService.GetAll();
                    ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
                    return View("Create", model);
                }
                if (model.ConfirmPassword != model.Password){
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không đúng.");
                    ViewBag.ListRole = RoleService.GetAll();
                    ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
                    return View("Create", model);
                }
                model.AvatarUrl = model.AvatarFile != null ?
                model.AvatarFile.Upload() :
                model.AvatarUrl;
                var result = AdminAccountService.Insert(new Data.Admin
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Avatar = model.AvatarUrl,
                    Email = model.Email,
                    FullName = model.FullName,
                    Role = model.Role,
                    Date = model.Date,
                    Status = model.Status,
                    Type = model.Type,
                });
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", string.Format("Tài khoản '{0}' đã tồn tại trên hệ thống.",model.UserName) );
                    ViewBag.ListRole = RoleService.GetAll();
                    ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
                    return View("Create", model);
                }
                SetFlashMessage( string.Format("Thêm tài khoản '{0}' thành công.",model.UserName) );
                if (model.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListRole = RoleService.GetAll();
                ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
                return View("Create", model.ResetValue());

            }
            ViewBag.ListRole = RoleService.GetAll();
            ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
            return View("Create", model);
        }        
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var adminEdit = AdminAccountService.Find(id);
            if (adminEdit == null)
            {
                return Redirect("Index");
            }
            var model = new AdminModels
            {
                Id = adminEdit.ID,
                UserName = adminEdit.UserName,
                Email = adminEdit.Email,
                FullName =  adminEdit.FullName,
                Role =  adminEdit.Role ?? 0,
                Status = adminEdit.Status??false,
                Type = adminEdit.Type??0,
                AvatarUrl = adminEdit.Avatar

            };
            ViewBag.ListRole = RoleService.GetAll();
            ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
            return View("Edit", model);
        }
        [CheckPermission(2, "Sửa")]        
        [ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnEdit(AdminModels model)
        {
            if (ModelState.IsValid)
            {
                model.AvatarUrl = model.AvatarFile != null ?
                   model.AvatarFile.Upload() :
                   model.AvatarUrl.ToImageOriginalPath();
                var result = AdminAccountService.Update(new Data.Admin
                {
                    ID = model.Id,
                    UserName = model.UserName,
                    Password = model.Password,
                    Avatar = model.AvatarUrl,
                    Email = model.Email,
                    FullName = model.FullName,
                    Role = model.Role,
                    Date = model.Date,
                    Status = model.Status,
                    Type = model.Type,

                });
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại trên hệ thống.");
                    ViewBag.ListRole = RoleService.GetAll();
                    ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
                    return View("Edit", model);
                }
                SetFlashMessage(string.Format("Sửa tài khoản '{0}' thành công.", model.FullName));
                if (model.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListRole = RoleService.GetAll();
                ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
                return View("Edit", model);

            }
            ViewBag.ListRole = RoleService.GetAll();
            ViewBag.ListAccountType = DataHelper.ListEnumType<AccountType>();
            return View("Edit", model);
        }      
        [CheckPermission(3, "Xóa")]
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult OnDelete(int id)
        {
            var result = AdminAccountService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa tài khoản thành công." :
                "Tài khoản không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }  
        [SkipCheckPermission]     
        [HttpPost]
        [ActionName("CheckUserName")]
        public string OnCheckUserName(string userName)
        {
            return  AdminAccountService.CheckUserName(userName);
        }       
        [CheckPermission(4, "Thay đổi trạng thái")]
        [HttpPost]
        public ActionResult ChangeStatus(int id)
        {
            var result = AdminAccountService.ChangeStatus(id);
            SetFlashMessage(result == Result.Ok
                ? "Thay đổi trạng thái tài khoản thành công."
                : "Không tìm thấy tài khoản cần thay đổi trạng thái.");
            return RedirectToAction("Index");
        }
        [CheckPermission(5, "Đổi mật khẩu")]       
        public ActionResult ChangePassword(int id)
        {
            var adminAccountPassword = AdminAccountService.Find(id);
            if (adminAccountPassword == null)
            {
                return RedirectToAction("Index");

            }
            var adminChangePasswordModels = new AdminChangePassworModels
            {
                Id = adminAccountPassword.ID,
                UserName = adminAccountPassword.UserName
            };

            return View("ChangePassword", adminChangePasswordModels);

        }
        [CheckPermission(5, "Đổi mật khẩu")]
        [ActionName("ChangePassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnChangePassword(AdminChangePassworModels admin)
        {
            if (ModelState.IsValid)
            {
                var result = AdminAccountService.ChangePassword(admin.Id, admin.NewPassword);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Khách hàng không tồn tại trên hệ thống.");
                    return View("ChangePassword", admin);
                }

                SetFlashMessage("Thay đổi mật khẩu thành công.");
                if (admin.SaveList)
                {
                    return RedirectToAction("Index");
                }
                return View("ChangePassword", admin);
            }
            return View("ChangePassword", admin);
        }


    }
}