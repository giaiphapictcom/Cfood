using System;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class ProductDistributorController : BaseController
    {
        #region NHA PHAN PHOI
       
        [CheckAdminAuthorize(1)]
        public ActionResult Index(int? pPage)
        {            
            ProductPage mProductPage = new ProductPage();
            if (pPage == null)
            {
                if (Session["NhaPhanPhoiPage"] != null)
                    pPage = (int)Session["NhaPhanPhoiPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["NhaPhanPhoiPage"] = pPage;
            }
            #endregion
            /*Lay danh sach cac tin theo page*/
            var mProductDistributor = ProductsService.LayProductDistributorTheoTrang((int)pPage, 10);
            if (mProductDistributor.Count < 10)
                mProductPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mProductPage.Html = V308HTMLHELPER.TaoDanhSachProductDistributor(mProductDistributor, (int)pPage);
            mProductPage.Page = (int)pPage;
            return View("Index", mProductPage);
        }       
        [CheckAdminJson(1)]
        [HttpPost]        
        public JsonResult OnDelete(int pId = 0)
        {           
            var mProductDistributor = ProductsService.LayProductDistributorTheoId(pId);
            if (mProductDistributor != null)
            {
                MpStartEntities.DeleteObject(mProductDistributor);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
        }       
        [CheckAdminAuthorize(1)]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]       
        [CheckAdminJson(1)]
        [ValidateInput(false)]      
        public JsonResult OnCreate(string pTieuDe, int? pUuTien, string pSummary, string pUrlImage)
        {
            
            var mProductDistributor = new ProductDistributor()
            {
                Date = DateTime.Now,
                Number = pUuTien,
                Name = pTieuDe,
                Detail = pSummary,
                Image = pUrlImage,
                Status = true,
                Visible = true
            };
            MpStartEntities.AddToProductDistributor(mProductDistributor);
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Lưu loại ảnh thành công." });

        }       
        [CheckAdminAuthorize(1)]
        public ActionResult Edit(int pId = 0)
        {          
            ProductPage mProductPage = new ProductPage();
            var mProductDistributor = ProductsService.LayProductDistributorTheoId(pId);
            if (mProductDistributor != null)
            {
                mProductPage.pProductDistributor = mProductDistributor;
            }
            else
            {
                mProductPage.Html = "Không tìm thấy tin tức cần sửa.";
            }
            return View("Edit", mProductPage);
        }
        [HttpPost]     
        [CheckAdminJson(1)]
        [ValidateInput(false)]       
        public JsonResult OnEdit(int pId, string pTieuDe, int? pUuTien, string pSummary, string pUrlImage)
        {           
            var mProductDistributor = ProductsService.LayProductDistributorTheoId(pId);
            if (mProductDistributor != null)
            {
                mProductDistributor.Name = pTieuDe;
                mProductDistributor.Date = DateTime.Now;
                mProductDistributor.Number = pUuTien;
                mProductDistributor.Detail = pSummary;
                mProductDistributor.Image = pUrlImage;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa thể loại ảnh thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy loại ảnh để sửa." });

        }     
    }
}