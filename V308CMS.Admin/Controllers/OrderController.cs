using System;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Đơn hàng")]
    public class OrderController : BaseController
    {
        public ActionResult Index(
            byte searchType = (byte)OrderSearchTypeEnum.All,
            byte status =0,
            string keyword ="", 
            string startDate = "", 
            string endDate ="",
            int page =1, 
            int pageSize =25

            )
        {
            DateTime startDateValue;
            DateTime endDateValue;
            DateTime.TryParse(startDate, out startDateValue);
            DateTime.TryParse(endDate, out endDateValue);
            int totalRecords;
            int totalPages = 0;
            var data = OrderService.GetListOrder(searchType, keyword, status, startDateValue, endDateValue, out totalRecords, page,pageSize);            
            if (totalRecords > 0)
            {

                totalPages = totalRecords / pageSize;
                if (totalRecords % pageSize > 0)
                    totalPages += 1;
            }
            ViewBag.ListSearchType = DataHelper.ListEnumType<OrderSearchTypeEnum>();
            ViewBag.ListStatus = DataHelper.ListEnumType<OrderStatusEnum>();
            var model = new OrderViewModels
            {
                SearchType = searchType,
                Keyword = keyword,
                Status = status,
                StartDate = startDate,
                EndDate = endDate,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                Data = data
            };
            return View("Index", model);
        }

        public ActionResult Detail(int id)
        {
            return View("Detail");
        }

        public ActionResult OnUpdate()
        {
            return View("Edit");

        }

        public ActionResult OnChangeStatus()
        {
            return Content("Ok");
        }
        [HttpPost]
        public ActionResult OnDelete()
        {
            return Content("Ok");
        }


       // #region ORDER
        
       // [CheckAdminAuthorize(3)]
       // public ActionResult Index(int? pType, int? pPage)
       // {
           
       //     ProductPage mProductPage = new ProductPage();
       //     if (pType == null)
       //     {
       //         if (Session["OrderType"] != null)
       //             pType = (int)Session["OrderType"];
       //         else
       //             pType = 0;
       //     }
       //     else
       //     {
       //         Session["OrderType"] = pType;
       //     }
       //     if (pPage == null)
       //     {
       //         if (Session["OrderPage"] != null)
       //             pPage = (int)Session["OrderPage"];
       //         else
       //             pPage = 1;
       //     }
       //     else
       //     {
       //         Session["OrderPage"] = pPage;
       //     }
       //     #endregion
       //     /*Lay danh sach cac tin theo page*/
       //     var mProductList = ProductsService.LayOrderTheoTrangAndType((int)pPage, 6, (int)pType);
       //     //lay danh sach cac kieu san pham
       //     if (mProductList.Count < 6)
       //         mProductPage.IsEnd = true;
       //     //Tao Html cho danh sach tin nay
       //     mProductPage.Html = V308HTMLHELPER.TaoDanhSachHoaDon(mProductList, (int)pPage);
       //     mProductPage.Page = (int)pPage;
       //     mProductPage.TypeId = (int)pType;
       //     return View("Index", mProductPage);
       // }        
       // [CheckAdminJson(3)]
       // [HttpPost]        
       // public JsonResult OnDelete(int pId = 0)
       // {          
       //     var mProductOrder = ProductsService.LayProductOrderTheoId(pId);
       //     if (mProductOrder != null)
       //     {
       //         MpStartEntities.DeleteObject(mProductOrder);
       //         MpStartEntities.SaveChanges();
       //         return Json(new { code = 1, message = "Xóa thành công!" });
       //     }
       //     return Json(new { code = 0, message = "Không tìm thấy hóa đơn cần xóa." });

       // }      
       // [CheckAdminAuthorize(3)]
       // public ActionResult Detail(int pId = 0)
       // {           
       //     ProductPage mProductPage = new ProductPage();
       //     var mProductOrder = ProductsService.LayProductOrderTheoId(pId);
       //     if (mProductOrder != null)
       //     {
       //         var mShopCart = JsonSerializer.DeserializeFromString<ShopCart>(mProductOrder.ProductDetail);
       //         mProductPage.pProductOrder = mProductOrder;
       //         mProductPage.Voucher = mShopCart.Voucher;
       //         mProductOrder.ProductDetail = V308HTMLHELPER.TaoDanhSachSanPhamGioHangAdmin(mShopCart.List);
       //         mProductPage.ShopCart = mShopCart;
       //     }
       //     else
       //     {
       //         mProductPage.pProductOrder = new ProductOrder();
       //         mProductPage.Html = "Không tìm thấy sản phẩm.";
       //     }
       //     return View("Detail", mProductPage);
       // }        
       // [CheckAdminAuthorize(3)]
       // public ActionResult OrderExportToExcel(int pId = 0)
       //{
       //     ProductOrder mProductOrder;
       //     ProductPage mProductPage = new ProductPage();
       //     ShopCart mShopCart = new ShopCart();
       //     StringBuilder Str = new StringBuilder();
       //     mProductOrder = ProductsService.LayProductOrderTheoId(pId);
       //     if (mProductOrder != null)
       //     {
       //         mShopCart = JsonSerializer.DeserializeFromString<ShopCart>(mProductOrder.ProductDetail);
       //     }
       //     else
       //     {
       //         mProductPage.pProductOrder = new ProductOrder();
       //         mProductPage.Html = "Không tìm thấy sản phẩm.";
       //     }
       //     //
       //     Response.ClearContent();
       //     Response.AddHeader("content-disposition", "attachement; filename=data.xls");
       //     Response.ContentType = "application/excel";

       //     Str.Append("<table>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"6\" style=\"text-align:center;font-size:16px;\">Công ty CP Thương Mại & Thực Phẩm Clean Food Việt Nam</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"6\">Số 42, Thái Thịnh 1, Thịnh Quang, Đống Đa, Hà Nội</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"6\"  style=\"text-align:center;font-size:16px;font-weight:bold;\">ĐẶC SẢN VÙNG CAO TÂM VIỆT</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"3\">Tel: 04.668.866.15</td>");
       //     Str.Append("<td colspan=\"3\">Hotline: 093.94.88883</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"6\">Web site : dacsanvungcaovn.com - dacsantamviet.com</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"6\">Facebook: Đặc Sản Vùng Cao</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"3\">Số Hóa Đơn: CF" + mProductOrder.ID + "</td>");
       //     Str.Append("<td colspan=\"3\">Ngày: " + mProductOrder.Date.ToString() + "</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"3\">Khách hàng:" + mProductOrder.FullName + "</td>");
       //     Str.Append("<td colspan=\"3\">Nợ trước:100%</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"3\">Địa chỉ:" + mProductOrder.Address + "</td>");
       //     Str.Append("<td colspan=\"3\">ĐT:" + mProductOrder.Phone + "</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"6\"  style=\"text-align:center;font-size:16px;font-weight:bold;\">PHIẾU THANH TOÁN</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td>ID</td>");
       //     Str.Append("<td>Tên Hàng</td>");
       //     Str.Append("<td>SL</td>");
       //     Str.Append("<td>Giá Bán</td>");
       //     Str.Append("<td>%</td>");
       //     Str.Append("<td>Thành tiền</td>");
       //     Str.Append("</tr>");
       //     foreach (Product it in mShopCart.List)
       //     {
       //         Str.Append("<tr>");
       //         Str.Append("<td>" + it.ID + "</td>");
       //         Str.Append("<td>" + it.Name + "</td>");
       //         Str.Append("<td>" + it.Number + "</td>");
       //         Str.Append("<td>" + String.Format("{0: 0,0}", (it.Price)) + "</td>");
       //         Str.Append("<td>" + it.SaleOff + " %</td>");
       //         Str.Append("<td>" + String.Format("{0: 0,0}", (it.Price - ((it.Price / 100) * it.SaleOff))) + "</td>");
       //         Str.Append("</tr>");
       //     }
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Tổng tiền hàng</td>");
       //     Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.getTotalBefore())) + "</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Giảm giá</td>");
       //     Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.Voucher)) + "  %</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Thực thu</td>");
       //     Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.getTotalPrice())) + "</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Phí ship</td>");
       //     Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.ServicePrice)) + "</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Dịch vụ sơ chế </td>");
       //     Str.Append("<td>" + String.Format("{0: 0,0}", (0)) + "</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Dịch vụ Tẩm ướp</td>");
       //     Str.Append("<td>" + String.Format("{0: 0,0}", (0)) + "</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"5\" style=\"font-weight:bold;\">Tổng tiền phải thanh toán </td>");
       //     Str.Append("<td>" + String.Format("{0: 0,0}", (mShopCart.getTotalPrice())) + "</td>");
       //     Str.Append("</tr>");
       //     Str.Append("<tr>");
       //     Str.Append("<td colspan=\"6\">Đặc sản vùng cao Tâm Việt cảm ơn quý khách đã ủng hộ</td>");
       //     Str.Append("</tr>");
       //     Str.Append("</table>");

       //     Response.Output.Write(Str.ToString());
       //     Response.Flush();
       //     Response.End();
       //     return View();
       // }
       // [HttpPost]       
       // [CheckAdminJson(3)]
       // [ValidateInput(false)]
       // public JsonResult OrderXacNhan(int pId = 0)
       // {            
       //     var mProductOrder = ProductsService.LayProductOrderTheoId(pId);
       //     if (mProductOrder != null)
       //     {
       //         mProductOrder.Status = 1;
       //         MpStartEntities.SaveChanges();
       //         return Json(new { code = 1, message = "Xác nhận thành công!" });
       //     }
       //     return Json(new { code = 0, message = "Không tìm thấy hóa đơn cần Xác nhận." });

       // }      
    }
}