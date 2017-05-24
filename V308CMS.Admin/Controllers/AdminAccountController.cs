using System;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class AdminAccountController : BaseController
    {
        #region AdminAccount
      
        [CheckAdminAuthorize(6)]
        public ActionResult Index(int? pType, int? pPage)
        {
           
            var mAccountPage = new AccountPage();
            if (pType == null)
            {
                if (Session["AdminAccountType"] != null)
                    pType = (int)Session["AdminAccountType"];
                else
                    pType = 0;
            }
            else
            {
                Session["AdminAccountType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["AdminAccountPage"] != null)
                    pPage = (int)Session["AdminAccountPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["AdminAccountPage"] = pPage;
            }
            #endregion
            /*Lay danh sach cac tin theo page*/
            var mmAccountList = AccountService.LayAdminTheoTrangAndType((int)pPage, 10, (int)pType);
            if (mmAccountList.Count < 10)
                mAccountPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mAccountPage.Html = V308HTMLHELPER.TaoDanhSachCacAdminAccount(mmAccountList, (int)pPage);
            mAccountPage.Page = (int)pPage;
            mAccountPage.TypeId = (int)pType;
            return View("Index", mAccountPage);

        }      
        [CheckAdminJson(6)]
        [HttpPost]       
        public JsonResult OnDelete(int pId = 0)
        {          
           
            var mAdmin = AccountService.LayAdminTheoId(pId);
            if (mAdmin != null)
            {
                MpStartEntities.DeleteObject(mAdmin);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });

        }        
        [CheckAdminAuthorize(6)]
        public ActionResult Create()
        {           
            return View("Create");           
        }
        [HttpPost]     
        [CheckAdminJson(6)]
        [CheckDelete]
        [ValidateInput(false)]     
        public JsonResult OnCreate(bool? phethong, bool? psanpham, bool? ptintuc, bool? pkhachhang, bool? phinhanh, bool? pupload, bool? ptaikhoan, bool? pthungrac, string pTitle, int? pGroupId, string pAccount, string pPassword1, string pPassword2, string pEmail)
        {

            if (pPassword1.Trim().Equals(pPassword2.Trim()))
            {
                if (pAccount.Length > 5 && pPassword1.Length > 5)
                {
                    var mAdmin = AccountService.LayAdminTheoUserName(pAccount);
                    if (!(mAdmin != null))
                    {
                        mAdmin = new Data.Admin()
                        {
                            Date = DateTime.Now,
                            Role = pGroupId,
                            FullName = pTitle,
                            Email = pEmail,
                            UserName = pAccount,
                            Password = EncryptionMD5.ToMd5(pPassword1.Trim()),
                            PSanPham = (psanpham),
                            PFileUpload = (pupload),
                            PHeThong = (phethong),
                            PHinhAnh = (phinhanh),
                            PKhachHang = (pkhachhang),
                            PTaiKhoan = (ptaikhoan),
                            PThungRac = (pthungrac),
                            PTinTuc = (ptintuc)
                        };
                        MpStartEntities.AddToAdmin(mAdmin);
                        MpStartEntities.SaveChanges();
                        return Json(new { code = 1, message = "Lưu  tài khoản thành công." });
                    }
                    return Json(new { code = 0, message = "Tài khoản đã tồn tại. Vui lòng tại tài khoản mới." });
                }
                return Json(new { code = 0, message = "Mật khẩu và tài khoản và có độ dài tối thiểu 6 kí tự." });
            }
            return Json(new { code = 0, message = "Mật khẩu xác nhận không trùng khớp." });

        }   
        [CheckAdminAuthorize(6)]
        public ActionResult Edit(int pId = 0)
        {
          
            var mAccountPage = new AccountPage();
            var mAdmin = AccountService.LayAdminTheoId(pId);
            if (mAdmin != null)
            {
                mAccountPage.pAdmin = mAdmin;
                mAccountPage.TypeId = (int)mAdmin.Role;
            }
            else
            {
                mAccountPage.Html = "Không tìm thấy tài khoản muốn sửa";
                mAccountPage.pAdmin = new Data.Admin();
            }
            return View("Edit", mAccountPage);
        }
        [HttpPost]       
        [CheckAdminJson(6)]
        [CheckDelete]
        [ValidateInput(false)]        
        public JsonResult OnEdit(bool? phethong, bool? psanpham, bool? ptintuc, bool? pkhachhang, bool? phinhanh, bool? pupload, bool? ptaikhoan, bool? pthungrac, int pId, string pTitle, int? pGroupId, string pAccount, bool? pActive, string pEmail)
        {          
            var mAdmin = AccountService.LayAdminTheoId(pId);
            if (mAdmin != null)
            {
                mAdmin.FullName = pTitle;
                mAdmin.Role = pGroupId;
                mAdmin.Email = pEmail;
                mAdmin.Status = (pActive);
                mAdmin.PSanPham = (psanpham);
                mAdmin.PFileUpload = (pupload);
                mAdmin.PHeThong = (phethong);
                mAdmin.PHinhAnh = (phinhanh);
                mAdmin.PKhachHang = (pkhachhang);
                mAdmin.PTaiKhoan = (ptaikhoan);
                mAdmin.PThungRac = (pthungrac);
                mAdmin.PTinTuc = (ptintuc);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Hoàn thành sửa thông tin." });
            }
            return Json(new { code = 0, message = "Không tìm thấy thông tin tài khoản." });
        }

      
        public ActionResult Info()
        {               
            AccountPage mAccountPage = new AccountPage();
            int pId = (int)Session["UserId"];
            var mAdmin = AccountService.LayAdminTheoId(pId);
            if (mAdmin != null)
            {
                mAccountPage.pAdmin = mAdmin;
            }
            else
            {
                mAccountPage.Html = "Không tìm thấy tài khoản muốn sửa";
                mAccountPage.pAdmin = new Data.Admin();
            }
            return View("Info", mAccountPage);
        }
        [HttpPost]        
        public JsonResult OnChangePassword(int pId, string pPassword1, string pPassword2, string pPassword3)
        {
            V308CMSEntities mEntities = new V308CMSEntities();           
            int mId = (int)Session["UserId"];
            var mAdmin = AccountService.LayAdminTheoId(mId);
            if (mAdmin != null)
            {
                if (mAdmin.Password.Trim().Equals(EncryptionMD5.ToMd5(pPassword1.Trim())))
                {
                    if (pPassword2.Trim().Equals(pPassword3.Trim()))
                    {
                        if (pPassword2.Length > 5)
                        {
                            mAdmin.Password = EncryptionMD5.ToMd5(pPassword2.Trim());
                            mEntities.SaveChanges();
                            return Json(new { code = 1, message = "thay đổi mật khẩu thành công." });
                        }
                        return Json(new { code = 0, message = "Mật khẩu và tài khoản và có độ dài tối thiểu 6 kí tự." });
                    }
                    return Json(new { code = 0, message = "Mật khẩu mới và Mật khẩu xác nhận không trùng khớp." });
                }
                return Json(new { code = 0, message = "Mật khẩu cũ không chính xác." });
            }
            return Json(new { code = 0, message = "Tài khoản không tồn tại." });
        }       
    }
}