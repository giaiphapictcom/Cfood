using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Thông tin cá nhân")]
    public class ProfileController : BaseController
    {
        //
        // GET: /UserProfile/
        [CheckPermission(0, "Cập nhật")]
        public ActionResult UpdateProfile()
        {
            var profile = AdminAccountService.Find(User.UserId);
            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new AdminModels
            {
                FullName = profile.FullName,
                AvatarUrl = profile.Avatar,
                Email = profile.Email
            };
            return View("UpdateProfile", model);
        }
        [HttpPost]
        [CheckPermission(0, "Cập nhật")]       
        [ActionName("UpdateProfile")]
        public ActionResult OnUpdateProfile(AdminModels model)
        {
            model.AvatarUrl = model.AvatarFile != null ?
                  model.AvatarFile.Upload() :
                  model.AvatarUrl.ToImageOriginalPath();
            var result = AdminAccountService.UpdateProfile(User.UserId, model.AvatarUrl, model.Email, model.FullName);

            if (result == Result.NotExists)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại trên hệ thống.");
                return View("UpdateProfile", model);
            }
            SetFlashMessage("Cập nhật thông tin cá nhân thành công.");
            return View("UpdateProfile", model);
        }
        [CheckPermission(1, "Đổ mật hkẩu")]
        public ActionResult ChangePassword()
        {
            var profile = AdminAccountService.Find(User.UserId);
            if (profile == null)
            {
                return Redirect("Index");
            }         
            return View("ChangePassword", new ProfileChangePassworModels());
        }
        [HttpPost]
        [CheckPermission(1, "Đổ mật hkẩu")]
        [ActionName("ChangePassword")]
        [ValidateAntiForgeryToken]        
        public ActionResult OnChangePassword(ProfileChangePassworModels model)
        {
            if (ModelState.IsValid)
            {
                var result = AdminAccountService.ChangePassword(User.UserId, model.OldPassword, model.NewPassword);
                if (result == "old_not_correct")
                {
                    ModelState.AddModelError("OldPassword","Mật khẩu hiện tại không đúng.");
                    return View("ChangePassword", model);
                }
                if (result == "not_exists")
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại trên hệ thống.");
                    return View("ChangePassword", model);
                }
                return RedirectToAction("LogOut", "Home");
            }
            return View("ChangePassword", model);
        }
    }
}
