﻿using System;
using System.Collections.Generic;
using System.Resources;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Enum;
using V308CMS.Data.Models;
using V308CMS.Helpers.Discount;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    
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
        [AllowAnonymous]
        public ActionResult BuyNow()
        {
            var shippingAddress = ShippingService.FistByIpAddress(IpHelper.ClientIpAddress);
            var shipModel = new ShippingModelsAnonymous();
            int region = 0,city = 0, ward =0;
          
            if (shippingAddress != null)
            {
                shipModel.Email = shippingAddress.Email;
                shipModel.FullName = shippingAddress.FullName;
                var listRegion = RegionService.GetListInByName(shippingAddress.Region, shippingAddress.City,
                shippingAddress.Ward);
                if (listRegion != null  && listRegion.Count > 0)
                {
                    region = listRegion[0].Id;
                    if (listRegion.Count > 1)
                    {
                        city = listRegion[1].Id;
                    }
                    if (listRegion.Count > 2)
                    {
                        ward = listRegion[2].Id;
                    }


                }              
                shipModel.Region = region;
                shipModel.Ward = ward;
                shipModel.City = city;
                shipModel.Phone = shippingAddress.Phone;
                shipModel.Address = shippingAddress.Address;

            }
            ViewBag.Cart = MyCart;
            ViewBag.ListRegion = RegionService.GetListRegionByParentId();
            ViewBag.ListCity = RegionService.GetListRegionByParentId(region);
            ViewBag.ListWard = RegionService.GetListRegionByParentId(city);
            return View("BuyNow", shipModel);            
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("BuyNow")]
        public ActionResult OnBuyNow(ShippingModelsAnonymous shipping)
        {
            if (ModelState.IsValid)
            {               
                var listRegion = RegionService.GetListIn(shipping.Region, shipping.City, shipping.Ward);                
                var shipAddress = shipping.CloneTo<ShippingAddress>();
                shipAddress.Region = listRegion.Count > 0 ? listRegion[0].Name : "";
                shipAddress.City = listRegion.Count > 1 ? listRegion[1].Name : "";
                shipAddress.Ward = listRegion.Count > 2 ? listRegion[2].Name : "";
                shipAddress.IpAddress = IpHelper.ClientIpAddress;
                var shippingId = ShippingService.Insert(shipAddress);              
                var newOrder = new ProductOrder
                {
                    FullName = shipping.FullName,                   
                    Address = $"{shipAddress.Address}, {shipAddress.Ward}, {shipAddress.City}, {shipAddress.Region}",
                    Email = shipping.Email,
                    Date = DateTime.Now,
                    Phone = shipping.Phone,
                    Count = MyCart.Items.Count,
                    Price = MyCart.SubTotalAfterService,
                    Status = (int)OrderStatusEnum.Pending,
                    ShippingId = shippingId
                };
                var tryUser = AccountService.FindEmail(shipping.Email);
                if (tryUser != null)
                {
                    newOrder.AccountID = tryUser.ID;
                }
                var orderId = CartService.InsertOrUpdate(newOrder);
                foreach (var product in MyCart.Items)
                {
                    CartItemService.Insert(
                    orderId, product.ProductItem.Id,
                    product.ProductItem.Name,
                    product.ProductItem.Avatar,
                    product.ProductItem.Price,
                    product.Quantity);
                }
                var transactionId = DateTime.Now.Ticks.ToString();
                //Add Transaction
                OrderTransactionService.Create(
                    orderId,
                    transactionId,
                    DateTime.Now,
                    (byte)TransactionEnum.Start);
                BeginTransaction(transactionId);
                return RedirectToAction("Index", "Payment");
            }
         
            ViewBag.Cart = MyCart;
            ViewBag.ListRegion = RegionService.GetListRegionByParentId();
            ViewBag.ListCity = RegionService.GetListRegionByParentId(shipping.Region);
            ViewBag.ListWard = RegionService.GetListRegionByParentId(shipping.City);
            return View("BuyNow", shipping);
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
