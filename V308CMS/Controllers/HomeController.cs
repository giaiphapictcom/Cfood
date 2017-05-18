using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ServiceStack.Text;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Controllers
{
    public class HomeController : Controller
    {
        #region Repository
        static V308CMSEntities mEntities;
        ProductRepository productRepos;
        SiteRepository siteRepo;
        MarketRepository marketRepo;
        AccountRepository accountRepos;
        MarketRepository marketRepos;
        NewsRepository newsRepos;
        private void CreateRepos()
        {
            mEntities = new V308CMSEntities();
            productRepos = new ProductRepository(mEntities);
            siteRepo = new SiteRepository(mEntities);
            marketRepo = new MarketRepository(mEntities);
            accountRepos = new AccountRepository(mEntities);
            marketRepos = new MarketRepository(mEntities);
            newsRepos = new NewsRepository(mEntities);
        }
        private void DisposeRepos()
        {
            mEntities.Dispose();
            productRepos.Dispose();
            siteRepo.Dispose();
            marketRepo.Dispose();
            accountRepos.Dispose();
            marketRepos.Dispose();
            newsRepos.Dispose();
        }
        #endregion
        

        public ActionResult Index()
        {
            CreateRepos();
            IndexPageContainer mIndexPageContainer = new IndexPageContainer();
            List<IndexPage> mIndexPageList = new List<IndexPage>();
            StringBuilder str = new StringBuilder();
            List<Market> mMarketList = new List<Market>();
            List<Product> mBestBuyList;
            List<ProductType> mTypeList;
            //List<ProductType> mSoCheList;
            //List<Product> mBestSoCheList;
            List<Product> mList;
            List<ProductType> mListParent;
            try
            {
                mListParent = productRepos.LayProductTypeTheoParentId(0);
                foreach (ProductType it in mListParent)
                {
                    //lay danh sach san pham
                    if (!Request.Browser.IsMobileDevice)
                        mList = productRepos.LayTheoTrangAndType(1, 4, it.ID, it.Level);
                    else
                        mList = productRepos.LayTheoTrangAndType(1, 50, it.ID, it.Level);
                    //lay danh sach nhom san pham con
                    mTypeList = productRepos.getProductTypeByParent(it.ID);
                    IndexPage mIndexPage = new IndexPage();
                    mIndexPage.Id = it.ID;
                    mIndexPage.Name = it.Name;
                    mIndexPage.Image = it.Image;
                    mIndexPage.ImageBanner = "/Content/Images/stepbuy.png"; //it.ImageBanner;
                    mIndexPage.ProductTypeList = mTypeList;
                    mIndexPage.ProductList = mList;
                    mIndexPageList.Add(mIndexPage);
                }
                mIndexPageContainer.IndexPageList = mIndexPageList;
                //lay cac san pham ban chay
                if (!Request.Browser.IsMobileDevice)
                    mBestBuyList = productRepos.LaySanPhamBanChay(1, 10);
                else
                    mBestBuyList = productRepos.LaySanPhamBanChay(1, 50);

                if (mBestBuyList.Count() < 1) {
                    mBestBuyList = productRepos.getProductsRandom(18);
                }
                mIndexPageContainer.BestBuyList = mBestBuyList;



                //lay danh sach cac nhom cua SO CHE
                //mSoCheList = productRepos.LayProductTypeTheoParentId(147);
                //lay cac san pham so che
                //if (!Request.Browser.IsMobileDevice)
                //    mBestSoCheList = productRepos.LayTheoTrangAndType(1, 9, 147, "10030");
                //else
                //    mBestSoCheList = productRepos.LayTheoTrangAndType(1, 50, 147, "10030");
                //mIndexPageContainer.BestSoCheList = mBestSoCheList;
                //mIndexPageContainer.ProductTypeList = mSoCheList;

                mIndexPageContainer.ProductLastest = productRepos.getProductsLastest(18);
                if (mIndexPageContainer.ProductLastest.Count() < 1)
                {
                    mIndexPageContainer.ProductLastest = productRepos.getProductsRandom(18);
                }

                List<ProductType> HomeCategorys = new List<ProductType>();
                
                HomeCategorys.Add(productRepos.LayLoaiSanPhamTheoId(177));
                HomeCategorys.Add(productRepos.LayLoaiSanPhamTheoId(176));
                HomeCategorys.Add(productRepos.LayLoaiSanPhamTheoId(179));
                HomeCategorys.Add(productRepos.LayLoaiSanPhamTheoId(180));

                HomeCategorys.Add(productRepos.LayLoaiSanPhamTheoId(183));
                HomeCategorys.Add(productRepos.LayLoaiSanPhamTheoId(175));
                
                mIndexPageContainer.ProductTypeList = HomeCategorys;
                //mIndexPageContainer.ProductTypeList = productRepos.LayProductTypeTheoParentId(0);

                string view = Theme.viewPage("home");
                if (view.Length > 0) {
                    return View(view, mIndexPageContainer);
                }

                if (!Request.Browser.IsMobileDevice)
                    return View(mIndexPageContainer);
                else
                    return View("~/Views/Home/MobileIndex.cshtml", mIndexPageContainer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }
        public ActionResult Category(int pGroupId = 0)
        {
            CreateRepos();

            ProductCategoryPageContainer Model = new ProductCategoryPageContainer();

            int nPage = Convert.ToInt32(Request.QueryString["p"]);
            if (nPage < 1)
            {
                nPage = 1;
            }

            List<Product> mProductList = new List<Product>();
            try
            {
                List<ProductCategoryPage> mProductPageList = new List<ProductCategoryPage>();
                List<ProductType> mProductTypeList;

                ProductType ProductCategory = productRepos.LayLoaiSanPhamTheoId(pGroupId);
                if (ProductCategory != null)
                {
                    CategoryPage CategoryPage = ProductHelper.getProductsByCategory(ProductCategory.ID, nPage);

                    Model.Products = CategoryPage.Products;
                    Model.ProductTotal = CategoryPage.ProductTotal;
                    Model.Brands = productRepos.getRandomBrands(ProductCategory.ID, 6);
                    if (ProductCategory.Parent == 0)
                        mProductTypeList = productRepos.getProductTypeByProductType(ProductCategory.ID);//lay danh sach cap 1
                    else
                    {
                        mProductTypeList = new List<ProductType>();
                        mProductTypeList.Add(ProductCategory);
                        //mProductTypeList = productRepository.getProductTypeByProductType((int)mProductType.Parent);//lay danh sach cap 2
                    }
                    
                    if (mProductTypeList.Count > 0)
                    {
                        foreach (ProductType it in mProductTypeList)
                        {
                            mProductPageList.Add(ProductHelper.GetCategoryPage(it, nPage));
                        }
                    }
                    else
                    {
                        mProductPageList.Add(ProductHelper.GetCategoryPage(ProductCategory, nPage));

                    }
                    //lay danh sach cac nhom so che
                    //mSoCheList = productRepos.LayProductTypeTheoParentId(147);
                    //Model.ProductTypeList = mSoCheList;
                    //lay cac san pham ban chay
                    //mBestBuyList = productRepos.getBestBuyWithType(1, 10, 147, "10030");
                    //Model.ProductList = mBestBuyList;
                }
                Model.List = mProductPageList;
                Model.ProductType = ProductCategory;
                Model.BestSeller = productRepos.getProductsRandom();
                //if (mProductList.Count < 40)
                //    Model.IsEnd = true;
                Model.Page = nPage;
                

                string view = Theme.viewPage("category");
                if (view.Length > 0)
                {
                    return View(view, Model);
                }

                if (!Request.Browser.IsMobileDevice)
                    return View(Model);
                else
                    return View("~/Views/Home/MobileCategory.cshtml", Model);
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }
        public ActionResult Detail(int pId = 0)
        {
            CreateRepos();
            ProductDetailPage mProductDetailPage = new ProductDetailPage();
            Product mProduct;
            ProductType mProductType = new ProductType();
            Market mMarket = new Market(); ;
            //List<Product> MarketList;
            List<Product> RelatedList;
            //List<Product> DiscountList;
            List<ProductType> mProductTypeList;
            try
            {
                mProduct = productRepos.LayTheoId(pId);
                if (mProduct != null)
                {
                    mProductDetailPage.Product = mProduct;
                    mMarket = marketRepos.LayTheoId((int)mProduct.MarketId);
                    if (mMarket == null)
                        mMarket = new Market();
                    mProductDetailPage.Market = mMarket;
                    //lay chi tiet ve loai san pham
                    mProductType = productRepos.LayLoaiSanPhamTheoId((int)mProduct.Type);
                    if (mProductType == null)
                        mProductType = new ProductType();
                    mProductDetailPage.ProductType = mProductType;
                    //lay danh sach san pham cua sieu thi
                    //MarketList = productRepos.getByPageSizeMarketId(1, 6, mMarket.ID);
                    //mProductDetailPage.MarketList = MarketList;

                    RelatedList = productRepos.LayDanhSachSanPhamLienQuan((int)mProduct.Type, 12,true);
                    mProductDetailPage.RelatedList = RelatedList;

                    //DiscountList = productRepos.LaySanPhamKhuyenMai(1, 6);
                    //mProductDetailPage.DiscountList = DiscountList;
                    //
                    mProductTypeList = productRepos.getProductTypeParent();
                    mProductDetailPage.ProductTypeList = mProductTypeList;

                    ProductSlideShow ProductImages = new ProductSlideShow();

                    mProductDetailPage.Images = productRepos.LayProductImageTheoIDProduct(mProduct.ID);
                    
                }
                mProductDetailPage.ProductLastest = productRepos.getProductsRandom(6);
                string view = Theme.viewPage("detail");
                if (view.Length > 0)
                {
                    return View(view, mProductDetailPage);
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
                DisposeRepos();
            }
        }
        public ActionResult Search(string pKey, int pVendor = 2, int pPage = 1)
        {
            //V308CMSEntities mEntities = new V308CMSEntities();
            //ProductRepository productRepository = new ProductRepository(mEntities);
            CreateRepos();
            SearchPage mSearchPage = new SearchPage();
            List<Product> mProductList = null;
            List<Market> mMarketList = null;
            try
            {
                //mList = newsRepository.LayDanhSachTinTheoKey(15, pKey, pPage);
                if (pVendor == 1)/*Tìm theo cửa hàng*/
                {
                    mMarketList = marketRepos.SearchMarketTheoTrangAndType(pPage, 30, pKey);

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
                    mProductList = productRepos.TimSanPhamTheoTen(pPage, 30, pKey.ToLower());
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

                string view = Theme.viewPage("Search");
                if (view.Length > 0)
                {
                    return View(view, mSearchPage);
                }

                return View(mSearchPage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return Content("<dx></dx>");
            }
            finally
            {
                DisposeRepos();
            }
        }
        public ActionResult YoutubeDetail(int pId = 0)
        {
            CreateRepos();
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
                string view = Theme.viewPage("Video");
                if (view.Length > 0)
                {
                    return View(view, mCommonModel);
                }

                return View(mCommonModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return Content("<h2>Có lỗi xảy ra trên hệ thống ! Vui lòng thử lại sau.</h2>");
            }
            finally
            {
                DisposeRepos();
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
                Console.WriteLine("error :", ex);
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
                Console.WriteLine("error :", ex);
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
                Console.WriteLine("error :", ex);
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
        public JsonResult CheckMarketRegister(string pUserName, string pPassWord, string pPassWord2, string pEmail, string pMobile, string InvisibleCaptchaValue, string Captcha = "", bool rbtAgree = false, string pFullName = "")
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            ETLogin mETLogin = null;
            try
            {
                mETLogin = accountRepository.CheckDangKyHome(pUserName, pPassWord, pPassWord2, pEmail, pFullName, pMobile);
                if (mETLogin.code == 1)
                {
                    //SET session cho UserId
                    Session["UserId"] = mETLogin.Account.ID;
                    Session["UserName"] = mETLogin.Account.UserName;
                    if (Session["ShopCart"] == null)
                        Session["ShopCart"] = new ShopCart();
                    //Thuc hien Authen cho User.
                    FormsAuthentication.SetAuthCookie(pUserName, true);
                    return Json(new { code = 1, message = "Đăng ký thành công. Tài khoản là : " + pUserName + "." });
                }
                else
                {
                    return Json(new { code = 0, message = mETLogin.message });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :", ex);
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }

     
       
    }

}
