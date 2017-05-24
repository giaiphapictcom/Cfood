using System;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class MarketController : BaseController
    {
        #region Market
     
        [CheckAdminAuthorize(6)]
        public ActionResult Index(int? pType, int? pPage)
        {           
            MarketPage mMarketPage = new MarketPage();
            if (pType == null)
            {
                if (Session["MarketType"] != null)
                    pType = (int)Session["MarketType"];
                else
                    pType = 0;
            }
            else
            {
                Session["MarketType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["MarketPage"] != null)
                    pPage = (int)Session["MarketPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["MarketPage"] = pPage;
            }
            #endregion
            /*Lay danh sach cac tin theo page*/
            var mMarketList = MarketService.LayMarketTheoTrangAndType((int)pPage, 10, (int)pType);
            if (mMarketList.Count < 10)
                mMarketPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mMarketPage.Html = V308HTMLHELPER.TaoDanhSachCacMarket(mMarketList, (int)pPage);
            mMarketPage.Page = (int)pPage;
            mMarketPage.TypeId = (int)pType;
            return View("Index", mMarketPage);
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
        public JsonResult OnCreate(string pUserName, string pAvata, int pMarketType, string pEmail, string pFullName, string pMobile, string pSumary, bool pActive = true)
        {
            var mMarket = new Market()
            {
                Date = DateTime.Now,
                BirthDay = DateTime.Now,
                UserName = Ultility.LocDau2(pUserName.Trim()),
                Avata = pAvata,
                Email = pEmail,
                FullName = pFullName,
                Gender = true,
                Phone = pMobile,
                Role = pMarketType,
                Status = pActive,
                Sumary = pSumary
            };
            MpStartEntities.AddToMarket(mMarket);
            MpStartEntities.SaveChanges();
            //lay danh sách nhom san pham
            var mList = ProductsService.getProductTypeParent();
            foreach (ProductType it in mList)
            {
                MarketProductType mMarketProductType = new MarketProductType()
                {
                    Date = DateTime.Now,
                    Name = it.Name,
                    Detail = it.Name,
                    Parent = it.ID,
                    Status = true,
                    Visible = true,
                    Number = 1,
                    MarketId = mMarket.ID,
                    MarketName = mMarket.UserName,
                    ImageBanner = it.ImageBanner
                };
                MpStartEntities.AddToMarketProductType(mMarketProductType);
            }
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Lưu cửa hàng thành công." });

        }       
        [CheckAdminAuthorize(1)]
        public ActionResult Edit(int pId = 0)
        {         
            MarketPage mMarketPage = new MarketPage();
            var mMarket = MarketService.LayTheoId(pId);
            if (mMarket != null)
            {
                mMarketPage.Market = mMarket;
            }
            else
            {
                mMarketPage.Html = "Không tìm thấy cửa hàng cần sửa.";
            }
            return View("Edit", mMarketPage);
        }
        [HttpPost]        
        [CheckAdminJson(1)]
        [ValidateInput(false)]
        [ActionName("Edit")]
        public JsonResult OnEdit(int pId, string pUserName, string pAvata, string pEmail, int pMarketType, string pFullName, string pMobile, string pSumary, bool pActive = true)
        {           
            var mMarket = MarketService.LayTheoId(pId);
            if (mMarket != null)
            {
                mMarket.UserName = pUserName;
                mMarket.Date = DateTime.Now;
                mMarket.Avata = pAvata;
                mMarket.Email = pEmail;
                mMarket.FullName = pFullName;
                mMarket.Phone = pMobile;
                mMarket.Sumary = pSumary;
                mMarket.Status = pActive;
                mMarket.Role = pMarketType;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa cửa hàng thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy cửa hàng để sửa." });
        }        
        [CheckAdminJson(6)]
        [HttpPost]
        [ActionName("Delete")]
        public JsonResult OnDelete(int pId = 0)
        {            
            var mMarket = MarketService.LayTheoId(pId);
            if (mMarket != null)
            {
                //lay danh sach cac san pham cua sieu thi
                var mList = ProductsService.getByPageSizeMarketId(1, 1000, mMarket.ID);
                //xoa cac san pham nay
                foreach (Product it in mList)
                {
                    MpStartEntities.DeleteObject(it);
                }
                MpStartEntities.DeleteObject(mMarket);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });

        }
        [CheckAdminJson(6)]
        [HttpPost]
        [ActionName("ChangeStatus")]
        public JsonResult OnChangeStatus(int pId = 0)
        {
           
            var mMarket = MarketService.LayTheoId(pId);
            if (mMarket != null)
            {
                mMarket.Status = !mMarket.Status;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message =(mMarket.Status == true? "Kích hoạt thành công!": "Khóa tài khoản thành công!")  });
            }
            return Json(new { code = 0, message = "Không tìm thấy tài khoản cần xóa." });

        }
        
       
    }
}