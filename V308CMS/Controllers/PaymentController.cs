using System;
using System.Resources;
using System.Web.Mvc;
using V308CMS.Data.Enum;
using V308CMS.Helpers.Discount;

namespace V308CMS.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public JsonResult UseVoucher(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return Json(new { code = 0, message = "Mã giảm giá trống." });
            }
            var voucherCode = VoucherCodeService.Find(code);
            if (voucherCode == null)
            {
                return Json(new { code = 0, message = "Mã giảm giá không đúng." });
            }
            if (voucherCode.State == (byte)StateEnum.Active)
            {
                return Json(new { code = 0, message = "Mã giảm giá này đã được sử dụng." });
            }
            if (voucherCode.Voucher.State == (int) StateEnum.Disable)
            {
                return Json(new { code = 0, message = "Voucher này không hữu dụng." });
            }
            if (voucherCode.Voucher.StartDate.HasValue && (DateTime.Now - voucherCode.Voucher.StartDate.Value).TotalDays <= 0)
            {
                return Json(new { code = 0, message = "Voucher này không hữu dụng." });
            }
            if (voucherCode.Voucher.ExpireDate.HasValue &&(voucherCode.Voucher.ExpireDate.Value - DateTime.Now).TotalDays>0)
            {
                return Json(new { code = 0, message = "Voucher này hiện đã hết hạn sử dụng." });
            }
            Discount discount = null;
            if (voucherCode.Voucher.DiscountType == (int) DiscountTypeEnum.ByItem)
            {

                discount = new Discount
                {
                    Amount = voucherCode.Voucher.Amount,
                    DiscountRule = new DiscountItemRule()
                };

            }
            if (voucherCode.Voucher.DiscountType == (int)DiscountTypeEnum.BySubTotal)
            {

                discount = new Discount
                {
                    Amount = voucherCode.Voucher.Amount,
                    DiscountRule = new DiscountSubTotalRule()
                };

            }
            MyCart.Discount = discount;
            return Json(new { code = 1, message = "Sử dụng mã giảm giá thành công." });


        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult UseAffilate(string affilateId)
        {
            if (string.IsNullOrWhiteSpace(affilateId))
            {
                return Json(new {code = 0, message = "Mã Affilate trống."});
            }
            var affilateItem = AffilateUserService.GetByUserId(User.UserId);
            if (affilateItem == null || affilateItem.AffilateId != affilateId)
            {
                return Json(new { code = 0, message = "Mã Affilate không đúng." });
            }
            MyCart.AffilateAmount = affilateItem.Amount;
            return Json(new { code = 1, message = "Sử dụng mã Affilate thành công." });
        }

        public ActionResult BuyNow()
        {
            return View("BuyNow");
            
        }
       
        //
        // GET: /Payment/       
        public ActionResult Index()
        {         
            var transactionInfo = OrderTransactionService.GetByTransactionId(TransactionId);
            if (transactionInfo == null)
            {
                if (IsEmptyCart())
                {
                    return RedirectToAction("Index", "Home");
                   
                }
                return RedirectToAction("Checkout", "Cart");
            }
            ViewBag.Cart = MyCart;
            return View("Payment.Index", transactionInfo.Order);
        }
      
        public ActionResult Success()
        {          
            var transactionInfo = OrderTransactionService.GetByTransactionId(TransactionId);
            if (transactionInfo == null)
            {
                if (IsEmptyCart())
                {
                    return RedirectToAction("Index", "Home");
                   
                }
                return RedirectToAction("Checkout", "Cart");
            }
            //Hoan tat giao dich mua ban
            OrderTransactionService.CompleteTransaction(TransactionId, DateTime.Now);           
            //Ket thuc giao dich
            EndTransaction();
            //Xoa gio hang
            ClearCart();           
            return View("Payment.Success");

        }
    }
}
