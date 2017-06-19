using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using ServiceStack.Text;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Sale.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            MarketRepository marketRepository = new MarketRepository(mEntities);
            IndexPageContainer mIndexPageContainer = new IndexPageContainer();
            List<IndexPage> mIndexPageList = new List<IndexPage>();
            StringBuilder str = new StringBuilder();
            List<Market> mMarketList = new List<Market>();
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult Category(int pGroupId = 0, int pPage = 1)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            MarketRepository marketRepository = new MarketRepository(mEntities);
            ProductCategoryPageContainer mProductCategoryPageContainer = new ProductCategoryPageContainer();
            List<ProductCategoryPage> mProductPageList = new List<ProductCategoryPage>();
            ProductType mProductType = new ProductType();
            List<ProductType> mProductTypeList;
            List<ProductType> mSoCheList;
            List<Product> mBestBuyList;
            List<Product> mProductList = new List<Product>();
            try
            {
                //lay chi tiet ProductType
                //lay danh sach các sieu thi co loai san pham nay
                //lay cac san pham cua nhom do
                mProductType = productRepository.LayLoaiSanPhamTheoId(pGroupId);
                if (mProductType != null)
                {
                    ViewBag.Id = mProductType.ID;
                    ViewBag.Name = mProductType.Name;
                    ViewBag.Level = mProductType.Level;
                    ViewBag.TypeBanner = mProductType.TypeBanner;
                    //lay danh sach sieu thi ban loai san pham nay
                    if (mProductType.Parent == 0)
                        mProductTypeList = productRepository.getProductTypeByProductType(mProductType.ID);//lay danh sach cap 1
                    else
                    {
                        mProductTypeList = new List<ProductType>();
                        mProductTypeList.Add(mProductType);
                        //mProductTypeList = productRepository.getProductTypeByProductType((int)mProductType.Parent);//lay danh sach cap 2
                    }
                    //lay các san pham cua nhom nay
                    if (mProductTypeList.Count > 0)
                    {
                        foreach (ProductType it in mProductTypeList)
                        {
                            //Chi tiết market
                            ProductCategoryPage mProductPage = new ProductCategoryPage();
                            mProductPage.Name = it.Name;
                            mProductPage.Id = (int)it.ID;
                            mProductPage.Image = it.Image;
                            if (mProductType.Parent == 0)
                            {
                                mProductList = productRepository.LayTheoTrangAndType(1, 4, it.ID, it.Level);
                            }
                            else
                            {
                                mProductList = productRepository.LayTheoTrangAndType(1, 40, it.ID, it.Level);
                            }
                            mProductPage.List = mProductList;
                            mProductPageList.Add(mProductPage);
                        }
                    }
                    else
                    {
                        ProductCategoryPage mProductPage = new ProductCategoryPage();
                        mProductPage.Name = mProductType.Name;
                        mProductPage.Id = (int)mProductType.ID;
                        mProductPage.Image = mProductType.Image;
                        mProductList = productRepository.LayTheoTrangAndType(1, 40, mProductType.ID, mProductType.Level);
                        mProductPage.List = mProductList;
                        mProductPageList.Add(mProductPage);
                    }
                    //lay danh sach cac nhom so che
                    mSoCheList = productRepository.LayProductTypeTheoParentId(147);
                    mProductCategoryPageContainer.ProductTypeList = mSoCheList;
                    //lay cac san pham ban chay
                    mBestBuyList = productRepository.getBestBuyWithType(1, 10, 147, "10030");
                    mProductCategoryPageContainer.ProductList = mBestBuyList;
                }
                mProductCategoryPageContainer.List = mProductPageList;
                mProductCategoryPageContainer.ProductType = mProductType;
                if (mProductList.Count < 40)
                    mProductCategoryPageContainer.IsEnd = true;
                mProductCategoryPageContainer.Page = pPage;
                //lay chi tiet san pham
                if (!Request.Browser.IsMobileDevice)
                    return View(mProductCategoryPageContainer);
                else
                    return View("~/Views/Home/MobileCategory.cshtml", mProductCategoryPageContainer);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult Detail(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            MarketRepository marketRepository = new MarketRepository(mEntities);
            ProductDetailPage mProductDetailPage = new ProductDetailPage();
            Product mProduct;
            ProductType mProductType = new ProductType();
            Market mMarket = new Market(); ;
            List<Product> MarketList;
            List<Product> RelatedList;
            List<Product> DiscountList;
            List<ProductType> mProductTypeList;
            try
            {
                mProduct = productRepository.LayTheoId(pId);
                if (mProduct != null)
                {
                    mProductDetailPage.Product = mProduct;
                    mMarket = marketRepository.LayTheoId((int)mProduct.MarketId);
                    if (mMarket == null)
                        mMarket = new Market();
                    mProductDetailPage.Market = mMarket;
                    //lay chi tiet ve loai san pham
                    mProductType = productRepository.LayLoaiSanPhamTheoId((int)mProduct.Type);
                    if (mProductType == null)
                        mProductType = new ProductType();
                    mProductDetailPage.ProductType = mProductType;
                    //lay danh sach san pham cua sieu thi
                    MarketList = productRepository.getByPageSizeMarketId(1, 6, mMarket.ID);
                    mProductDetailPage.MarketList = MarketList;
                    RelatedList = productRepository.LayDanhSachSanPhamLienQuan((int)mProduct.Type, 6);
                    mProductDetailPage.RelatedList = RelatedList;
                    DiscountList = productRepository.LaySanPhamKhuyenMai(1, 6);
                    mProductDetailPage.DiscountList = DiscountList;
                    //
                    mProductTypeList = productRepository.getProductTypeParent();
                    mProductDetailPage.ProductTypeList = mProductTypeList;
                }
                //lay chi tiet san pham
                if (!Request.Browser.IsMobileDevice)
                    return View(mProductDetailPage);
                else
                    return View("~/Views/Home/MobileDetail.cshtml", mProductDetailPage);

            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult Market(string pMarketName = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            MarketRepository marketRepository = new MarketRepository(mEntities);
            Market mMarket;
            ProductCategoryPageBox mProductCategoryPageBox = new ProductCategoryPageBox();
            ProductType mProductType = new ProductType();
            List<ProductType> mProductTypeList;
            try
            {
                //lay chi tiet ProductType
                //lay danh sach các nhóm sản phẩm con
                //lay cac san pham cua nhom
                mMarket = marketRepository.getByName(pMarketName.Replace('-', ' '));
                if (mMarket != null)
                {
                    mProductCategoryPageBox.List = new List<ProductCategoryPage>();
                    mProductCategoryPageBox.Market = mMarket;
                    mProductTypeList = productRepository.getProductTypeParent();
                    foreach (ProductType it in mProductTypeList)
                    {
                        ProductCategoryPage mProductPage = new ProductCategoryPage();
                        mProductPage.Name = it.Name;
                        mProductPage.Value = mMarket.UserName;
                        mProductPage.Id = (int)it.ID;
                        mProductPage.Image = it.Image;
                        List<Product> mProductList = productRepository.getByPageSizeMarketId(1, 4, (int)it.ID, mMarket.ID, it.Level);
                        mProductPage.List = mProductList;
                        mProductCategoryPageBox.List.Add(mProductPage);
                    }

                }
                return View(mProductCategoryPageBox);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult MarketCategory(string pMarketName = "", int pGroupId = 0, int pPage = 1)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            MarketRepository marketRepository = new MarketRepository(mEntities);
            Market mMarket;
            ProductCategoryPage mProductPage = new ProductCategoryPage();
            ProductType mProductType = new ProductType();
            try
            {
                //lay chi tiet ProductType
                //lay danh sach các nhóm sản phẩm con
                //lay cac san pham cua nhom
                mMarket = marketRepository.getByName(pMarketName.Replace('-', ' '));
                mProductType = productRepository.LayLoaiSanPhamTheoId(pGroupId);
                if (mMarket != null)
                {
                    mProductPage.Market = mMarket;
                    mProductPage.Name = mProductType.Name;
                    mProductPage.Id = mProductType.ID;
                    mProductPage.Image = mProductType.Image;
                    List<Product> mProductList = productRepository.getByPageSizeMarketId(1, 25, mProductType.ID, mMarket.ID, mProductType.Level);
                    mProductPage.List = mProductList;
                    if (mProductList.Count < 25)
                        mProductPage.IsEnd = true;
                    mProductPage.Page = pPage;
                }
                return View(mProductPage);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult ShopCartDetail(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository();
            AccountRepository accountRepository = new AccountRepository();
            ShopCartPage mShopCartPage = new ShopCartPage();
            Account mAccount = null;
            try
            {
                ShopCart mShopCart;
                if (Session["ShopCart"] != null)
                {
                    mShopCart = (ShopCart)Session["ShopCart"];
                    //
                    if (HttpContext.User.Identity.IsAuthenticated == true && Session["UserId"] != null)
                    {
                        mAccount = accountRepository.LayTinTheoId((int)Session["UserId"]);
                    }
                    if (mAccount == null)
                        mAccount = new Account();
                    mShopCart.Account = mAccount;
                    mShopCartPage.ShopCart = mShopCart;
                }
                return View(mShopCartPage);
            }
            catch (Exception ex)
            {
                return Content("<h2>Có lỗi xảy ra trên hệ thống ! Vui lòng thử lại sau.</h2>");
            }
            finally
            {
                accountRepository.Dispose();
                productRepository.Dispose();
                mEntities.Dispose();
            }
        }
        [HttpPost]
        public JsonResult addToShopCart(int pNumber = 1, int pId = 0, int pUnit = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            ProductRepository productRepository = new ProductRepository(mEntities);
            try
            {
                ShopCart mShopCart;
                Product mProduct;
                if (Session["ShopCart"] != null)
                    mShopCart = (ShopCart)Session["ShopCart"];
                else
                    mShopCart = new ShopCart();
                //thuc hien them muon do vao gio hang
                //lay chi tiet san pham
                mProduct = productRepository.LayTheoId(pId);
                if (mProduct != null)
                {
                    mProduct.Number = pNumber;
                    mProduct.Unit = pUnit;
                    mShopCart.List.Add(mProduct);
                    Session["ShopCart"] = mShopCart;
                    mProduct.Buy = (mProduct.Buy + 1);
                    return Json(new { code = 1, message = "Sản phẩm đã được thêm vào giỏ hàng thành công." });
                }
                else
                    return Json(new { code = 0, message = "Không tìm thấy sản phẩm." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult getShopCart()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            ProductRepository productRepository = new ProductRepository(mEntities);
            try
            {
                ShopCart mShopCart;
                if (Session["ShopCart"] != null)
                {
                    mShopCart = (ShopCart)Session["ShopCart"];
                    return Json(new { code = 1, count = 1, totalprice = String.Format("{0: 0,0}", (mShopCart.getTotalPrice())), message = "Không tìm thấy sản phẩm.", html = V308HTMLHELPER.createShopCart(mShopCart) });
                }
                else
                {
                    return Json(new { code = 0, count = 1, totalprice = 0, message = "Không tìm thấy sản phẩm." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, count = 1, totalprice = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult updateShopCart(int? pId, int pCount, string pVoucher, int pType = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            ProductRepository productRepository = new ProductRepository(mEntities);
            FileRepository fileRepository = new FileRepository(mEntities);
            File mFile;
            int mVoucher = 0;
            try
            {
                ShopCart mShopCart;
                if (Session["ShopCart"] != null)
                {
                    mShopCart = (ShopCart)Session["ShopCart"];
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
                        mFile = fileRepository.GetFileByTypeIdAndName(1, pVoucher, 1).FirstOrDefault();
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
                return Json(new { code = 0, count = 1, totalprice = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        [HttpPost]
        public JsonResult ThucHienThanhToan(string pFullName, string pEmail, string pMobile, string pAddress, string pAddressDelivery, string pCity, string pDistrict, string pTimeDelivery, string pDayDelivery)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            ShopCart mShopCart;
            List<Product> mList;
            ProductOrder mProductOrder = new ProductOrder();
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated == true && Session["UserId"] != null)
                {
                    if (Session["ShopCart"] != null)
                    {
                        mShopCart = (ShopCart)Session["ShopCart"];
                        mList = mShopCart.List;
                        mProductOrder.AccountID = (int)Session["UserId"];
                        mProductOrder.Address = pAddress + "____" + pAddressDelivery;
                        mProductOrder.Date = DateTime.Now;
                        mProductOrder.Detail = "Đơn mua hàng - Giao ngày:" + pDayDelivery + " - Giờ giao: " + pTimeDelivery;
                        mProductOrder.Email = pEmail;
                        mProductOrder.FullName = pFullName;
                        mProductOrder.Phone = pMobile;
                        mProductOrder.Status = 0;
                        //mProductOrder.ProductDetail = V308HTMLHELPER.TaoDanhSachSanPhamGioHang(mList);
                        
                        //mProductOrder.ProductDetail = JsonSerializer.SerializeToString<ShopCart>(mShopCart);

                        mProductOrder.Price = mShopCart.getTotalPrice();
                        mEntities.AddToProductOrder(mProductOrder);
                        mEntities.SaveChanges();
                        Session["ShopCart"] = new ShopCart();
                        //Gửi email báo cho người mua và quản trị về việc mua hàng
                        //gửi email cho khách
                        string EmailContent = "Thông tin đơn hàng của " + pFullName + " từ C-FOOD: <br/> " + V308HTMLHELPER.TaoDanhSachSanPhamGioHang(mList) + " <br/> Email: " + pEmail + "<br/> Mobile: " + pMobile + " <br/> Ngày : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "";
                        //V308Mail.SendMail(pEmail, "Thông tin đơn hàng của " + pFullName + " từ C-FOOD: " + " - " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "", EmailContent);
                        //gửi email tới admin
                        string EmailContent2 = "Thông tin đơn hàng của " + pFullName + " từ C-FOOD: <br/> " + V308HTMLHELPER.TaoDanhSachSanPhamGioHang(mList) + " <br/> Email: " + pEmail + "<br/> Mobile: " + pMobile + " <br/> Ngày : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "";
                        //V308Mail.SendMail("haviethoang611990@gmail.com", "Thông tin đơn hàng của " + pFullName + " từ C-FOOD: " + " - " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "", EmailContent2);

                        return Json(new { code = 1, message = "Hoàn thành mua bán. Chúng tôi sẽ liên lạc ngay với bạn qua số điện thoại bạn đã cung cấp." });
                    }
                    else
                    {
                        return Json(new { code = 3, message = "Giỏ hàng chưa có sản phẩm nào." });
                    }
                }
                else
                {
                    return Json(new { code = 2, message = "Vui lòng đăng nhập." });
                }

            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        public ActionResult Search(string pKey, int pVendor = 2, int pPage = 1)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            MarketRepository marketRepository = new MarketRepository(mEntities);
            SearchPage mSearchPage = new SearchPage();
            List<Product> mProductList = null;
            List<Market> mMarketList = null;
            try
            {
                //mList = newsRepository.LayDanhSachTinTheoKey(15, pKey, pPage);
                if (pVendor == 1)/*Tìm theo cửa hàng*/
                {
                    mMarketList = marketRepository.SearchMarketTheoTrangAndType(pPage, 30, pKey);

                    mSearchPage.Code = 1;
                    if (mMarketList.Count > 0)
                    {
                        mSearchPage.MarketList = mMarketList;
                        if (mMarketList.Count < 30)
                            mSearchPage.IsEnd = true;
                        mSearchPage.Page = pPage;
                        mSearchPage.Name = pKey;
                    }
                    else
                    {
                        mSearchPage.MarketList = new List<Data.Market>();
                        mSearchPage.Name = pKey;
                        mSearchPage.Html = "Không tìm thấy kết quả nào.";
                    }
                }
                else /*Tìm theo sản phẩm*/
                {
                    mProductList = productRepository.TimSanPhamTheoTen(pPage, 30, pKey.ToLower());
                    mSearchPage.Code = 2;
                    if (mProductList.Count > 0)
                    {
                        mSearchPage.ProductList = mProductList;
                        if (mProductList.Count < 30)
                            mSearchPage.IsEnd = true;
                        mSearchPage.Page = pPage;
                        mSearchPage.Name = pKey;
                    }
                    else
                    {
                        mSearchPage.ProductList = new List<Product>();
                        mSearchPage.Name = pKey;
                        mSearchPage.Html = "Không tìm thấy kết quả nào.";
                    }
                }

                return View(mSearchPage);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public JsonResult GuiThongTinLienHe(string pfirstname = "", string plastname = "", string pemail = "", string pphone = "", string pcontent = "")
        {
            try
            {
                //lay danh sach diem di :sales@dongxuanmedia.vn
                string EmailContent = "Thông tin liên hệ từ " + pfirstname + " " + plastname + " của Đồng Xuân Media: <br/> " + pcontent + " <br/> Email: " + pemail + "<br/> Mobile: " + pphone + " <br/> Ngày : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "";
                V308Mail.SendMail("sales@dongxuanmedia.vn", "Thông tin liên hệ từ " + pfirstname + " " + plastname + " của Đồng Xuân Media: " + " - " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "", EmailContent);
                return Json(new { code = 1, message = "Yêu cầu được gửi thành công ! Chúng tôi sẽ sớm liên lạc lại với bạn." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra, Vui lòng gửi lại hoặc liên lạc trực tiếp với chúng tôi qua điện thoại." });
            }
        }
        public ActionResult Add()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            ProductRepository productRepository = new ProductRepository(mEntities);
            MarketRepository marketRepository = new MarketRepository(mEntities);
            StringBuilder str = new StringBuilder();
            List<Market> mMarketList = new List<Market>();
            List<ProductType> mProductTypeList = new List<ProductType>();
            List<MarketProductType> mMarketProductTypeList;
            try
            {
                //lay danh sach cac cua hang
                //lay danh sach cac nhom hang cua cua hang ban
                //them cac san pham vao theo cac nhom cac cua hang
                mMarketList = marketRepository.getAll(18);
                foreach (Market it in mMarketList)
                {
                    //lay danh sach cac nhom san pham
                    mMarketProductTypeList = marketRepository.getAllMarketProductType(it.ID);
                    //lap khap cac kieu nay
                    foreach (MarketProductType ut in mMarketProductTypeList)
                    {
                        //lay danh sach cac nhom con
                        mProductTypeList = productRepository.getProductTypeByParent((int)ut.Parent);
                        //them cac san pham
                        foreach (ProductType ht in mProductTypeList)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                Product mProduct = new Product() { Name = "Sản phẩm " + it.ID + "_" + ht.ID, Type = ht.ID, MarketId = it.ID, Image = "/Content/Images/pimg.png", Price = 1000, Summary = "Rau muống " + it.ID, Status = true, Date = DateTime.Now };
                                mEntities.AddToProduct(mProduct);
                            }
                        }
                    }
                    mEntities.SaveChanges();
                }
                //
                return Content("OK"); ;
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                mEntities.Dispose();
                productRepository.Dispose();
            }
        }
        public ActionResult News(int pPage = 1, int pType = 1)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsPage mCommonModel = new NewsPage();
            StringBuilder mStr = new StringBuilder();
            List<News> mList;
            NewsGroups mNewsGroups;
            try
            {
                //lay chi tiet loai tin tuc
                mNewsGroups = newsRepository.LayTheLoaiTinTheoId(pType);
                if (mNewsGroups != null)
                {
                    mCommonModel.NewsGroups = mNewsGroups;
                    //lay chi tiet san pham
                    mList = newsRepository.LayTinTheoTrangAndGroupIdAndLevel(pPage, 10, pType, mNewsGroups.Level);
                    if (mList.Count > 0)
                    {
                        foreach (News it in mList)
                        {
                            if (mNewsGroups.Level.Substring(0, 5) == "10006")
                            {
                                mStr.Append("<div class=\"news\">");
                                mStr.Append("<h2 class=\"title\"><a href=\"/" + Ultility.LocDau(it.Title) + "-youtube" + it.ID + ".html\">" + it.Title + "</a></h2>");
                                mStr.Append("<div class=\"image_container\">");
                                mStr.Append("<div class=\"image_cell\">");
                                mStr.Append("<a href=\"/" + Ultility.LocDau(it.Title) + "-youtube" + it.ID + ".html\">");
                                mStr.Append("<img class=\"image_news\" src=\"https://i.ytimg.com/vi/" + it.Summary + "/hqdefault.jpg?custom=true&w=250&h=141&stc=true&jpg444=true&jpgq=90&sp=68\" alt=\"" + it.Title + "\">");
                                mStr.Append("</a>");
                                mStr.Append("</div>");
                                mStr.Append("</div>");
                                mStr.Append("<div class=\"create_time\"><span class=\"crateTimeTitle\">Thời gian đăng :</span> " + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</div>");
                                mStr.Append("<p class=\"description\">" + it.Title + "</p>");
                                mStr.Append("</div>");
                            }
                            else
                            {
                                mStr.Append("<div class=\"news\">");
                                mStr.Append("<h2 class=\"title\"><a href=\"/" + Ultility.LocDau(it.Title) + "-n" + it.ID + ".html\">" + it.Title + "</a></h2>");
                                mStr.Append("<div class=\"image_container\">");
                                mStr.Append("<div class=\"image_cell\">");
                                mStr.Append("<a href=\"/" + Ultility.LocDau(it.Title) + "-n" + it.ID + ".html\">");
                                mStr.Append("<img class=\"image_news\" src=\"" + it.Image + "\" alt=\"" + it.Title + "\">");
                                mStr.Append("</a>");
                                mStr.Append("</div>");
                                mStr.Append("</div>");
                                mStr.Append("<div class=\"create_time\"><span class=\"crateTimeTitle\">Thời gian đăng :</span> " + ConverterUlti.GetNgayDangByDateTime(it.Date.Value) + "</div>");
                                mStr.Append("<p class=\"description\">" + it.Summary + "</p>");
                                mStr.Append("</div>");
                            }

                        }
                        if (mList.Count < 10)
                            mCommonModel.IsEnd = true;
                        mCommonModel.Page = pPage;
                        mCommonModel.Html = mStr.ToString();
                    }
                    else
                    {
                        mCommonModel.Html = "Không tìm thấy sản phẩm";
                    }
                }
                return View(mCommonModel);
            }
            catch (Exception ex)
            {
                return Content("<h2>Có lỗi xảy ra trên hệ thống ! Vui lòng thử lại sau.</h2>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult NewsDetail(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsPage mCommonModel = new NewsPage();
            StringBuilder mStr = new StringBuilder();
            News mNews;
            try
            {
                //lay chi tiet san pham
                mNews = newsRepository.LayTinTheoId(pId);
                if (mNews != null)
                {

                    mCommonModel.pNews = mNews;
                    //mListLienQuan = newsRepository.LayTinTucLienQuan(mNews.ID, (int)mNews.TypeID, 4);
                    //tao Html tin tuc lien quan
                }
                else
                {
                    mCommonModel.Html = "Không tìm thấy sản phẩm";
                }
                return View(mCommonModel);
            }
            catch (Exception ex)
            {
                return Content("<h2>Có lỗi xảy ra trên hệ thống ! Vui lòng thử lại sau.</h2>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult YoutubeDetail(int pId = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            NewsRepository newsRepository = new NewsRepository(mEntities);
            NewsPage mCommonModel = new NewsPage();
            StringBuilder mStr = new StringBuilder();
            News mNews;
            List<News> mListLienQuan;
            try
            {
                //lay chi tiet san pham
                mNews = newsRepository.LayTinTheoId(pId);
                if (mNews != null)
                {

                    mCommonModel.pNews = mNews;
                    mListLienQuan = newsRepository.LayTinTucLienQuan(mNews.ID, 26, 5);
                    //tao Html tin tuc lien quan
                    mCommonModel.List = mListLienQuan;
                }
                else
                {
                    mCommonModel.Html = "Không tìm thấy sản phẩm";
                }
                return View(mCommonModel);
            }
            catch (Exception ex)
            {
                return Content("<h2>Có lỗi xảy ra trên hệ thống ! Vui lòng thử lại sau.</h2>");
            }
            finally
            {
                mEntities.Dispose();
                newsRepository.Dispose();
            }
        }
        public ActionResult MarketList(int ptype = 0)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            List<Market> mList = new List<Market>();
            try
            {
                //lay danh sach nhom tin
                mList = marketRepository.getMarketByType(100, ptype);
                ViewBag.MkType = ptype;
                //lay danh sach cac tin theo nhom
                return View(mList);
            }
            catch (Exception ex)
            {
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }
        }
        [HttpPost]
        public JsonResult addEmail(string pEmail)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            EmailRepository emailRepository = new EmailRepository(mEntities);
            try
            {

                if (ValidateInput.IsValidEmailAddress(pEmail))
                {
                    VEmail mVEmail = new VEmail() { CreatedDate = DateTime.Now, State = true, Type = 1, Value = pEmail };
                    mEntities.AddToVEmail(mVEmail);
                    mEntities.SaveChanges();
                    return Json(new { code = 1, message = "Email đã được thêm vào hệ thống." });
                }
                else
                {
                    return Json(new { code = 0, message = "Email bạn nhập không chính xác." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                emailRepository.Dispose();
            }

        }
        public ActionResult MarketRegister()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckMarketRegister(string pUserName, string pPassWord, string pPassWord2, string pEmail, string pMobile, int pMarketType, string pSummary, string pImage, string pSumary, string InvisibleCaptchaValue, string Captcha = "", bool rbtAgree = false, string pFullName = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            ProductRepository productRepository = new ProductRepository(mEntities);
            AccountRepository accountRepository = new AccountRepository(mEntities);
            List<ProductType> mList;
            Market mMarket;
            Admin mAdmin;
            try
            {
                if (pPassWord.Trim().Equals(pPassWord2.Trim()))
                {
                    if (pEmail.Length > 5 && pPassWord.Length > 5)
                    {
                        mAdmin = accountRepository.LayAdminTheoUserName(pEmail);
                        if (!(mAdmin != null))
                        {
                            mAdmin = new Admin()
                            {
                                Date = DateTime.Now,
                                Role = 2,
                                FullName = pFullName,
                                Email = pEmail,
                                UserName = pEmail,
                                Password = EncryptionMD5.ToMd5(pPassWord.Trim()),
                                //PSanPham = (true),
                                //PFileUpload = (true),
                                //PHeThong = (true),
                                //PHinhAnh = (true),
                                //PKhachHang = (true),
                                //PTaiKhoan = (true),
                                //PThungRac = (true),
                                //PTinTuc = (true),
                                Status=false
                            };
                            mEntities.AddToAdmin(mAdmin);
                            mEntities.SaveChanges();
                            //return Json(new { code = 1, message = "Lưu  tài khoản thành công." });
                        }
                        else
                        {
                            return Json(new { code = 0, message = "Tài khoản đã tồn tại. Vui lòng tại tài khoản mới." });
                        }
                    }
                    else
                    {
                        return Json(new { code = 0, message = "Mật khẩu và tài khoản và có độ dài tối thiểu 6 kí tự." });
                    }
                }
                else
                {
                    return Json(new { code = 0, message = "Mật khẩu xác nhận không trùng khớp." });
                }
                ////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////TẠO GIAN HÀNG
                ///////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////
                pImage = "http://cleanfoodvn.vn/Content/Images/logo.png";
                mMarket = new Market() { Date = DateTime.Now,UserId=mAdmin.ID, BirthDay = DateTime.Now, UserName = Ultility.LocDau2(pUserName.Trim()), Avata = pImage, Email = pEmail, FullName = pFullName, Gender = true, Phone = pMobile, Role = pMarketType, Status = false, Sumary = pSumary };
                mEntities.AddToMarket(mMarket);
                mEntities.SaveChanges();
                //lay danh sách nhom san pham
                mList = productRepository.getProductTypeParent();
                foreach (ProductType it in mList)
                {
                    MarketProductType mMarketProductType = new MarketProductType() { Date = DateTime.Now, Name = it.Name, Detail = it.Name, Parent = it.ID, Status = true, Visible = true, Number = 1, MarketId = mMarket.ID, MarketName = mMarket.UserName, ImageBanner = it.ImageBanner };
                    mEntities.AddToMarketProductType(mMarketProductType);
                }
                mEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu cửa hàng thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }
        }
    }

}
