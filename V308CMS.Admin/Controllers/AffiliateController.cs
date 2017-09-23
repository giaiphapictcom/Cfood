using System.Web.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using V308CMS.Admin.Models;
using V308CMS.Data;
using V308CMS.Admin.Attributes;
using V308CMS.Common;
using V308CMS.Data.Models;
using V308CMS.Data.Enum;
using V308CMS.Admin.Helpers;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Tài khoản Affiliate")]
    public class AffiliateController : BaseController
    {

        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            return View("Index", UserService.GetList(-1, Data.Helpers.Site.affiliate));
        }

        [CheckPermission(0, "Danh sách")]
        public ActionResult accounts(int status = -1)
        {
            return View("affiliate_accounts", UserService.GetList(status, Data.Helpers.Site.affiliate));
        }

        [CheckPermission(0, "Danh sách")]
        public ActionResult Links(int id=0)
        {
            var Model = LinkRepo.GetItems(1,id,-1);
            return View("links", Model);
        }

        [CheckPermission(0, "Danh sách")]
        public ActionResult Vouchers(int id=0)
        {
            var Model = VoucherRepo.GetList(1, -1,-1,id);
            return View("vouchers", Model);
        }

        [CheckPermission(0, "Danh sách")]
        public ActionResult Website()
        {
            var Model = WebsiteRequestRepo.GetList();
            return View("WebsiteRequest", Model);
        }

        [CheckPermission(0, "Danh sách")]
        public ActionResult Orders(int id=0)
        {
            
            int totalRecords;
            var searchType = (byte)0;
            var keyword = string.Empty;
            var status = -1;
            var startDateValue = DateTime.MinValue;
            var endDateValue = DateTime.MinValue;
            var page = 1;
            var orders = new List<ProductOrder>();

            var members = UserService.GetMemberIdOfAffiliate(id);
            if (members.Count > 0) {

                orders = OrderService.GetListOrder(searchType, keyword, status, startDateValue, endDateValue, out totalRecords, page, -1);
                orders = orders.Where(o => members.Contains((int)o.AccountID)).ToList();
            }

            var model = new OrderViewModels
            {
                SearchType = (byte)OrderSearchTypeEnum.All,
                Data = orders
            };

            return View("Orders", model);
        }
        [HttpPost]

        public ActionResult RevenuePay(int id=0, string pay = "")
        {
            double payed;
            double.TryParse(pay, out payed);

            var result = OrderService.UpdateRevenuePay(id, payed);
            if (result != Result.Ok)
            {
                return Json(new { message = "Lỗi xảy ra" });
            }
            return Json(new { message = "Cập nhật thành công." });
        }

       
        public JsonResult codes() {
            var CodeItem = new List<AffilateCode>();
            //int page = 1;
            //int psize = 4 * 20;
            using (var mEntities = new V308CMSEntities()) {
                var used = mEntities.Account.Where(a => a.affiliate_code.Length > 0 && a.affiliate_code != string.Empty && a.affiliate_code != "").ToList();
                var CodeQuery = mEntities.AffilateCode.Where(c => c.Status == 1);
                //CodeQuery = CodeQuery.Where(c=> !used.Any(u=>u.affiliate_code == c.Code));
                CodeQuery = CodeQuery.Where(c=>!mEntities.Account.Any(a=>a.affiliate_code==c.Code));
                //!db.Fi.Any(f => f.UserID == user.UserID)
                CodeItem = CodeQuery.OrderBy(c => c.Code).ToList();
                //CodeItem = CodeItem.Skip((page - 1) * psize).Take(psize).ToList();

            }
            return Json(new
            {
                code = 1,
                items = CodeItem
            },JsonRequestBehavior.AllowGet);
        }
    }
}
