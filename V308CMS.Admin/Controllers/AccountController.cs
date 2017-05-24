using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class AccountController : BaseController
    {
        #region Account
       
        [CheckAdminAuthorize(6)]
        public ActionResult Index(int? pType, int? pPage)
        {
         
            var mAccountPage = new AccountPage();
            if (pType == null){
                if (Session["AccountType"] != null)
                    pType = (int)Session["AccountType"];
                else
                    pType = 0;
            }
            else{
                Session["AccountType"] = pType;
            }
            if (pPage == null){
                if (Session["AccountPage"] != null)
                    pPage = (int)Session["AccountPage"];
                else
                    pPage = 1;
            }
            else{
                Session["AccountPage"] = pPage;
            }
            #endregion
            /*Lay danh sach cac tin theo page*/
            var mmAccountList = AccountService.LayAccountTheoTrangAndType((int)pPage, 10, (int)pType);
            if (mmAccountList.Count < 10)
                mAccountPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mAccountPage.Html = V308HTMLHELPER.TaoDanhSachCacAccount(mmAccountList, (int)pPage);
            mAccountPage.Page = (int)pPage;
            mAccountPage.TypeId = (int)pType;
            return View("Index", mAccountPage);
           
          
        }     
        [CheckAdminJson(6)]
        [HttpPost]        
        public JsonResult OnDelete(int pId = 0)
        {
            var mAccount = AccountService.LayTinTheoId(pId);
            if (mAccount != null)
            {
                MpStartEntities.DeleteObject(mAccount);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });


        }

        [CheckAdminJson(6)]
        [HttpPost]       
        public JsonResult OnChangeStatus(int pId = 0)
        {
            
            var mAccount = AccountService.LayTinTheoId(pId);
            if (mAccount != null)
            {
                mAccount.Status = ! mAccount.Status;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = (mAccount.Status == true ? "Kích hoạt thành công!" : "Khóa tài khoản thành công!") });
            }
            return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });
        }      
      
        [CheckAdminAuthorize(6)]
        public ActionResult Detail(int pId = 0)
        {
         
            var mAccountPage = new AccountPage();
            var mAccount = AccountService.LayTinTheoId(pId);
            if (mAccount != null)
            {
                mAccountPage.pAccount = mAccount;
            }
            else
            {
                mAccountPage.Html = "Không tìm thấy tài khoản muốn sửa";
                mAccountPage.pAccount = new Account();
            }
            return View("Detail", mAccountPage);
          
        }

    }
}