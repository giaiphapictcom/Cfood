using System;
using System.Web.Mvc;

namespace V308CMS.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
       
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
