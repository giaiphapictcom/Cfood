using System;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;
using V308CMS.Admin.Helpers;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Đơn hàng")]
    public class OrderController : BaseController
    {
        [CheckPermission(0, "Danh sách")]
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
        [CheckPermission(1, "Chi tiết")]
        public ActionResult Detail(int id)
        {
            var order = OrderService.FindToEdit(id);
            if (order == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ListStatus = DataHelper.ListEnumType<OrderStatusEnum>();
            return View("Edit", order.CloneTo<OrderModels>());
        }

        [CheckPermission(2, "Xóa")]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = OrderService.Delete(id);
            return RedirectToAction("Index");
            //return Json(new { code = result });
        }

        [CheckPermission(3, "Thay đổi trạng thái")]
        [HttpPost]
        [ActionName("ChangeStatus")]
        public ActionResult OnChangeStatus(int id, int status)
        {
            var result = OrderService.ChangeStatus(id, status);
            SetFlashMessage(result == Result.Ok ?
                "Thay đổi trạng thái thành công." :
                "đơn hàng không tồn tại trên hệ thống.");
            //return RedirectToAction("Index");
            return Json(new { code = result });
        }
        [CheckPermission(4, "Hủy đơn hàng")]
        [HttpPost]
        [ActionName("CancelOrder")]
        public ActionResult OnCancelOrder(int id)
        {
            var result = OrderService.ChangeStatus(id,(int) OrderStatusEnum.CancelledOrder);
           
            return RedirectToAction("Index");
            
        }

        [CheckPermission(5, "Cập nhật ghi chú")]
        [HttpPost]
        [ActionName("UpdateDetail")]
        public JsonResult OnUpdateDetail(int id,string detail)
        {
            var result = OrderService.UpdateDetail(id, detail);

            return Json(new {code = result});
        }
  
    }
}