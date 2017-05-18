using System;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    public class CartController : BaseController
    {
        //
        // GET: /ShoppingCart/

        [HttpPost]
        public JsonResult Add(int id = 0,int quantity = 1)
        {                   
            try
            {
                var product = ProductsService.LayTheoId(id);
                if (product != null)
                {        
                    var shoppingCart = ShoppingCart.Instance;
                    shoppingCart.AddItem(product);
                    return Json(new
                    {
                        code = 1,
                        totalprice = $"{shoppingCart.SubTotal: 0,0}",
                        message = "Sản phẩm đã được thêm vào giỏ hàng thành công."
                    });

                }
                else
                    return Json(new { code = 0, message = "Không tìm thấy sản phẩm." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
         
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Index()
        {
        
            try
            {
                if (Session["ShopCart"] != null)
                {
                    var mShopCart = (ShopCart)Session["ShopCart"];
                    return Json(new { code = 1, count = 1, totalprice = String.Format("{0: 0,0}", mShopCart.getTotalPrice()), message = "Không tìm thấy sản phẩm.", html = V308HTMLHELPER.createShopCart(mShopCart) });
                }
                else
                {
                    return Json(new { code = 0, count = 1, totalprice = 0, message = "Không tìm thấy sản phẩm." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return Json(new { code = 0, count = 1, totalprice = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            

        }
        public ActionResult ShopCartDetail(int pId = 0)
        {           
            ShopCartPage mShopCartPage = new ShopCartPage();
            Account mAccount = null;
            try
            {
                if (Session["ShopCart"] != null)
                {
                    var mShopCart = (ShopCart)Session["ShopCart"];
                    //
                    if (HttpContext.User.Identity.IsAuthenticated == true && Session["UserId"] != null)
                    {
                        mAccount = AccountService.LayTinTheoId((int)Session["UserId"]);
                    }
                    if (mAccount == null)
                        mAccount = new Account();
                    mShopCart.Account = mAccount;
                    mShopCartPage.ShopCart = mShopCart;
                }
                return View(FindView("ShopCardDetail"), mShopCartPage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return Content("<h2>Có lỗi xảy ra trên hệ thống ! Vui lòng thử lại sau.</h2>");
            }
          
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateCart(int? pId, int pCount, string pVoucher, int pType = 0)
        {
            try
            {
                if (Session["ShopCart"] != null)
                {
                    var mShopCart = (ShopCart)Session["ShopCart"];
                    if (pType == 0)
                    {
                        foreach (Product it in mShopCart.List)
                        {
                            if (it.ID == pId)
                            {
                                it.Number = pCount;
                                break;
                            }

                        }
                    }
                    else if (pType == 1)
                    {
                        //pVoucher
                        mShopCart.VoucherName = pVoucher;
                        var mFile = FileService.GetFileByTypeIdAndName(1, pVoucher, 1).FirstOrDefault();
                        var mVoucher = 0;
                        if (mFile != null)
                            mVoucher = (int)mFile.Value;
                        else
                            mVoucher = 0;
                        mShopCart.Voucher = mVoucher;
                    }
                    else if (pType == 2)
                    {
                        foreach (Product it in mShopCart.List)
                        {
                            if (it.ID == pId)
                            {
                                mShopCart.List.Remove(it);
                                break;
                            }
                        }
                    }
                    Session["ShopCart"] = mShopCart;
                    return Json(new { code = 1, message = "Không tìm thấy sản phẩm." });
                }
                else
                {
                    return Json(new { code = 0, count = 1, totalprice = 0, message = "Không tìm thấy sản phẩm." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return Json(new { code = 0, count = 1, totalprice = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
        }

    }
}
