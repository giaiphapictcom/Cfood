using System;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class ProductManufacturerController : BaseController
    {
        #region NHA SAN XUAT
     
        [CheckAdminAuthorize(1)]
        public ActionResult Index(int? pPage)
        {           
            ProductPage mProductPage = new ProductPage();
            if (pPage == null)
            {
                if (Session["NhaSanXuatPage"] != null)
                    pPage = (int)Session["NhaSanXuatPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["NhaSanXuatPage"] = pPage;
            }
            #endregion
            /*Lay danh sach cac tin theo page*/
            var mProductManufacturer = ProductsService.LayProductManufacturerTheoTrang((int)pPage, 10);
            if (mProductManufacturer.Count < 10)
                mProductPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mProductPage.Html = V308HTMLHELPER.TaoDanhSachProductManufacturer(mProductManufacturer, (int)pPage);
            mProductPage.Page = (int)pPage;
            return View("Index", mProductPage);
        }       
        [CheckAdminJson(1)]
        [HttpPost]
        [ActionName("Delete")]
        public JsonResult OnDelete(int pId = 0)
        {            
            var mProductManufacturer = ProductsService.LayProductManufacturerTheoId(pId);
            if (mProductManufacturer != null)
            {
                MpStartEntities.DeleteObject(mProductManufacturer);
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
        [ActionName("Create")]
        public JsonResult OnCreate(string pTieuDe, int? pUuTien, string pSummary, string pUrlImage)
        {
            var mProductManufacturer = new ProductManufacturer() { Date = DateTime.Now, Number = pUuTien, Name = pTieuDe, Detail = pSummary, Image = pUrlImage, Status = true, Visible = true };
            MpStartEntities.AddToProductManufacturer(mProductManufacturer);
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Lưu loại ảnh thành công." });
        }       
        [CheckAdminAuthorize(1)]
        public ActionResult Edit(int pId = 0)
        {           
            ProductPage mProductPage = new ProductPage();          
            var mProductManufacturer = ProductsService.LayProductManufacturerTheoId(pId);
            if (mProductManufacturer != null)
            {
                mProductPage.pProductManufacturer = mProductManufacturer;
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
        [ActionName("Edit")]
        public JsonResult OnEdit(int pId, string pTieuDe, int? pUuTien, string pSummary, string pUrlImage)
        {
            var mProductManufacturer = ProductsService.LayProductManufacturerTheoId(pId);
            if (mProductManufacturer != null)
            {
                mProductManufacturer.Name = pTieuDe;
                mProductManufacturer.Date = DateTime.Now;
                mProductManufacturer.Number = pUuTien;
                mProductManufacturer.Detail = pSummary;
                mProductManufacturer.Image = pUrlImage;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa thể loại ảnh thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy loại ảnh để sửa." });
        }        
    }
}