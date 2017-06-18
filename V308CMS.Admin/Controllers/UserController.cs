using System;
using System.ComponentModel;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Khách hàng")]
    public class UserController : BaseController
    {
        //
        // GET: /User/        
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index(int status =0)
        {
            ViewBag.ListStateFilter = DataHelper.ListEnumType<StateFilterEnum>();
            return View("Index", UserService.GetList(status));
        }
        [SkipCheckPermission]
        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            var result = AccountService.CheckEmail(email);
            return Json(result);

        }      
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            return View("Create", new UserModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        
        public ActionResult OnCreate(UserModels user)
        {
            if (ModelState.IsValid)
            {
                var newAccount = new Account
                {
                    Email = user.Email,
                    UserName = user.Username,
                    Phone = user.Phone,
                    FullName = user.FullName,
                    Salt = StringHelper.GenerateString(6),
                    Avata = user.Avatar != null
                        ? user.Avatar.Upload()
                        : user.AvatarUrl
                };
                newAccount.Password = EncryptionMD5.ToMd5($"{user.Password}|{newAccount.Salt }");
                newAccount.Address = user.Address;
                newAccount.Gender = user.Gender;
                newAccount.Date = user.CreateDate;
                DateTime birthDayValue;
                DateTime.TryParse(user.BirthDay, out birthDayValue);

                newAccount.BirthDay = birthDayValue;
                newAccount.Status = user.Status;
                var result = UserService.Insert(newAccount);
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Khách hàng {user.Email} đã được sử dụng để đăng ký.");
                    return View("Create", user);
                }
                SetFlashMessage("Thêm khách hàng thành công.");
                if (user.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                return View("Create", user.ResetValue());



            }
            return View("Create", user);
        }        
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var user = UserService.Find(id);
            if (user == null)
            {
                return RedirectToAction("Index");

            }
            var userEdit = new UserModels
            {
                Id = user.ID,
                Username = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Gender = user.Gender.HasValue && user.Gender.Value,
                BirthDay = user.BirthDay?.ToString("dd/MM/yyyy") ?? "",
                Status = user.Status ?? false,
                AvatarUrl = user.Avata
            };

            return View("Edit", userEdit);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnEdit(UserModels user)
        {
            if (ModelState.IsValid)
            {
                var userUpdate = new Account
                {
                    Email = user.Email,
                    UserName = user.Username,
                    Phone = user.Phone,
                    FullName = user.FullName,
                    Avata = user.Avatar != null
                        ? user.Avatar.Upload()
                        : user.AvatarUrl.ToImageOriginalPath(),
                    Address = user.Address,
                    Gender = user.Gender,
                    Date = user.CreateDate
                };
                DateTime birthDayValue;
                DateTime.TryParse(user.BirthDay, out birthDayValue);

                userUpdate.BirthDay = birthDayValue;
                userUpdate.Status = user.Status;

                var result = UserService.Update(userUpdate);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", $"Tài khoản {user.Email} không tồn tại trên hệ thống.");
                    return View("Edit", user);
                }
                SetFlashMessage("Cập nhật tài khoản thành công.");
                if (user.SaveList)
                {
                    return RedirectToAction("Index");
                }               
                return View("Edit", user);
            }


            return View("Edit", user);
        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]        
        [ActionName("Delete")]        
        [ValidateAntiForgeryToken]
        public ActionResult OnDelete(int id)
        {
            var result = UserService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa khách hàng thành công." :
                "Khách hàng không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }
        [HttpPost]
        [CheckPermission(4, "Thay đổi trạng thái")]
        [ActionName("ChangeStatus")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnChangeStatus(int id)
        {
            var result = UserService.ChangeStatus(id);
            SetFlashMessage(result == Result.Ok
                ? $"Thay đổi trạng thái khách hàng thành công."
                : "Không tìm thấy khách hàng cần thay đổi trạng thái.");
            return RedirectToAction("Index");

        }
        [CheckPermission(5, "Đổi mật khẩu")]            
        public ActionResult ChangePassword(int id)
        {
            var userChangePassword = UserService.Find(id);
            if (userChangePassword == null)
            {
                return RedirectToAction("Index");

            }
            var userChangePasswordModels = new UserChangePassworModels
            {
                Id = userChangePassword.ID,
                UserName = userChangePassword.UserName
            };

            return View("ChangePassword", userChangePasswordModels);

        }
        [HttpPost]
        [CheckPermission(5, "Đổi mật khẩu")]
        [ActionName("ChangePassword")]        
        [ValidateAntiForgeryToken]
        public ActionResult OnChangePassword(UserChangePassworModels user)
        {
            if (ModelState.IsValid)
            {
                var result = UserService.ChangePassword(user.Id, user.NewPassword);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Khách hàng không tồn tại trên hệ thống.");
                    return View("ChangePassword", user);
                }

                SetFlashMessage("Thay đổi mật khẩu thành công.");
                if (user.SaveList)
                {
                    return RedirectToAction("Index");
                }
                return View("ChangePassword", user);
            }
            return View("ChangePassword", user);
        }
       
    }
}

