using System;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Enum;
using V308CMS.Data.Models;
using V308CMS.Helpers;
using V308CMS.Helpers.Url;
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
            var product =  ProductsService.Find(id);
            if (product != null)
            {
                if (product.Quantity < quantity)
                {
                    return Json(new { code = 0,  message = $"Chỉ còn {product.Quantity} sản phẩm {product.Name} trong kho."
                    });
                }
                MyCart.AddItem(new ProductModels
                {
                    Id = product.ID,
                    Avatar = product.Image.ToUrl(95, 100),
                    Name = product.Name,
                    SaleOff = product.SaleOff ?? 0,
                    Price = product.Price ?? 0
                });
                return Json(new
                {
                    code = 1,
                    totalprice = $"{MyCart.SubTotal: 0,0}",
                    message = "Sản phẩm đã được thêm vào giỏ hàng thành công."
                });

            }
            return Json(new { code = 0, message = "Không tìm thấy sản phẩm." });
        }
        [HttpGet,ActionName("remove")]
        public ActionResult OnRemoveItem(int id)
        {
            var product =  ProductsService.Find(id);
            if (product != null)
            {
                RemoveItemInCart(id);               
            }
            return RedirectToAction("ViewCart");

        }
        [HttpPost]
        public JsonResult RemoveItem(int id, int quantity =0)
        {
            var product =  ProductsService.Find(id);
            if (product != null)
            {
                RemoveItemInCart(id);              
                return Json(new
                {
                    code = 1,
                    totalprice = $"{ MyCart.SubTotal: 0,0}",
                    message = $"Sản phẩm {product.Name} đã được xóa khỏi giỏ hàng thành công."
                });

            }
            return Json(new { code = 0, message = "Không tìm thấy sản phẩm." });
        }
      
        [ValidateInput(false)]
        public JsonResult Index()
        {
        
            return Json(new
            {
                code = 1,
                item_count = MyCart.Items.Count,
                items = MyCart.Items.Select(product => new
                {
                    id = product.ProductItem.Id,
                    url = url.productURL(product.ProductItem.Name, product.ProductItem.Id),
                    title = product.ProductItem.Name,
                    quantity = product.Quantity,
                    image = product.ProductItem.Avatar,
                    price = product.ProductItem.Price.ToPriceString()
                }),
                total_price = MyCart.SubTotal.ToPriceString()

            }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ViewCart()
        {
            return View("Cart.View", MyCart);
        }

        public ActionResult EmptyCart()
        {
           return View("Cart.Empty");
        }

        public ActionResult Checkout()
        {
            var model = new ShippingModels();
            if (IsEmptyCart())
            {
                return RedirectToAction("EmptyCart");
            }                    
            if (User != null)
            {
                var listShipAddress = ShippingService.GetListAddressByUserId(User.UserId);
                if (listShipAddress.Count > 0)
                {
                    var lastestShippingAddress = listShipAddress[0];
                    var listRegion = RegionService.GetListInByName(
                        lastestShippingAddress.Region,
                        lastestShippingAddress.City,
                        lastestShippingAddress.Ward);
                    if (listRegion != null && listRegion.Count > 0)
                    {
                        model.Region = listRegion[0].Id;                      
                        if (listRegion.Count > 1)
                        {
                            model.City = listRegion[1].Id;
                             
                        }
                        if (listRegion.Count > 2)
                        {
                            model.Ward = listRegion[2].Id;
                        }
                    }
                    model.FullName = lastestShippingAddress.FullName;
                    model.Phone = lastestShippingAddress.Phone;
                    model.Address = lastestShippingAddress.Address;
                }
                ViewBag.ListShippingAddress = listShipAddress;

            }       
            if (!string.IsNullOrEmpty(TransactionId))
            {
                var transactionInfo = OrderTransactionService.GetByTransactionId(TransactionId);
                ViewBag.Order = transactionInfo.Order;
            }           
            ViewBag.ListRegion = RegionService.GetListRegionByParentId();
            ViewBag.ListCity = RegionService.GetListRegionByParentId(model.Region);
            ViewBag.ListWard = RegionService.GetListRegionByParentId(model.City);
            ViewBag.Cart = MyCart;
            ViewBag.TransactionId = TransactionId;          
            return View("Cart.Checkout", model);
        }


        [HttpPost]
        public  ActionResult UpdateCart(int id=0, int quantity=0)
        {

            var product =  ProductsService.Find(id);
            if (product != null)
            {
                if (product.Quantity == 0)
                {
                    return Json(new { code = 0, message = "Sản phẩm hiện đã hết hàng." });
                }
                if (product.Quantity < quantity)
                {
                    return Json(new { code = 0, message = $"Chỉ còn {product.Quantity} sản phẩm."});
                }

                MyCart.SetItemQuantity(new ProductModels
                {
                    Id = product.ID
                }, quantity);
                return Json(new
                {
                    code = 1,
                    message = $"Cập nhật số lượng sản phẩm {product.Name} thành công."
                });

            }
            return Json(new { code = 0, message = "Không tìm thấy sản phẩm." });
        }
        [Authorize]
        [HttpPost]
        public ActionResult SendOrder(ShippingModels shippingModel)
        {
            
            var region = Request.Form["Region"];
            var city = Request.Form["City"];
            var ward = Request.Form["Ward"];
            var address = Request.Form["Address"];
            var fullName = Request.Form["FullName"];
            var phone  = Request.Form["Phone"];
           
            var shipping = new ShippingModels
            {
                FullName = fullName,
                Phone = phone
            };


            int regionValue, cityValue, wardValue;
            int.TryParse(region, out regionValue);
            int.TryParse(city, out cityValue);
            int.TryParse(ward, out wardValue);
            shipping.Region = regionValue;
            shipping.City = cityValue;
            shipping.Ward = wardValue;
            shipping.Address = address;          

            if (ModelState.IsValid)
            {
                //Add shipping service
                var listRegion = RegionService.GetListIn(shipping.Region, shipping.City, shipping.Ward);
                shipping.UserId = User.UserId;
                var shipAddress = shipping.CloneTo<ShippingAddress>();
                shipAddress.Region = listRegion.Count > 0 ? listRegion[0].Name : "";
                shipAddress.City = listRegion.Count > 1 ? listRegion[1].Name : "";
                shipAddress.Ward = listRegion.Count > 2 ? listRegion[2].Name : "";
                shipAddress.IpAddress = IpHelper.ClientIpAddress;
                var shippingId = ShippingService.InsertOrUpdate(shipAddress);
                            
                var newOrder = new ProductOrder
                {
                    FullName = shipping.FullName,                
                    Address = $"{shipAddress.Address}, {shipAddress.Ward}, {shipAddress.City}, {shipAddress.Region}",                   
                    AccountID = User.UserId,
                    Date = DateTime.Now,
                    Phone = shipping.Phone,
                    Count = MyCart.Items.Count,
                    Price = MyCart.SubTotalAfterService,
                    Status = (int)OrderStatusEnum.Pending,
                    ShippingId = shippingId,
                  
                };
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
                    (byte) TransactionEnum.Start);
                BeginTransaction(transactionId);                         
                return RedirectToAction("Index", "Payment");
            }
            if (User != null)
            {
                ViewBag.ListShippingAddress = ShippingService.GetListAddressByUserId(User.UserId);
            }
            ViewBag.ListRegion = RegionService.GetListRegionByParentId();
            ViewBag.ListCity = RegionService.GetListRegionByParentId(regionValue);
            ViewBag.ListWard = RegionService.GetListRegionByParentId(cityValue);
            ViewBag.Cart = MyCart;
            return View("Cart.Checkout", shipping);

        }
    }
}

