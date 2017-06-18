using System;
using System.Web.Mvc;

namespace V308CMS.Admin.Models
{
    public class CheckAdminAuthorizeAttribute : AuthorizeAttribute
    {
        int pId = 0;
        public CheckAdminAuthorizeAttribute(int pValue)
        {
            pId = pValue;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (!(filterContext.HttpContext.Session["Admin"] == null))
            {
                Data.Admin mAdmin = (Data.Admin)filterContext.HttpContext.Session["Admin"];
                //if (mAdmin.PSanPham != true && pId==1)
                //{
                //    RedirectResult mRS = new RedirectResult("/Home/ChucNang");
                //    filterContext.Result = mRS;
                //}
                //else if (mAdmin.PTinTuc != true && pId == 2)
                //{
                //    RedirectResult mRS = new RedirectResult("/Home/ChucNang");
                //    filterContext.Result = mRS;
                //}
                //else if (mAdmin.PKhachHang != true && pId == 3)
                //{
                //    RedirectResult mRS = new RedirectResult("/Home/ChucNang");
                //    filterContext.Result = mRS;
                //}
                //else if (mAdmin.PHinhAnh != true && pId == 4)
                //{
                //    RedirectResult mRS = new RedirectResult("/Home/ChucNang");
                //    filterContext.Result = mRS;
                //}
                //else if (mAdmin.PFileUpload != true && pId == 5)
                //{
                //    RedirectResult mRS = new RedirectResult("/Home/ChucNang");
                //    filterContext.Result = mRS;
                //}
                //else if (mAdmin.PTaiKhoan != true && pId == 6)
                //{
                //    RedirectResult mRS = new RedirectResult("/Home/ChucNang");
                //    filterContext.Result = mRS;
                //}
                //else if (mAdmin.PHeThong != true && pId == 7)
                //{
                //    RedirectResult mRS = new RedirectResult("/Home/ChucNang");
                //    filterContext.Result = mRS;
                //}
                //else if (mAdmin.PThungRac != true && pId == 8)
                //{
                //    RedirectResult mRS = new RedirectResult("/Home/ChucNang");
                //    filterContext.Result = mRS;
                //}
            }
            else
            {
                RedirectResult mRS = new RedirectResult("/Home/ChucNang");
                filterContext.Result = mRS;
            }
        }
    }
    public class CheckAdminJsonAttribute : AuthorizeAttribute
    {
        int pId = 0;
        public CheckAdminJsonAttribute(int pValue)
        {
            pId = pValue;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.HttpContext.Session["Admin"] != null)
            {
                Data.Admin mAdmin = (Data.Admin)filterContext.HttpContext.Session["Admin"];
                //if (mAdmin.PSanPham != true && pId == 1)
                //{
                //    JsonResult mCR = new JsonResult();
                //    mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                //    filterContext.Result = mCR;
                //}
                //else if (mAdmin.PTinTuc != true && pId == 2)
                //{
                //    JsonResult mCR = new JsonResult();
                //    mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                //    filterContext.Result = mCR;
                //}
                //else if (mAdmin.PKhachHang != true && pId == 3)
                //{
                //    JsonResult mCR = new JsonResult();
                //    mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                //    filterContext.Result = mCR;
                //}
                //else if (mAdmin.PHinhAnh != true && pId == 4)
                //{
                //    JsonResult mCR = new JsonResult();
                //    mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                //    filterContext.Result = mCR;
                //}
                //else if (mAdmin.PFileUpload != true && pId == 5)
                //{
                //    JsonResult mCR = new JsonResult();
                //    mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                //    filterContext.Result = mCR;
                //}
                //else if (mAdmin.PTaiKhoan != true && pId == 6)
                //{
                //    JsonResult mCR = new JsonResult();
                //    mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                //    filterContext.Result = mCR;
                //}
                //else if (mAdmin.PHeThong != true && pId == 7)
                //{
                //    JsonResult mCR = new JsonResult();
                //    mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                //    filterContext.Result = mCR;
                //}
                //else if (mAdmin.PThungRac != true && pId == 8)
                //{
                //    JsonResult mCR = new JsonResult();
                //    mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                //    filterContext.Result = mCR;
                //}
            }
            else
            {
                JsonResult mCR = new JsonResult();
                mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                filterContext.Result = mCR;
            }
        }
    }

    public class CheckDeleteAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (!(filterContext.HttpContext.Session["Admin"] == null))
            {
                Data.Admin mAdmin = (Data.Admin)filterContext.HttpContext.Session["Admin"];
                if (mAdmin.Role != 1)
                {
                    JsonResult mCR = new JsonResult();
                    mCR.Data = new { code = 0, message = "Bạn không có quyền thực hiện hành động này. Chỉ Administrator mới có quyền thêm các tài khoản quản trị. Hãy liên hệ với Administrator của website để nâng cấp quyền cho tài khoản của bạn.Vào menu: Tài khoản--> Tài khoản quản trị--> Thêm mới." };
                    filterContext.Result = mCR;
                }
            }
            else
            {
                JsonResult mCR = new JsonResult();
                mCR.Data = new { code = 0, message = "Bạn không có quyền truy cập chức năng này." };
                filterContext.Result = mCR;
            }
        }
    }

}