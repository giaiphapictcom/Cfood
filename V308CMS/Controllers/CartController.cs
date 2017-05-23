using System;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Helpers;
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
            var product = ProductsService.LayTheoId(id);
            if (product != null)
            {
                var shoppingCart = ShoppingCart.Instance;
                shoppingCart.AddItem(new ProductModels
                {
                    Id = product.ID,
                    Avatar = product.Image.ToUrl(95, 100),
                    Name = product.Name,
                    SaleOff = product.SaleOff.HasValue ? product.SaleOff.Value : 0,
                    Price = product.Price.HasValue ? product.Price.Value : 0
                });
                return Json(new
                {
                    code = 1,
                    totalprice = String.Format("{0: 0,0}", shoppingCart.SubTotal),
                    message = "Sản phẩm đã được thêm vào giỏ hàng thành công."
                });

            }
            return Json(new { code = 0, message = "Không tìm thấy sản phẩm." });
        }
        [HttpGet,ActionName("remove")]
        public ActionResult HandleRemoveItem(int id)
        {
            var product = ProductsService.LayTheoId(id);
            if (product != null)
            {
                ShoppingCart.Instance.RemoveItem(new ProductModels
                {
                    Id = product.ID
                });
            }
            return RedirectToAction("ViewCart");

        }
        [HttpPost]
        public JsonResult RemoveItem(int id, int quantity =0)
        {
            var product = ProductsService.LayTheoId(id);
            if (product != null)
            {
                var shoppingCart = ShoppingCart.Instance;
                shoppingCart.RemoveItem(new ProductModels
                {
                    Id = product.ID
                });
                return Json(new
                {
                    code = 1,
                    totalprice = String.Format("{0: 0,0}", shoppingCart.SubTotal),
                    message = string.Format("Sản phẩm {0} đã được xóa khỏi giỏ hàng thành công.", product.Name)
                });

            }
            return Json(new { code = 0, message = "Không tìm thấy sản phẩm." });
        }
      
        [ValidateInput(false)]
        public JsonResult Index()
        {

            var shoppingCart = ShoppingCart.Instance;
            return Json(new
            {
                code = 1,
                item_count = shoppingCart.Items.Count,
                items = shoppingCart.Items.Select(product => new
                {
                    id = product.ProductItem.Id,
                    url = url.productURL(product.ProductItem.Name, product.ProductItem.Id),
                    title = product.ProductItem.Name,
                    quantity = product.Quantity,
                    image = product.ProductItem.Avatar,
                    price = product.ProductItem.Price.ToString("N0")
                }),
                total_price = shoppingCart.SubTotal.ToString("N0")

            }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ViewCart()
        {
            return View("Cart.View", ShoppingCart.Instance);
        }

        public ActionResult Checkout()
        {
            return View("Cart.Checkout");
        }
        [HttpPost]
        public ActionResult UpdateCart(int id, int quantity)
        {

            var product = ProductsService.LayTheoId(id);
            if (product != null)
            {
                if (product.Number == 0)
                {
                    return Json(new { code = 0, message = "Sản phẩm hiện đã hết hàng." });
                }
                if (product.Number < quantity)
                {
                    return Json(new { code = 0, message = string.Format("Chỉ còn {0} sản phẩm.", product.Number) });
                }

                var shoppingCart = ShoppingCart.Instance;
                shoppingCart.SetItemQuantity(new ProductModels
                {
                    Id = product.ID
                }, quantity);
                return Json(new
                {
                    code = 1,
                    message = string.Format("Cập nhật số lượng sản phẩm {0} thành công.", product.Name)
                });

            }
            return Json(new { code = 0, message = "Không tìm thấy sản phẩm." });
        }
        
       
    }
}
